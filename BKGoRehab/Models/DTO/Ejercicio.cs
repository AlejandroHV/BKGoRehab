using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BKGoRehab.Models.DTO
{
    public class Ejercicio
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string SeccionCuerpo { get; set; }

        public string UrlVideo { get; set; }

        public string UrlImagen { get; set; }

        public Double Duracion { get; set; }

        public string Nivel { get; set; }


    }
}