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
            //Obtención de la imagen 
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"..\Imagenes\beer.jpg");
            //Creacion de los filtros
            FilterNegative NegativeFilter = new FilterNegative();
            FilterGreyscale GreyFilter= new FilterGreyscale();
            //Ahora empezaremos con la creacion del  circuito que realiza la imagen
            PipeNull pipeNull = new PipeNull();
            //Segundo Filtro
            PipeSerial pipe2 = new PipeSerial(NegativeFilter, pipeNull);
            //Primer Filtro
            PipeSerial pipe1 = new PipeSerial(GreyFilter, pipe2);
            //Comienzo del recorrido de la imagen
            IPicture initialpicture = pipe1.Send(picture);
            //Parte final, guardado de la imagen
            provider.SavePicture(picture, @"..\Imagenes\nuevo.jpg");
        }
    }
}


/*
Dudas:
- ¿Tengo que crear una instancia de la clase picture o simplemente con el metodo de getimage sirve?
-

*/