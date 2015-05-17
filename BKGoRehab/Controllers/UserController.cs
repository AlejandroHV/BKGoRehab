using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BKGoRehab.Models;
using BKGoRehab.Models.DTO;
using BKGoRehab.Models.DAL;


namespace BKGoRehab.Controllers
{
    /// <summary>
    /// Allows to manage all the transactions related to the patient.
    /// </summary>
    public class UserController : ApiController
    {

        private GoRehabDAL dbDal = new GoRehabDAL();

        /// <summary>
        /// Obtain a list of all the patients in the database.
        /// </summary>
        /// <returns>List  of patients</returns>
        public List<Paciente> Get()
        {

            var response = dbDal.GetAllPatients();


            return response;
        }

        /// <summary>
        /// Obtain an specific patient by it's id
        /// </summary>
        /// <param name="id">Patient Id</param>
        /// <returns>Patient Object. If it's null there is not an patient identificated with that Id in the system</returns>
        public Paciente GetPatientById(int id)
        {

            var patientResponse = dbDal.GetPatientById(id);

            return patientResponse;
        }
        /// <summary>
        /// Obtain the excercises asigned to an specific Patient
        /// </summary>
        /// <param name="patientId">Id/Primary of the paitient</param>
        /// <returns>List of Ejercicio object. Null if there are non excercises for the user</returns>
        public List<Ejercicio> GetPatientExcercises(int patientId)
        {
            try
            {
                return dbDal.GetPatientExcercisesByPatientId(patientId);
            }
            catch (Exception)
            {

                return null;
            }

        }

        [HttpPost]
        public Paciente LoginPatientByUserName([FromBody] Usuario user)
        {
            Paciente loggedPatient = null;

            try
            {
                if (user != null)
                {
                    loggedPatient = dbDal.GetPatientByUserNameAndPassword(user.UserName, user.PassWord);
                }

            }
            catch (Exception)
            {

                
            }

            return loggedPatient;
        }

        /*public Paciente LoginTerapitsByUserName([FromBody] Usuario user)
        {
            Paciente loggedPatient = null;

            try
            {
                if (user != null)
                {
                    loggedPatient = dbDal.GetUserByUserNameAndPassword(user.UserName, user.PassWord);
                }

            }
            catch (Exception)
            {


            }

            return loggedPatient;
        }*/

    }
}
