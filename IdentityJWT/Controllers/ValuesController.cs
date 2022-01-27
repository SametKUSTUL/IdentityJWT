using IdentityJWT.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {

        private AppIdentityDbContext context;

        public ValuesController(AppIdentityDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Authorize(Roles ="admin")] // Admin rolune sahip kullanıcılar yapar
        public IEnumerable<string> Get()
        {
            return context.Users.Select(x => x.UserName).ToArray();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [Authorize(Roles = "admin, editor")] // Admin,editor rolune sahip kullanıcılar yapar
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
