using ML;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LSantosAlfaSolucionesEntities context = new DL.LSantosAlfaSolucionesEntities())
                {
                    DL.Alumno alumnoDL = new DL.Alumno();
                    alumnoDL.Nombre = alumno.Nombre;
                    alumnoDL.ApellidoPaterno = alumno.ApellidoPaterno;
                    alumnoDL.ApeliidoMaterno = alumno.ApellidoMaterno;
                    alumnoDL.Genero = alumno.Genero;
                    alumnoDL.Edad = alumno.Edad;


                    context.Alumno.Add(alumnoDL);

                    var queryResult = context.SaveChanges();

                    if(queryResult > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha agregado el alumno";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se agregao el alumno";
                    }


                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error; " + result.Ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.LSantosAlfaSolucionesEntities context = new DL.LSantosAlfaSolucionesEntities())
                {
                    var query = (from alumnoTDB in context.Alumno
                                 where alumnoTDB.IdAlumno == alumno.IdAlumno
                                 select alumnoTDB).SingleOrDefault();

                    if(query != null)
                    {
                        query.Nombre = alumno.Nombre;
                        query.ApellidoPaterno = alumno.ApellidoPaterno;
                        query.ApeliidoMaterno = alumno.ApellidoMaterno;
                        query.Genero = alumno.Genero;
                        query.Edad = alumno.Edad;


                        var queryResult = context.SaveChanges();

                        if(queryResult > 0)
                        {
                            result.Correct = true;
                            result.Message = "Se ha actualizado el alumno";
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "No se ha actualizado el Alumno";
                        }
                    }
                        
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error; " + result.Ex;
            }
            return result;
        }

        public static ML.Result Delete(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LSantosAlfaSolucionesEntities context = new DL.LSantosAlfaSolucionesEntities())
                {
                    var query = (from alumnoTDB in context.Alumno
                                 where alumnoTDB.IdAlumno == IdAlumno
                                 select alumnoTDB).First();
                    context.Alumno.Remove(query);
                    context.SaveChanges();
                        
                }

                result.Message = "Se ha eliminado el alumno";
                result.Correct = true;
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error; " + result.Ex;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.LSantosAlfaSolucionesEntities context = new DL.LSantosAlfaSolucionesEntities())
                {
                    var query = (from alumnoTDB in context.Alumno
                                 select new
                                 {
                                     IdAlumno = alumnoTDB.IdAlumno,
                                     Nombre = alumnoTDB.Nombre,
                                     ApellidoPaterno = alumnoTDB.ApellidoPaterno,
                                     ApellidoMaterno = alumnoTDB.ApeliidoMaterno,
                                     Genero = alumnoTDB.Genero,
                                     Edad = alumnoTDB.Edad
                                 }
                                 ).ToList();

                    result.Objects = new List<object>();

                    if(query != null && query.ToList().Count > 0)
                    {
                        foreach(var obj in query)
                        {
                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = obj.IdAlumno;
                            alumno.Nombre = obj.Nombre;
                            alumno.ApellidoPaterno = obj.ApellidoPaterno;
                            alumno.ApellidoMaterno = obj.ApellidoMaterno;
                            alumno.Genero = obj.Genero.Value;
                            alumno.Edad = obj.Edad.Value;

                            result.Objects.Add(alumno);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se enontraron registros";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error; " + result.Ex;
            }
            return result;
        }

        public static ML.Result GetById(int Idalumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LSantosAlfaSolucionesEntities context = new DL.LSantosAlfaSolucionesEntities())
                {
                    var query = (from alumnoTBD in context.Alumno
                                 where alumnoTBD.IdAlumno == Idalumno
                                 select new
                                 {
                                     IdAlumno = alumnoTBD.IdAlumno,
                                     Nombre = alumnoTBD.Nombre,
                                     ApellidoPaterno = alumnoTBD.ApellidoPaterno,
                                     ApellidoMaterno = alumnoTBD.ApeliidoMaterno,
                                     Genero = alumnoTBD.Genero,
                                     Edad = alumnoTBD.Edad
                                 }

                                 ).SingleOrDefault();

                    if(query != null)
                    {
                        ML.Alumno alumno = new ML.Alumno();

                        alumno.IdAlumno = query.IdAlumno;
                        alumno.Nombre = query.Nombre;
                        alumno.ApellidoPaterno = query.ApellidoPaterno;
                        alumno.ApellidoMaterno = query.ApellidoMaterno;
                        alumno.Genero = query.Genero.Value;
                        alumno.Edad = query.Edad.Value;

                        result.Object = alumno;

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error; " + result.Ex;
            }

            return result;

        }
    }
}
