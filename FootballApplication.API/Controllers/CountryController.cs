using FootballApplication.Model.Entities;
using FootballApplication.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        protected CountryRepository Repository {get;}

        public CountryController(CountryRepository repository) {
            Repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<Country> GetCountry([FromRoute] int id)
        {
            Country country = Repository.GetCountryById(id);
            if (country == null) {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetCountries()
        {
            return Ok(Repository.GetCountries());
        }

        [HttpPost]
        public ActionResult Post([FromBody] Country country) {
            if (country == null)
            {
                return BadRequest("Country info not correct");
            }

            bool status = Repository.InsertCountry(country);
            if (status)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut()]
        public ActionResult UpdateCountry([FromBody] Country country)
        {
            if (country == null)
            {
                return BadRequest("Country info not correct");
            }

            Country existingCountry = Repository.GetCountryById(country.Id);
            if (existingCountry == null)
            {
                return NotFound($"Country with id  not found");
            }

            bool status = Repository.UpdateCountry(country);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCountry([FromRoute] int id) {
            Country existingClub = Repository.GetCountryById(id);
            if (existingClub == null)
            {
                return NotFound($"Country with id {id} not found");
            }

            bool status = Repository.DeleteCountry(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Unable to delete Country with id {id}");        
        }
    }
}
