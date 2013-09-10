using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Fish.Service.Models
{
    public class ArticleDTO
    {
        [DataMember(Name="id")]
        public int Id { get; set; }

        [DataMember(Name="title")]
        public string Title { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "imageURL")]
        public string ImageURL { get; set; }
    }
}
