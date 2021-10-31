using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Code.Models;

namespace Code.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        // POST api/candidate
        [HttpPost]
        public Response1 Post([FromBody] AddRequest req)
        {
            Response1 resp = new Response1();
            try
            {
                resp.name = req.name;
                resp.phone = req.phone;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return resp;
        }

       
    }
}
