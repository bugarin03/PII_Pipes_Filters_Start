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
            IPicture picture = provider.GetPicture(@".\Imagenes\beer.jpg");
            //Creacion de los filtros
            FilterNegative NegativeFilter = new FilterNegative();
            FilterGreyscale GreyFilter = new FilterGreyscale();
            FilterBlurConvolution BlurConvolutionFilter = new FilterBlurConvolution();
            FilterSave SaveFilter = new FilterSave();
            FilterTwitter TwitterFilter = new FilterTwitter();
            //Ahora empezaremos con la creacion del  circuito que realiza la imagen
            PipeNull pipeNull = new PipeNull();
            //Segundo Filtro
            PipeSerial toSave3 = new PipeSerial(SaveFilter,pipeNull );
            PipeSerial toTwitter3 = new PipeSerial(TwitterFilter,toSave3);
            PipeSerial pipeSerial2 = new PipeSerial(NegativeFilter, toTwitter3);
            
            //Condicional
            PipeSerial toSave2 = new PipeSerial(SaveFilter,pipeSerial2 );
            PipeSerial toTwitter2 = new PipeSerial(TwitterFilter,toSave2);
            CognitivePipe conditionalPipe = new CognitivePipe(BlurConvolutionFilter,toTwitter2);
            //Primer Filtro
            PipeSerial toSave1 = new PipeSerial(SaveFilter,conditionalPipe );
            PipeSerial toTwitter1 = new PipeSerial(TwitterFilter,toSave1);
            PipeSerial pipeSerial1 = new PipeSerial(GreyFilter, toTwitter1);
            //Comienzo del recorrido de la imagen
            IPicture initialpicture = pipeSerial1.Send(picture, @".\Imagenes\beer.jpg");
            //Parte final, guardado de la imagen
            provider.SavePicture(initialpicture, @".\Imagenes\nuevo.jpg");
        }
    }
}