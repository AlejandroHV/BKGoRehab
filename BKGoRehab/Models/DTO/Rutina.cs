using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BKGoRehab.Models;

namespace BKGoRehab.Models.DTO
{
    public class Rutina
    {
        public int IdPaciente { get; set; }

        public int IdEjercicio { get; set; }
        

    }
}