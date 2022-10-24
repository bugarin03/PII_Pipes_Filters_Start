using System;
using System.Drawing;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que guarda una imagen.
    /// </remarks>
    public class FilterSave : IFilter
    {
        /// Un filtro que retorna el negativo de la imagen recibida.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida pero en negativo.</returns>
        public IPicture Filter(IPicture picture)
        {
            PictureProvider provider = new PictureProvider();
            provider.SavePicture(picture, $@".\Imagenes\{this}.jpg");
            return picture;
        }
    }
}
