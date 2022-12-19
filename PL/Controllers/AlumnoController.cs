using ML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);

                    var responseTask = client.GetAsync("Alumno/GetAll");

                    responseTask.Wait();
                    var resultServicio = responseTask.Result;

                    if(resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach(var resultItem in readTask.Result.Objects)
                        {
                            ML.Alumno resultItemList = JsonConvert.DeserializeObject<ML.Alumno>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                        result.Correct = true;
                    }
                }

            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }

            //ML.Result result = BL.Alumno.GetAll();

            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(alumno);
        }

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();

            if(IdAlumno == null)
            {
                return View(alumno);
            }
            else
            {
                ML.Result result = new ML.Result();
                try
                {
                    using(var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);
                        var responseTask = client.GetAsync("Alumno/GetById/" + IdAlumno.Value);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();

                            readTask.Wait();
                            ML.Alumno alumnoItem = new ML.Alumno();

                            alumnoItem = JsonConvert.DeserializeObject<ML.Alumno>(readTask.Result.Object.ToString());

                            result.Object = alumnoItem;
                            result.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }
                //ML.Result result = BL.Alumno.GetById(IdAlumno.Value);

                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el usuario seleccionado";
                }
                return View(alumno);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
           if(alumno.IdAlumno == 0)
            {
                // ML.Result result = BL.Alumno.Add(alumno);
                ML.Result result = new ML.Result();

                try
                {
                    using(var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);

                        var postTask = client.PostAsJsonAsync<ML.Alumno>("Alumno/Add", alumno);

                        postTask.Wait();

                        var resultServicio = postTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "Ocurrio un error al agregar al usuario";
                        }
                    }
                }
                catch(Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha agregad el alumno";
                }
                else
                {
                    ViewBag.Message = "No se pudo agregar el alumno";
                }
            }
            else
            {
                //ML.Result result = BL.Alumno.Update(alumno);

                ML.Result result = new ML.Result();
                try
                {
                    using(var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);

                        var postask = client.PutAsJsonAsync<ML.Alumno>("Alumno/Update/" + alumno.IdAlumno, alumno);

                        postask.Wait();

                        var resultServicio = postask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "Ocurrio un error al agregar al usuario";
                        }


                    }
                }
                catch(Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualizado el alumno";
                }
                else
                {
                    ViewBag.Message = "No se pudo actualizar el alumno";
                }
            }
            return PartialView("Modal");
        }

        public ActionResult Delete(int IdAlumno)
        {
            if(IdAlumno > 0)
            {
                //ML.Result result = BL.Alumno.Delete(IdAlumno);

                ML.Result result = new ML.Result();
                try
                {
                    using(var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);

                        var responseTask = client.DeleteAsync("Alumno/Delete/" + IdAlumno);

                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }

                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }


                if (result.Correct)
                {
                    ViewBag.Message = "Se ha eliminado el alumno";
                }
                else
                {
                    ViewBag.Message = "No se pudo eliminar el alumno";
                }
            }
            return PartialView("Modal");
        }
    }
}