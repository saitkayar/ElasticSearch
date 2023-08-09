using ElasticSearch.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly ECommerseRepository eCommerseRepository;

        public ECommerceController(ECommerseRepository eCommerseRepository)
        {
            this.eCommerseRepository = eCommerseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> TermQuery(string firstname) {


            return Ok(await eCommerseRepository.TermQuery(firstname));
        }

        [HttpPost("terms")]
        public async Task<IActionResult> TermsQuery(List<string> firstname)
        {


            return Ok(await eCommerseRepository.TermsQuery(firstname));
        }

        [HttpGet("prefix")]
        public async Task<IActionResult> Prefix(string firstname)
        {


            return Ok(await eCommerseRepository.Prefix(firstname));
        }


        [HttpGet("ranfe")]
        public async Task<IActionResult> range(double from,double to)
        {


            return Ok(await eCommerseRepository.range(from,to));
        }

        [HttpGet("matchall")]
        public async Task<IActionResult> match()
        {


            return Ok(await eCommerseRepository.matchall());
        }
        [HttpGet("matchfulltext")]
        public async Task<IActionResult> match(string categoryname)
        {


            return Ok(await eCommerseRepository.MatchFullText(categoryname));
        }

        [HttpGet("compıound")]
        public async Task<IActionResult> CompoundQuery(string cityname, double price, string category, string menu)
        {


            return Ok(await eCommerseRepository.CompoundQuery(cityname,price,category,menu));
        }

    }
}
