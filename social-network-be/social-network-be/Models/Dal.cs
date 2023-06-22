using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace social_network_be.Models
{
    /// <summary>
    /// Communicates with the database
    /// </summary>
    public class Dal
    {
        public Response Registration(Registration registration, SqlConnection connection) 
        {
            Response response = new();
            SqlCommand cmd = new("INSERT INTO Registration(Name,Email,Password,PhoneNo,IsActive,IsApproved) VALUES('" + registration.Name +"','" + registration.Email + "','" + registration.Password + "','" + registration.PhoneNo + "',1,0) ",connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode= 200;
                response.StatusMessage = "Registration Successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Registration Failed";
            }


            return response;
        }
    
        public Response Login(Registration registration, SqlConnection connection)
        {
            SqlDataAdapter da = new("SELECT * FROM Registration WHERE Email = '" + registration.Email + "' AND Password = '" + registration.Password + "' ", connection);
            DataTable dt = new();
            da.Fill(dt);
            Response response = new(); 
            if (dt.Rows.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Login Successful";
                Registration reg = new();
                reg.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                reg.Name = Convert.ToString(dt.Rows[0]["Name"]);
                reg.Email = Convert.ToString(dt.Rows[0]["Email"]);
                response.Registration = reg;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Login failed";
                response.Registration = null;
            }
            return response;
        }

        public Response UserApproval(Registration registration, SqlConnection connection)
        {
            Response response = new();
            SqlCommand cmd = new("UPDATE Registration SET IsApproved = 1 WHERE ID = '" + registration.Id + "' AND IsActive = 1", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User approved";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User approval failed";
            }

            return response;
        }

        public Response AddNews(News news, SqlConnection connection)
        {
            Response response = new();
            SqlCommand cmd = new("INSERT INTO News(Title,Content,Email,IsActive,CreatedOn) VALUES('"+news.Title+ "','" +news.Content+ "','" +news.Email+ "',1,GETDATE())", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "News Created";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "News failed to create";
            }
            return response;
        }

        public Response NewsList(SqlConnection connection) 
        {
            Response response = new();
            SqlDataAdapter da = new("SELECT * FROM News WHERE IsActive = 1", connection);
            DataTable dt = new();
            da.Fill(dt);
            List<News> lstNews = new();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    News news = new();
                    news.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    news.Title = Convert.ToString(dt.Rows[i]["Title"]);
                    news.Content = Convert.ToString(dt.Rows[i]["Content"]);
                    news.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    news.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    news.CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]);
                    lstNews.Add(news);
                }
                if (lstNews.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "News Data Found";
                    response.listNews = lstNews;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No News Data Found";
                    response.listNews = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No News Data Found";
                response.listNews = null;
            }

            return response;
        }

        public Response AddArticle(Article article, SqlConnection connection)
        {
            Response response = new();
            SqlCommand cmd = new("INSERT INTO Article(Title,Content,Email,Image,IsActive,IsApproved) VALUES('" + article.Title + "','" + article.Content + "','" + article.Email + "','" + article.Image + "',1,0)", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Article Created";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Article failed to create";
            }
            return response;
        }

        public Response ArticleList(Article article, SqlConnection connection)
        {
            Response response = new();
            SqlDataAdapter da = null;
            if (article.Type == "User")
            {
                da = new SqlDataAdapter("SELECT * FROM Article WHERE Email = '"+article.Email+"' AND IsActive = 1", connection);
            }
            if (article.Type == "Page")
            {
                da = new SqlDataAdapter("SELECT * FROM Article WHERE IsActive = 1", connection);
            }

            DataTable dt = new();
            da.Fill(dt);
            List<Article> lstArticle = new();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Article art = new();
                    art.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    art.Title = Convert.ToString(dt.Rows[i]["Title"]);
                    art.Content = Convert.ToString(dt.Rows[i]["Content"]);
                    art.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    art.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    art.Image = Convert.ToString(dt.Rows[i]["Image"]);
                    lstArticle.Add(art);
                }
                if (lstArticle.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Article Data Found";
                    response.listArticle = lstArticle;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No Article Data Found";
                    response.listArticle = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Article Data Found";
                response.listArticle = null;
            }

            return response;
        }

        public Response ArticleApproval(Article article, SqlConnection connection)
        {
            Response response = new();
            SqlCommand cmd = new("UPDATE Article SET IsApproved = 1 WHERE ID = '" + article.Id + "' AND IsActive = 1", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Article approved";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Article approval failed";
            }

            return response;
        }

        public Response StaffRegistration(Staff staff, SqlConnection connection)
        {
            Response response = new();
            SqlCommand cmd = new("INSERT INTO Staff(Name,Email,Password,IsActive) VALUES('" + staff.Name + "','" + staff.Email + "','" + staff.Password + "',1) ", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Staff Registration Successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Staff Registration Failed";
            }


            return response;
        }
        
        public Response DeleteStaff(Staff staff, SqlConnection connection)
        {
            Response response = new();
            SqlCommand cmd = new("DELETE FROM Staff WHERE ID =  '" + staff.Id + "' AND IsActive = 1 ", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Staff Deletion Successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Staff Deletion Failed";
            }


            return response;
        }
        
        public Response AddEvent(Events events, SqlConnection connection)
        {
            Response response = new();
            SqlCommand cmd = new("INSERT INTO Events(Title,Content,Email,IsActive,CreatedOn) VALUES('" + events.Title + "','" + events.Content + "','" + events.Email +  "',1,GETDATE())", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Event Created";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Event failed to create";
            }
            return response;
        }

        public Response EventsList(SqlConnection connection)
        {
            Response response = new();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Events WHERE IsActive = 1", connection);
            DataTable dt = new();
            da.Fill(dt);
            List<Events> lstEvents = new();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Events event_ = new();
                    event_.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    event_.Title = Convert.ToString(dt.Rows[i]["Title"]);
                    event_.Content = Convert.ToString(dt.Rows[i]["Content"]);
                    event_.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    event_.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    event_.CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]);
                    lstEvents.Add(event_);
                }
                if (lstEvents.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Events Data Found";
                    response.listEvents = lstEvents;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No Event Data Found";
                    response.listEvents = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Events Data Found";
                response.listEvents = null;
            }

            return response;
        }
    }
}
