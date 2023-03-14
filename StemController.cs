using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StemService.Controllers {
    [Route("")]
    [ApiController]
    public class StemsController : Controller {
        private const string uri =
            "https://raw.githubusercontent.com/dwyl/english-words/master/words_alpha.txt";
        private static readonly HttpClient client = new HttpClient();

        private HttpResponseMessage response;

        private string[] listOfWords;
        public StemsController() {
            response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode) {
                listOfWords = response.Content.ReadAsStringAsync()
                                  .Result.Replace("\r", string.Empty)
                                  .Split('\n');
            }
        }

        [HttpGet]
        public IActionResult Get(string stem) {
            if (response.IsSuccessStatusCode) {
                List<string> listOfstream = new List<string>();
                listOfstream =
                    listOfWords.Where(x => x.StartsWith(stem)).ToList();
                var k = new { data = listOfstream };
                return Ok(k);
            }

            return BadRequest();
        }
    }
}
