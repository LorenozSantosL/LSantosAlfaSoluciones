using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WEBAPI.Controllers
{
    public class AlumnoBecaController : ApiController
    {
        // GET: api/AlumnoBeca
        
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AlumnoBeca/5
        [Route("api/AlumnoBeca/GetByIdAlumno/{idalumno}")]
        public IHttpActionResult Get(int idalumno)
        {
            ML.Result result = BL.AlumnoBeca.GetBecaalumnoById(idalumno);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
            
        }

        // POST: api/AlumnoBeca
        [Route("api/AlumnoBeca/Add")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] ML.Alumno alumno)
        {
            ML.Result result = BL.AlumnoBeca.AddBeca(alumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        // PUT: api/AlumnoBeca/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AlumnoBeca/5
        [Route("api/AlumnoBeca/{idbeca}/{idalumno}")]
        public IHttpActionResult Delete(int idbeca, int idalumno)
        {
            ML.Result result = BL.AlumnoBeca.Delete(idbeca, idalumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }
    }
}
