using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using social_network_be.Models;
using System.Data.SqlClient;

namespace social_network_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EventsController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("AddEvent")]
        public Response AddEvent(Events events)
        {
            Response response = new();
            SqlConnection connection = new(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new();
            response = dal.AddEvent(events, connection);
            return response;
        }
        
        [HttpGet]
        [Route("EventList")]
        public Response EventList()
        {
            Response response = new();
            SqlConnection connection = new(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new();
            response = dal.EventsList(connection);
            return response;
        }

    }
}
