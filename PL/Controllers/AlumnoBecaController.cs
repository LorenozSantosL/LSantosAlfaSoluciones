using ML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoBecaController : Controller
    {
        // GET: AlumnoBeca
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

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Alumno resultItemList = JsonConvert.DeserializeObject<ML.Alumno>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
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
        public ActionResult BecaAlumno(int IdAlumno)
        {
            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();
            ML.Alumno alumno = new ML.Alumno();
            alumno.Beca = new ML.Beca();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            //ML.Result result = BL.AlumnoBeca.GetBecaalumnoById(IdAlumno);
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);
                    var responseTask = client.GetAsync("AlumnoBeca/GetByIdAlumno/" + IdAlumno);

                    responseTask.Wait();
                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.AlumnoBeca resultItemList = JsonConvert.DeserializeObject<ML.AlumnoBeca>(resultItem.ToString());
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


            if (result.Correct)
            {
                 
                alumnoBeca.AlumnoBecas = result.Objects;
                //ML.Result resultAlumno = BL.Alumno.GetById(IdAlumno);
                ML.Result resultAlumno = new  ML.Result();

                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);
                        var responseTask = client.GetAsync("Alumno/GetById/" + IdAlumno);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();

                            readTask.Wait();
                            ML.Alumno alumnoItem = new ML.Alumno();

                            alumnoItem = JsonConvert.DeserializeObject<ML.Alumno>(readTask.Result.Object.ToString());

                            resultAlumno.Object = alumnoItem;
                            resultAlumno.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }

                alumnoBeca.Alumno = (ML.Alumno)resultAlumno.Object;
               
            }
            else
            {
                // ML.Result resultAlumno = BL.Alumno.GetById(IdAlumno);

                ML.Result resultAlumno = new ML.Result();

                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);
                        var responseTask = client.GetAsync("Alumno/GetById/" + IdAlumno);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();

                            readTask.Wait();
                            ML.Alumno alumnoItem = new ML.Alumno();

                            alumnoItem = JsonConvert.DeserializeObject<ML.Alumno>(readTask.Result.Object.ToString());

                            resultAlumno.Object = alumnoItem;
                            resultAlumno.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }


                alumnoBeca.Alumno = (ML.Alumno)resultAlumno.Object;
                alumnoBeca.AlumnoBecas = result.Objects;
            }

            return View(alumnoBeca);
        }

        [HttpGet]
        public ActionResult AsignarBeca(int IdAlumno)
        {
            ML.AlumnoBeca alumnobeca = new ML.AlumnoBeca();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            //ML.Result result = BL.AlumnoBeca.GetBecaalumnoById(IdAlumno);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);
                    var responseTask = client.GetAsync("AlumnoBeca/GetByIdAlumno/" + IdAlumno);

                    responseTask.Wait();
                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.AlumnoBeca resultItemList = JsonConvert.DeserializeObject<ML.AlumnoBeca>(resultItem.ToString());
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
            ML.Alumno alumno = new ML.Alumno();
            

            if (result.Correct)
            {
                alumnobeca.AlumnoBecas = result.Objects;
            }
            else
            {
                alumnobeca.AlumnoBecas = result.Objects;   
            }

            if(alumnobeca.AlumnoBecas.Count == 2)
            {
                ViewBag.Message = "El alumno no puede ser registrado a otra beca";
                return PartialView("Modal");
            }
            else
            {
                //ML.Result resultAlumno = BL.Alumno.GetById(IdAlumno);
                ML.Result resultAlumno = new  ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);
                        var responseTask = client.GetAsync("Alumno/GetById/" + IdAlumno);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();

                            readTask.Wait();
                            ML.Alumno alumnoItem = new ML.Alumno();

                            alumnoItem = JsonConvert.DeserializeObject<ML.Alumno>(readTask.Result.Object.ToString());

                            resultAlumno.Object = alumnoItem;
                            resultAlumno.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }

                if (resultAlumno.Correct)
                {
                    alumno = (ML.Alumno)resultAlumno.Object;
                    alumno.Beca = new ML.Beca();
                    alumno.Beca.Becas = new List<object>();
                }



                if(alumnobeca.AlumnoBecas.Count == 1)
                {
                    ML.Beca beca = new ML.Beca();

                    foreach ( ML.AlumnoBeca obj in alumnobeca.AlumnoBecas)
                    {
                       
                        beca.IdBeca = obj.Beca.IdBeca;
                    }



                    //ML.Result resultSinBeca = BL.Beca.GetAllExcept(beca.IdBeca);
                    ML.Result resultSinBeca = new ML.Result();
                    resultSinBeca.Objects = new List<object>();

                    try
                    {
                        using(var client = new HttpClient())
                        {

                            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);
                            var responseTask = client.GetAsync("Beca/GetAllExcept/" + beca.IdBeca);
                            responseTask.Wait();

                            var resultServicio = responseTask.Result;

                            if (resultServicio.IsSuccessStatusCode)
                            {
                                var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                                readTask.Wait();

                                foreach (var resultItem in readTask.Result.Objects)
                                {
                                    ML.Beca resultItemList = JsonConvert.DeserializeObject<ML.Beca>(resultItem.ToString());
                                    resultSinBeca.Objects.Add(resultItemList);
                                }
                                resultSinBeca.Correct = true;
                            }


                        }

                    }
                    catch(Exception ex)
                    {
                        result.Correct = false;
                        result.Message = ex.Message;
                    }


                    if (resultSinBeca.Correct)
                    {
                        alumno.Beca.Becas = resultSinBeca.Objects;
                    }
                }
                else
                {

                    //ML.Result resultBecas = BL.Beca.GetAll();
                    ML.Result resultBecas = new ML.Result();
                    resultBecas.Objects = new List<object>();
                    try
                    {
                        using (var client = new HttpClient())
                        {

                            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);
                            var responseTask = client.GetAsync("Beca/GetAll");
                            responseTask.Wait();

                            var resultServicio = responseTask.Result;

                            if (resultServicio.IsSuccessStatusCode)
                            {
                                var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                                readTask.Wait();

                                foreach (var resultItem in readTask.Result.Objects)
                                {
                                    ML.Beca resultItemList = JsonConvert.DeserializeObject<ML.Beca>(resultItem.ToString());
                                    resultBecas.Objects.Add(resultItemList);
                                }
                                resultBecas.Correct = true;
                            }
                        }
                        }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.Message = ex.Message;
                    }


                    if (resultBecas.Correct)
                    {
                        alumno.Beca.Becas = resultBecas.Objects;
                    }
                }
                return View(alumno);
            }


        }

        [HttpPost]
        public ActionResult AgregarBeca(ML.Alumno alumno)
        {
            //ML.Result result = BL.AlumnoBeca.AddBeca(alumno);

            ML.Result result = new ML.Result();
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);

                    var postTask = client.PostAsJsonAsync<ML.Alumno>("AlumnoBeca/Add", alumno);

                    postTask.Wait();

                    var resultServicio = postTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Ocurrio un error al agregar la beca";
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
                ViewBag.Message = "Se ha agregad la beca";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al agregar la beca";
            }
            return PartialView("Modal");
        }

        
        public ActionResult QuitarBeca(int IdBeca, int IdAlumno)
        {
            //ML.Result result = BL.AlumnoBeca.Delete(IdBeca, IdAlumno);
            ML.Result result = new ML.Result();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIUrl"]);

                    var responseTask = client.DeleteAsync("AlumnoBeca/" + IdBeca+"/"+IdAlumno);

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }

                }
            }
            catch(Exception ex)
            {

            }

            if (result.Correct)
            {
                ViewBag.Message = "Se ha eliminado la beca";
            }
            else
            {
                ViewBag.Message = "no se ha podido elimnar la beca";
            }

            return PartialView("Modal");
        }
    }
}