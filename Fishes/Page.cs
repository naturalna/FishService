using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishes
{
    public class Page
    {
        public int Id { get; set; }

        public int Number { get; set; }

        private ICollection<Article> article;

        public Page()
        {
            this.Article = new HashSet<Article>();
        }

        public virtual ICollection<Article> Article
        {
            get { return this.article; }
            set { this.article = value; }
        }
        
    }
}
