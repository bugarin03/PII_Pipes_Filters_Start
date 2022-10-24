using System;
using System.Drawing;
using CompAndDel;
using TwitterUCU;
namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que guarda una imagen.
    /// </remarks>
    public class FilterTwitter : IFilter
    {
        /// Un filtro que retorna el negativo de la imagen recibida.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida pero en negativo.</returns>
        public IPicture Filter(IPicture picture)
        {
            var twitter = new TwitterImage();
           Console.WriteLine(twitter.PublishToTwitter($"{this}",$@".\Imagenes\{this}.jpg"));
            return picture;
        }
    }
}