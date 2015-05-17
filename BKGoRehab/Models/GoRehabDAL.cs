using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BKGoRehab.Models;
using BKGoRehab.Models.DTO;
using BKGoRehab.Content.Util;

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


        public List<Ejercicio> GetPatientExcercisesByPatientId(int patientId ,SystemFail error)
        {
            try
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
            catch (Exception ex)
            {
                error.IsError = true;
                error.Error = ex;
                error.Message = "Error al obtener los ejercicios del paciente";
                return null;
            }
            

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
        public Paciente GetPatientByUserNameAndPassword(string userName,string password, SystemFail error)
        {
            Paciente userPatient = null;
            try
            {
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
                        if (userPatient == null)
                        {
                            error.Message = "El usuario no es un terapeuta.";

                        }
                    }
                    else
                    {
                        error.Message = "El usuario no es un paciente.";
                    }
                }
                else
                {
                    error.Message = "Los datos de log in son insuficientes.";
                }
            }
            catch (Exception ex)
            {
                error.IsError = true;
                error.Error = ex;
                error.Message = "Error al logear al Paciente";
                userPatient = null;
            }
            
            return userPatient;

        }

        public Terapeuta GetTerapistByUserNameAndPassword(string userName, string password,SystemFail error)
        {
            Terapeuta userTherapist = null;
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    var userObject = (from user in _dbGoRehabContext.tblUsuario
                                      where user.UserName.Equals(userName)
                                      where user.Contrasena.Equals(password)
                                      select user).FirstOrDefault();
                    if (userObject != null)
                    {
                        userTherapist = (from therapits in _dbGoRehabContext.tblTerapeuta
                                         where therapits.IdUsuario == userObject.Id
                                         select new Terapeuta
                                         {
                                             Id = therapits.Id,
                                             PrimerApellido = userObject.PrimerApellido,
                                             PrimerNombre = userObject.PrimerNombre,
                                             Especialidad = therapits.Especialidad,
                                             UserName = userObject.UserName,
                                             Contrasena = userObject.Contrasena
                                         }).FirstOrDefault();

                        if (userTherapist == null)
                        {
                            error.Message = "El usuario no es un terapeuta.";
                             userTherapist = null;
                        }
                    }
                    else
                    {
                        
                            error.Message = "El usuarios no fue encontrado o los datos son incorrectos.";
                            userTherapist = null;
                        
                    }
                }else
                {
                    error.Message = "Los datos de log in son insuficientes.";
                    userTherapist = null;
                }
                
            }
            catch (Exception ex)
            {
                error.IsError = true;
                error.Error = ex;
                error.Message = "Error al logear al terapeuta";
                userTherapist = null;
            }
            return userTherapist;

        }


        public void InsertExcercise(Ejercicio ejercicio,SystemFail error)
        {
            try
            {
                tblEjercicio newExcercise = new tblEjercicio();
                newExcercise.Nombre = ejercicio.Nombre;
                newExcercise.Nivel = ejercicio.Nivel;
                newExcercise.SeccionCuerpo = ejercicio.SeccionCuerpo;
                newExcercise.URLImagen = ejercicio.UrlImagen;
                newExcercise.URLVideoVimeo = ejercicio.UrlVideo;
                newExcercise.Descripcion = ejercicio.Descripcion;
                newExcercise.Duracion = ejercicio.Duracion;
                _dbGoRehabContext.Entry(newExcercise).State = System.Data.EntityState.Added;
                _dbGoRehabContext.SaveChanges();
                error.Message = "Ejercicio guardado correctamente";
            }
            catch (Exception ex)
            {
                error.Message = "Error al  guardar el ejercicio";
                error.Error = ex;
                error.IsError = true;
            }
            


        }
    }
}