using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WEBAPI.Controllers
{
    public class AlumnoController : ApiController
    {
        // GET: api/Alumno
        [Route("api/Alumno/GetAll")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            ML.Result result = BL.Alumno.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // GET: api/Alumno/5
        [Route("api/Alumno/GetById/{idalumno}")]
        [HttpGet]
        public IHttpActionResult Get(int idalumno)
        {

            ML.Result result = BL.Alumno.GetById(idalumno);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // POST: api/Alumno

        [Route("api/Alumno/Add")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Add(alumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        // PUT: api/Alumno/5
        [Route("api/Alumno/Update/{idalumno}")]
        [HttpPut]
        public IHttpActionResult Put(int idalumno, [FromBody] ML.Alumno alumno)
        {
            alumno.IdAlumno = idalumno;

            ML.Result result = BL.Alumno.Update(alumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        // DELETE: api/Alumno/5
        [Route("api/Alumno/Delete/{idalumno}")]
        public IHttpActionResult Delete(int idalumno)
        {
            ML.Result result = BL.Alumno.Delete(idalumno);

            if (result.Correct)
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }

        }
    }
}
