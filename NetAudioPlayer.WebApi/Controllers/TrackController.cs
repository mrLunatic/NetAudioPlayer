using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetAudioPlayer.WebApi.Controllers
{
    public class TrackController : ApiController
    {
        // GET: api/Track
        public IEnumerable<string> Get([FromUri] string like = null, [FromUri] int? from = null)
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Track/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Track
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Track/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Track/5
        public void Delete(int id)
        {
        }
    }
}
