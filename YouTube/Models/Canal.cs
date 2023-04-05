using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YouTube.Datos;

namespace YouTube.Models
{
    public class Canal
    {
        public int IDCanal { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string Pais { get; set; }
        public string FechaInscripcion { get; set; }

        public static Respuesta Agregar(Canal canal)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@Nombre", canal.Nombre),
                new Parametro("@Descripcion", canal.Descripcion),
                new Parametro("@Categoria", canal.Categoria),
                new Parametro("@Pais", canal.Pais),
                new Parametro("@FechaInscripcion", canal.FechaInscripcion)
            };

            return DBDatos.Ejecutar("Canal_Agregar", parametros);
        }

        public static Respuesta ListarxNombre(string nombre)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@Nombre", nombre)
            };

            return DBDatos.Listar("Canal_ListarxNombre", parametros);
        }

        public static Respuesta ListarxToken(string token)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@Token", token)
            };

            return DBDatos.Listar("CanalxToken_Listar", parametros);
        }
    }   
}