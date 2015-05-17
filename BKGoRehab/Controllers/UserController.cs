using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BKGoRehab.Models;
using BKGoRehab.Models.DTO;
using BKGoRehab.Models.DAL;
using BKGoRehab.Content.Util;


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
        public ApiResponse GetPatientExcercises(int patientId)
        {
            SystemFail error = new SystemFail();
            if (patientId != 0)
            {
                var excercises = dbDal.GetPatientExcercisesByPatientId(patientId, error);
                if (error.IsError)
                {
                    return new ApiResponse
                    {
                        Data = null,
                        Error = error.Error,
                        Message = error.Message
                    };
                }
                if (excercises == null && !error.IsError)
                {
                    return new ApiResponse
                    {
                        Data = null,
                        Message = error.Message
                    };
                }
                return new ApiResponse
                {
                    Data = excercises
                };
            }
            else
            {
                return new ApiResponse
                {
                    Message = "Parametro patientId Vacio"
                };
            }




        }

        [HttpPost]
        public ApiResponse LoginPatientByUserName([FromBody] Usuario user)
        {

            SystemFail error = new SystemFail();
            if (user != null)
            {
                var loggedPatient = dbDal.GetPatientByUserNameAndPassword(user.UserName, user.PassWord, error);
                if (error.IsError)
                {
                    return new ApiResponse
                    {
                        Data = null,
                        Error = error.Error,
                        Message = error.Message
                    };
                }
                if (loggedPatient == null && !error.IsError)
                {
                    return new ApiResponse
                    {
                        Data = null,
                        Message = error.Message
                    };
                }

                return new ApiResponse
                {
                    Data = loggedPatient,
                    Message = "Terapista Logeado"
                };
            }
            else
            {
                return new ApiResponse
                {
                    Message = "No se recibio la informacion del usuario.(Parametro User vacio)"
                };
            }




        }

        [HttpPost]
        public ApiResponse LoginTerapitsByUserName([FromBody] Usuario user, string dummy)
        {

            SystemFail error = new SystemFail();
            if (user != null)
            {
                var therapist = dbDal.GetTerapistByUserNameAndPassword(user.UserName, user.PassWord, error);
                if (error.IsError)
                {
                    return new ApiResponse
                    {
                        Data = null,
                        Error = error.Error,
                        Message = error.Message
                    };
                }
                if (therapist == null && !error.IsError)
                {
                    return new ApiResponse
                    {
                        Data = null,
                        Message = error.Message
                    };
                }

                return new ApiResponse
                {
                    Data = therapist,
                    Message = "Terapista Logeado"
                };
            }
            else
            {
                return new ApiResponse
                {
                    Data = null,
                    Error = null,
                    Message = "No se recibio la informacion del usuario.(Parametro User vacio)"
                };

            }








        }

    }
}
