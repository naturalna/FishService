using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Newtonsoft.Json;
using Fishes.Model;
using Fishes;

namespace DB.Client
{
    [JsonObject("results")]
    class Results
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
    }

    [JsonObject("resultsAll")]
    class PartResult
    {
        [JsonProperty("results")]
        public List<Results> allResults { get; set; }
    }

    [JsonObject("article")]
    class ArticleIn
    {
        [JsonProperty("dataObjects")]
        public List<DataObjects> DataObjects { get; set; }

    }

    [JsonObject("dataObjects")]
    class DataObjects
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("dataObjectVersionID")]
        public string dataObjectVersionID { get; set; }
        [JsonProperty("eolMediaURL")]
        public string eolMediaURL { get; set; }
        [JsonProperty("bibliographicCitation")]
        public string bibliographicCitation { get; set; }
        [JsonProperty("dataType")]
        public string dataType { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            for (int k = 40; k < 41; k++)
            {

                HttpClient client = new HttpClient();
                int page = k;
                string baseURL = "http://eol.org/api/";
                client.BaseAddress = new Uri(baseURL);
                string query = "search/1.0.json?q=fish&page=" + page + "&exact=false&filter_by_taxon_concept_id=1&filter_by_hierarchy_entry_id=143&filter_by_string=&cache_ttl=";


                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(query).Result;
                response.EnsureSuccessStatusCode();
                string ans = response.Content.ReadAsStringAsync().Result;
                var results = JsonConvert.DeserializeObjectAsync<PartResult>(ans).Result;

                FishesContext context = new FishesContext();
                var all = results.allResults;

                Page newPage = new Page()
                {
                    Number = k,
                };

                context.Page.Add(newPage);
                context.SaveChanges();

                for (int i = 0; i < all.Count; i++)
                {
                    int fishId = all[i].Id;
                    string queryDetails = "pages/1.0/" + fishId + ".json?images=10&videos=10&sounds=0&maps=0&text=10&iucn=false&subjects=overview&licenses=all&details=true&common_names=true&synonyms=true&references=true&vetted=0&cache_ttl=";
                    HttpResponseMessage resp = client.GetAsync(queryDetails).Result;
                    resp.EnsureSuccessStatusCode();
                    string answer = resp.Content.ReadAsStringAsync().Result;

                    var partResult = JsonConvert.DeserializeObjectAsync<ArticleIn>(answer).Result;

                    Article article = new Article();
                    for (int j = 0; j < partResult.DataObjects.Count; j++)
                    {
                        if (partResult.DataObjects[j].dataType == "http://purl.org/dc/dcmitype/Text")
                        {
                            article.Description = partResult.DataObjects[j].Description;
                            article.Page = newPage;
                            article.Title = all[i].Title;
                            article.ImageURL = partResult.DataObjects[j].eolMediaURL;

                            break;
                        }
                    }
                    if (article.ImageURL == null)
                    {
                        for (int j = 0; j < partResult.DataObjects.Count; j++)
                        {
                            if (partResult.DataObjects[j].eolMediaURL != null)
                            {
                                article.ImageURL = partResult.DataObjects[j].eolMediaURL;
                                break;
                            }
                        }
                    }

                    context.Article.Add(article);
                    context.SaveChanges();
                }
            }
        }
    }
}
