using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Fish.Service.Models
{
    [DataContract]
    public class PageDTO
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "number")]
        public int Number { get; set; }
        [DataMember(Name = "articles")]
        public IEnumerable<ArticleDTO> article;
    }
}