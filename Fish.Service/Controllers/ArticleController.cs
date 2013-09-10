using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fishes.Model;
using Fish.Service.Models;

namespace Fish.Service.Controllers
{
    public class ArticleController : ApiController
    {
        [HttpGet]
        [ActionName("search")]
        public HttpResponseMessage searchForFishes(string value)
        {
            var context = new FishesContext();

            var foundFish =
                from f in context.Article
                where f.Title.Contains(value)
                select new ArticleDTO()
                {
                    Description = f.Description,
                    Id = f.Id,
                    ImageURL = f.ImageURL,
                    Title = f.Title,
                };

            var response = this.Request.CreateResponse(HttpStatusCode.OK, foundFish);
            return response;
        }

        [HttpGet]
        [ActionName("getbyid")]
        public HttpResponseMessage searchForFishes(int id)
        {
            var context = new FishesContext();

            var foundFish =
                from f in context.Article
                where f.Id == id
                select new ArticleDTO()
                {
                    Description = f.Description,
                    Id = f.Id,
                    ImageURL = f.ImageURL,
                    Title = f.Title,
                };

            var response = this.Request.CreateResponse(HttpStatusCode.OK, foundFish);
            return response;
        }
    }
}
