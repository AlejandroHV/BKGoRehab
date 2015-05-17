using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BKGoRehab.Models.DTO
{
    public class Paciente
    {
        public int Id { get; set; }

        public string Estado { get; set; }

        public string Incapacidad { get; set; }

        public DateTime FechaUltimoTratamiento { get; set; }

        public string PrimerNombre { get; set; }


        public string PrimerApellido { get; set; }

        public string UserName { get; set; }

        public string Contrasena { get; set; }

    }
}