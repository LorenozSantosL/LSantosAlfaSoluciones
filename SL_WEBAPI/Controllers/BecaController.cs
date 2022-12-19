using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WEBAPI.Controllers
{
    public class BecaController : ApiController
    {
        // GET: api/Beca
        [Route("api/Beca/GetAll")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            ML.Result result = BL.Beca.GetAll();

            if (result.Correct)
            {
                return Ok(result);

            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }

        }

        // GET: api/Beca/5
        [Route("api/Beca/GetAllExcept/{idbeca}")]
        [HttpGet]
        public IHttpActionResult Get(int idbeca)
        {
            ML.Result result = BL.Beca.GetAllExcept(idbeca);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
            
        }

        // POST: api/Beca
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Beca/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Beca/5
        public void Delete(int id)
        {
        }
    }
}
