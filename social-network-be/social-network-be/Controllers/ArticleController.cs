using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using social_network_be.Models;
using System.Data.SqlClient;

namespace social_network_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ArticleController(IConfiguration configuration)
        {
            _configuration= configuration;
        }

        [HttpPost]
        [Route("AddArticle")]
        public Response AddArticle(Article article)
        {
            Response response = new();
            SqlConnection connection = new(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new();
            response = dal.AddArticle(article, connection);
            return response;
        }

        [HttpPost]
        [Route("ArticleList")]
        public Response ArticleList(Article article) 
        {
            Response response = new();
            SqlConnection connection = new(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new();
            response = dal.ArticleList(article, connection);
            return response;
        }

        [HttpPost]
        [Route("ArticleApproval")]
        public Response ArticleApproval(Article article)
        {
            Response response = new();
            SqlConnection connection = new(_configuration.GetConnectionString("SNCon").ToString());
            Dal dal = new();
            response = dal.ArticleApproval(article, connection);
            return response;
        }
    }
}
