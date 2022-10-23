using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompAndDel;
using TwitterUCU;
using CognitiveCoreUCU;
using CompAndDel.Filters;

namespace CompAndDel.Pipes
{
    public class CognitivePipe : IPipe
    {
        protected IFilter filtro;
        protected IPipe nextPipe;
        protected bool Face { get; set; }


        /// <summary>
        /// La cañería recibe una imagen, le aplica un filtro si la cara existe procediendo a envíar a la siguiente cañería, en caso  contrario,
        /// no se le aplicara ningúnfiltro y tmp se guardaria.
        /// </summary>
        /// <param name="filtro">Filtro que se debe aplicar sobre la imagen</param>
        /// <param name="nextPipe">Siguiente cañería</param>
        public CognitivePipe(IFilter filtro, IPipe nextPipe)
        {
            this.nextPipe = nextPipe;
            this.filtro = filtro;
        }
        /// <summary>
        /// Devuelve el proximo IPipe
        /// </summary>
        public IPipe Next
        {
            get { return this.nextPipe; }
        }
        /// <summary>
        /// Devuelve el IFilter que aplica este pipe
        /// </summary>
        public IFilter Filter
        {
            get { return this.filtro; }
        }

        /// <summary>
        /// Este metodo se utiliza para verificar si realmete hay un rostro en la imagen.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public bool DetectFace(string image)
        {
            CognitiveFace cog = new CognitiveFace(true, System.Drawing.Color.GreenYellow);
            cog.Recognize(@$"{image}");
            if (cog.FaceFound)
            {
                this.Face = true;
                return true;
            }
            else
            {
                this.Face = false;
                return false;
            }
        }

        /// <summary>
        /// Recibe una imagen, dependiendo de las condiciones le aplica un filtro o no y la envía al siguiente Pipe
        /// </summary>
        /// <param name="picture">Imagen a la cual se debe aplicar el filtro</param>
        public IPicture Send(IPicture picture,string path)
        {
            FilterBlurConvolution filter = new FilterBlurConvolution();
            var twitter = new TwitterImage();

            if (this.DetectFace(path))
            {
                picture = this.filtro.Filter(picture);
                this.Save(picture);
                Console.WriteLine(this.filtro.ToString());
            }
            
            Console.WriteLine(twitter.PublishToTwitter($"{this.filtro.ToString()}", $@".\Imagenes\{this.filtro.ToString()}.jpg"));
            return this.nextPipe.Send(picture, $@".\Imagenes\{this.filtro.ToString()}.jpg");
        }

        public void Save(IPicture picture)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picturewithfilter = this.filtro.Filter(picture);
            provider.SavePicture(picture, $@".\Imagenes\{this.filtro.ToString()}.jpg");
        }
    }
}


/*
    Sería aplicar el filtro como ultimo filtro?
    falta aplicar el filtro según la condicion de la cara 
*/