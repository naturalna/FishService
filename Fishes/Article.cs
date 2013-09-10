using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fishes
{
    public class Article
    {
        public int Id { get; set; }
        public Page Page { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}
