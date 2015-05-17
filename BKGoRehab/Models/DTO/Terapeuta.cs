using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BKGoRehab.Models.DTO
{
    public class Terapeuta
    {

        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public string Especialidad { get; set; }

        public string PrimerNombre { get; set; }


        public string PrimerApellido { get; set; }


    }
}