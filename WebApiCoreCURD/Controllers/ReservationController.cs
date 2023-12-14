using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreCURD.Models;

namespace WebApiCoreCURD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IRepository repository;
        public ReservationController(IRepository repo) => repository = repo;



        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.Reservations;

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            if (id == 0)
                return BadRequest("Value must be passed in the request body.");
            return Ok(repository[id]);
        }

        //[HttpPost]
        //public Reservation Post([FromBody] Reservation res) =>
        //repository.AddReservation(new Reservation
        //{
        //    Name = res.Name,
        //    StartLocation = res.StartLocation,
        //    EndLocation = res.EndLocation
        //});

        [HttpPost]
        public IActionResult Post([FromBody] Reservation res)
        {
            if (!Authenticate())
                return Unauthorized();
            return Ok(repository.AddReservation(new Reservation
            {
                Name = res.Name,
                StartLocation = res.StartLocation,
                EndLocation = res.EndLocation
            }));
        }
        bool Authenticate()
        {
            var allowedKeys = new[] { "Secret@123", "Secret#12", "SecretABC" };
            StringValues key = Request.Headers["Key"];
            int count = (from t in allowedKeys where t == key select t).Count();
            return count == 0 ? false : true;
        }

        
    }
}
