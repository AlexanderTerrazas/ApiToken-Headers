using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using YouTube.Datos;
using YouTube.Models;

namespace YouTube.Controllers
{
    public class VideoController : ApiController
    {
        [Route("api/Video/Crear")]
        [HttpPost]
        public dynamic Agregar(Video video, HttpRequestMessage request)
        {
            Respuesta respuesta = new Respuesta();

            string idCanal = validarToken(request);

            if(idCanal == "")
            {
                respuesta.exito = false;
                respuesta.message = "Token incorrecto";
                return respuesta;
            }

            video.IDCanal = idCanal;

            return Video.Agregar(video);
        }

        public string validarToken(HttpRequestMessage request)
        {
            string token = "";

            foreach (var item in request.Headers)
            {
                if (item.Key.Equals("Authorization"))
                {
                    token = item.Value.First();
                    break;
                }
            }

            DataTable table = Canal.ListarxToken(token).result;

            if(table.Rows.Count > 0)
            {
                return table.Rows[0]["IDCanal"].ToString();
            }

            return "";
        }
    }
}