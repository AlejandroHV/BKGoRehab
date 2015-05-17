using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BKGoRehab.Models;
using BKGoRehab.Models.DTO;
using BKGoRehab.Models.DAL;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BKGoRehab.Controllers
{

    /// <summary>
    /// Allow to obtain the diferrent Routines.
    /// </summary>
    public class EjerciciosController : ApiController
    {

        private GoRehabDAL dbDal = new GoRehabDAL();
        /// <summary>
        /// Obtain All the routines
        /// </summary>
        /// <returns></returns>
        public List<Ejercicio> Get()
        {

            List<Ejercicio> response = dbDal.ObtenerTodosEjercicios();
            return response;
        }

    }
}