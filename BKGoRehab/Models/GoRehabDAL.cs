using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BKGoRehab.Models;
using BKGoRehab.Models.DTO;

namespace BKGoRehab.Models.DAL
{
    public class GoRehabDAL
    {

        private DBGoRehabEntities _dbGoRehabContext;


       
        public GoRehabDAL()
        {
            if (_dbGoRehabContext == null)
            {
                _dbGoRehabContext = new DBGoRehabEntities();
            }
        }


        /// <summary>
        /// Obtiene todos los ejercicios disponibles en la plataforma
        /// </summary>
        /// <returns></returns>
        public List<Ejercicio> GetAllExcercises()
        {
            

            var objectExcercises = from excercises in _dbGoRehabContext.tblEjercicio
                                select new Ejercicio { 
                                    Id = excercises.Id, 
                                    Nombre = excercises.Nombre, 
                                    Descripcion = excercises.Descripcion, 
                                    Nivel = excercises.Nivel, 
                                    Duracion = (double)excercises.Duracion, 
                                    SeccionCuerpo = excercises.SeccionCuerpo,
                                    UrlImagen = excercises.URLImagen,
                                    UrlVideo = excercises.URLVideoVimeo
                                    
                                };


            return objectExcercises.ToList();
        }

        /// <summary>
        /// Obtain all patientis in the database
        /// </summary>
        /// <returns></returns>
        public List<Paciente> GetAllPatients()
        {


            var objectPatient = from patient in _dbGoRehabContext.tblPaciente
                                  join user in _dbGoRehabContext.tblUsuario on patient.IdUsuario equals user.Id    
                                select new Paciente {
                                    Id = patient.Id,
                                    PrimerApellido = user.PrimerApellido,
                                    PrimerNombre = user.PrimerNombre,
                                    Estado = patient.Estado,
                                    FechaUltimoTratamiento = (DateTime)patient.FechaUltimoTratamiento,
                                    Incapacidad = patient.Incapacidad,
                                    UserName = user.UserName,
                                    Contrasena = user.Contrasena
                                };


            return objectPatient.ToList();
        }

        public Paciente GetPatientById(int patientId)
        {

            var patientObject = from patient in _dbGoRehabContext.tblPaciente 
                          join user in _dbGoRehabContext.tblUsuario on patient.IdUsuario equals user.Id
                          where patient.Id == patientId
                           select new Paciente
                                {
                                    Id = patient.Id,
                                    PrimerApellido = user.PrimerApellido,
                                    PrimerNombre = user.PrimerNombre,
                                    Estado = patient.Estado,
                                    FechaUltimoTratamiento = (DateTime)patient.FechaUltimoTratamiento,
                                    Incapacidad = patient.Incapacidad,
                                    UserName = user.UserName,
                                    Contrasena = user.Contrasena
                                };

            return patientObject.FirstOrDefault();

        }


        public Ejercicio GetExcerciseById(int excerciseId)
        {
            var excerciseObject = from excercise in _dbGoRehabContext.tblEjercicio
                                  where excercise.Id == excerciseId
                                  select new Ejercicio
                                  {
                                      Id = excercise.Id,
                                      Nombre = excercise.Nombre,
                                      Descripcion = excercise.Descripcion,
                                      Nivel = excercise.Nivel,
                                      Duracion = (double)excercise.Duracion,
                                      SeccionCuerpo = excercise.SeccionCuerpo,
                                      UrlImagen = excercise.URLImagen,
                                      UrlVideo = excercise.URLVideoVimeo
                                  };
            return excerciseObject.FirstOrDefault();
        
        }


        public void InsertPatientRoutine(int patientId,int excerciseId)
        {
            tblRutina newRoutine = new tblRutina();
            newRoutine.IdEjercicio = excerciseId;
            newRoutine.IdPaciente = patientId;
            _dbGoRehabContext.Entry(newRoutine).State = System.Data.EntityState.Added;
            _dbGoRehabContext.SaveChanges();
           
        
        }


