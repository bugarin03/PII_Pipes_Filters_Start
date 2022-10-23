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
            IPicture picture = provider.GetPicture(@".\Imagenes\luke.jpg");
            //Creacion de los filtros
            FilterNegative NegativeFilter = new FilterNegative();
            FilterGreyscale GreyFilter = new FilterGreyscale();
            FilterBlurConvolution BlurConvolutionFilter = new FilterBlurConvolution();
            //Ahora empezaremos con la creacion del  circuito que realiza la imagen
            PipeNull pipeNull = new PipeNull();
            //Segundo Filtro
            PipeSerial pipe2 = new PipeSerial(NegativeFilter, pipeNull);
            //Condicional
            CognitivePipe conditionalPipe = new CognitivePipe(BlurConvolutionFilter,pipe2);
            //Primer Filtro
            PipeSerial pipe1 = new PipeSerial(GreyFilter, conditionalPipe);
            //Comienzo del recorrido de la imagen
            IPicture initialpicture = pipe1.Send(picture, @".\Imagenes\luke.jpg");
            //Parte final, guardado de la imagen
            provider.SavePicture(initialpicture, @".\Imagenes\nuevo.jpg");
        }
    }
}