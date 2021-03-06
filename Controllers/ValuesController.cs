﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace localmarket.Controllers {
 //   [Authorize]
    [Produces ("application/json")]
    [Route ("api/[controller]")]
    public class ValuesController : Controller {
        // GET api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get () {
            return new string[] { "Eshwar Rachala", "NaveenKumar" };
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public string Get (int id) {
            return "You entered:" + id;
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}