        public List<Ejercicio> GetPatientExcercisesByPatientId(int patientId)
        {
            var objectExcercises = from excercises in _dbGoRehabContext.tblEjercicio
                                   join routine in _dbGoRehabContext.tblRutina on excercises.Id equals routine.IdEjercicio
                                   join patient in _dbGoRehabContext.tblPaciente on routine.IdPaciente equals patient.Id
                                   where patient.Id == patientId
                                   select new Ejercicio
                                   {
                                       Id = excercises.Id,
                                       Nombre = excercises.Nombre,
                                       Descripcion = excercises.Descripcion,
                                       Nivel = excercises.Nivel,
                                       Duracion = (double)excercises.Duracion,
                                       SeccionCuerpo = excercises.SeccionCuerpo,
                                       UrlImagen = excercises.URLImagen,
                                       UrlVideo = excercises.URLVideoVimeo

                                   };


            return objectExcercises.ToList();

        }


        public Paciente GetPatientByUserName(string userName)
        {
            Paciente userPatient = null;
            if (!string.IsNullOrEmpty(userName))
            {
                var userObject = (from user in _dbGoRehabContext.tblUsuario
                                  where user.UserName.Equals(userName)
                                  select user).FirstOrDefault();
                if (userObject != null)
                {
                    userPatient = (from patient in _dbGoRehabContext.tblPaciente
                                           where patient.IdUsuario == userObject.Id
                                           select new Paciente
                                           {
                                               Id = patient.Id,
                                               PrimerApellido = userObject.PrimerApellido,
                                               PrimerNombre = userObject.PrimerNombre,
                                               Estado = patient.Estado,
                                               FechaUltimoTratamiento = (DateTime)patient.FechaUltimoTratamiento,
                                               Incapacidad = patient.Incapacidad,
                                               UserName = userObject.UserName,
                                               Contrasena = userObject.Contrasena
                                           }).FirstOrDefault();

                }                           
            }
            return userPatient;
            
        }
        public Paciente GetPatientByUserNameAndPassword(string userName,string password)
        {
            Paciente userPatient = null;
            if (!string.IsNullOrEmpty(userName))
            {
                var userObject = (from user in _dbGoRehabContext.tblUsuario
                                  where user.UserName.Equals(userName)
                                  where user.Contrasena.Equals(password)
                                  select user).FirstOrDefault();
                if (userObject != null)
                {
                    userPatient = (from patient in _dbGoRehabContext.tblPaciente
                                   where patient.IdUsuario == userObject.Id
                                   select new Paciente
                                   {
                                       Id = patient.Id,
                                       PrimerApellido = userObject.PrimerApellido,
                                       PrimerNombre = userObject.PrimerNombre,
                                       Estado = patient.Estado,
                                       FechaUltimoTratamiento = (DateTime)patient.FechaUltimoTratamiento,
                                       Incapacidad = patient.Incapacidad,
                                       UserName = userObject.UserName,
                                       Contrasena = userObject.Contrasena
                                   }).FirstOrDefault();

                }
            }
            return userPatient;

        }

        /*public Paciente GetTerapistByUserNameAndPassword(string userName, string password)
        {
            Paciente userPatient = null;
            if (!string.IsNullOrEmpty(userName))
            {
                var userObject = (from user in _dbGoRehabContext.tblUsuario
                                  where user.UserName.Equals(userName)
                                  where user.Contrasena.Equals(password)
                                  select user).FirstOrDefault();
                if (userObject != null)
                {
                    userPatient = (from terapits in _dbGoRehabContext.tblTerapeuta
                                   where terapits.IdUsuario == userObject.Id
                                   select new Paciente
                                   {
                                       Id = terapits.Id,
                                       PrimerApellido = userObject.PrimerApellido,
                                       PrimerNombre = userObject.PrimerNombre,
                                       Estado = patient.Estado,
                                       FechaUltimoTratamiento = (DateTime)patient.FechaUltimoTratamiento,
                                       Incapacidad = patient.Incapacidad,
                                       UserName = userObject.UserName,
                                       Contrasena = userObject.Contrasena
                                   }).FirstOrDefault();

                }
            }
            return userPatient;

        }*/
    }
}