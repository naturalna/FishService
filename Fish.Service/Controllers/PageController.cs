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
    public class PageController : ApiController
    {
        // GET api/page
        [ActionName("getbypage")]
        public HttpResponseMessage Get(int id)
        {
            var context = new FishesContext();
            var pages =
                from page in context.Page
                where page.Number == id
                select new PageDTO()
                {
                    Id = page.Id,
                    Number = page.Number,
                    article =
                    from a in page.Article
                    select new ArticleDTO()
                    {
                        Description = a.Description,
                        Id = a.Id,
                        ImageURL = a.ImageURL,
                        Title = a.Title,
                    }
                };
            var response = this.Request.CreateResponse(HttpStatusCode.OK, pages);
            return response;
        
        }
    }
}
