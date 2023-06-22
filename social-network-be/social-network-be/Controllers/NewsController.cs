using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using social_network_be.Models;
using System.Data.SqlClient;

namespace social_network_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public NewsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddNews")]
        public Response AddNews(News news)
        {
            Response response = new();
            SqlConnection connection = new(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new();
            response = dal.AddNews(news, connection);
            return response;
        }

        [HttpGet]
        [Route("NewsList")]
        public Response NewsList()
        {
            Response response = new();
            SqlConnection connection = new(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new();
            response = dal.NewsList(connection);
            return response;
        }

    }
}
