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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace BKGoRehab.Controllers
{

    /// <summary>
    /// Allow to obtain the diferrent Routines.
    /// </summary>
    public class ExcercisesController : ApiController
    {

        private GoRehabDAL dbDal = new GoRehabDAL();
        /// <summary>
        /// Obtain All the Excercises in the database
        /// </summary>
        /// <returns>List of excercises. Null if the is not an excercise</returns>
        public List<Ejercicio> Get()
        {

            dynamic response = null;
            try
            {
                response = dbDal.GetAllExcercises();
            }
            catch (Exception)
            {

                response = null;
            }

            return response;
        }
        /// <summary>
        /// Obtain an excercise by it's id. 
        /// </summary>
        /// <param name="Id">Id of the excercise looked up.</param>
        /// <returns>Single excercise. Null if there isn't an excercise with the specific id</returns>
        [HttpGet]
        public Ejercicio GetExcerciseById(int Id)
        {
            dynamic response = null;
            try
            {
                response = dbDal.GetExcerciseById(Id);
            }
            catch (Exception)
            {

                response = null;
            }
            return response;

        }


        /// <summary>
        /// Create a new tblRutina register in the database. Asign a excercise to an a user.
        /// </summary>
        /// <param name="value">Rutina Object with the id of the excercise and the patient</param>
        /// <returns>bool indicating if the relationship was created correctly</returns>
        public bool InsertPatientExcercise([FromBody] Rutina value)
        {
            bool successfullyInserted = true;
            try
            {
                dbDal.InsertPatientRoutine(value.IdEjercicio, value.IdPaciente);
            }
            catch (Exception ex)
            {

                successfullyInserted = false;
            }

            return successfullyInserted;
        }


        
        
    }
}