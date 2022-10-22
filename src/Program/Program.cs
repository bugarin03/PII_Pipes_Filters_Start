using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using CompAndDel;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creación de la instancia de la clase Picture
            Picture image = new Picture(604,340);
            PictureProvider provider = new PictureProvider();
            //Aplicación de FilterGreyscale
            FilterGreyscale GreyFilter= new FilterGreyscale();
            IPicture filter1 = GreyFilter.Filter(image);
            provider.SavePicture(filter1,@"..\Imagenes");
            //Aplicación de FilterNegative
            FilterNegative NegativeFilter = new FilterNegative();
            IPicture filter2 = NegativeFilter.Filter(filter1);
            //Parte final, guardado de la imagen
            provider.SavePicture(image, @"PathToImageToSave.jpg");
        }
    }
}


/*
Dudas:
- ¿Tengo que crear una instancia de la clase picture o simplemente con el metodo de getimage sirve?
-

*/