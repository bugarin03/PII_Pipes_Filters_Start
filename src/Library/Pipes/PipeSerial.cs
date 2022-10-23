﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompAndDel;
using TwitterUCU;

namespace CompAndDel.Pipes
{
    public class PipeSerial : IPipe
    {
        protected IFilter filtro;
        protected IPipe nextPipe;
        

        
        
        /// <summary>
        /// La cañería recibe una imagen, le aplica un filtro y la envía a la siguiente cañería
        /// </summary>
        /// <param name="filtro">Filtro que se debe aplicar sobre la imagen</param>
        /// <param name="nextPipe">Siguiente cañería</param>
        public PipeSerial(IFilter filtro, IPipe nextPipe)
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
        /// Recibe una imagen, le aplica un filtro y la envía al siguiente Pipe
        /// </summary>
        /// <param name="picture">Imagen a la cual se debe aplicar el filtro</param>
        public IPicture Send(IPicture picture,string image)
        {
            var twitter = new TwitterImage();
            picture = this.filtro.Filter(picture);
            this.Save(picture);
            Console.WriteLine(this.filtro.ToString());
            Console.WriteLine(twitter.PublishToTwitter($"{this.filtro.ToString()}",$@".\Imagenes\{this.filtro.ToString()}.jpg"));
            return this.nextPipe.Send(picture,$@".\Imagenes\{this.filtro.ToString()}.jpg");
        }

        public void Save(IPicture picture)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picturewithfilter = this.filtro.Filter(picture);
            provider.SavePicture(picture, $@".\Imagenes\{this.filtro.ToString()}.jpg");
        } 
    }
}
