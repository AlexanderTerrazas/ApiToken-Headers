using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YouTube.Datos;

namespace YouTube.Models
{
    public class Video
    {
        public int IDVideo { get; set; }
        public string Titulo { get; set; }
        public string Ruta { get; set; }
        public string IDCanal { get; set; }

        public static Respuesta Agregar(Video video)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@titulo", video.Titulo),
                new Parametro("@ruta", video.Ruta),
                new Parametro("@IDCanal", video.IDCanal),
            };

            return DBDatos.Ejecutar("Video_Agregar", parametros);
        }
    }
}