using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using YouTube.Models;

namespace YouTube.Controllers
{
    public class CanalController : ApiController
    {
        [Route("api/Canal/Listar")]
        [HttpGet]
        public dynamic Listar(string nombre)
        {
            return Canal.ListarxNombre(nombre);
        }

        [Route("api/Canal/Crear")]
        [HttpPost]
        public dynamic Crear(Canal canal)
        {
            return Canal.Agregar(canal);
        }

    }
}