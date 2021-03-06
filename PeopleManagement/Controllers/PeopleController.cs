using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleManagement.DataModel;
using PeopleManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PeopleController : Controller
    {        
        private IPeopleRepository _peopleRepository;
        private ILogger<PeopleController> _logger;

        public PeopleController(IPeopleRepository peopleRepository, ILogger<PeopleController> logger)
        {
            _peopleRepository = peopleRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get's a list of people.
        /// </summary>
        /// <url>
        /// api/people/get?Pagesize=10&CurrentPage=1
        /// </url>
        /// <param name="page_parameter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PersonDataModel>>> Get([FromQuery]PageParameter page_parameter)
        {
            if (await _peopleRepository.checkDBConnection()) { 
                var people = await _peopleRepository.GetPeople(page_parameter);
                var newPeopleList =  people.Select(p =>
                {
                    var surname = Encoding.Unicode.GetString(p.Surname);

                    return new PersonDataModel(p.ID, p.Name, surname, p.Gender, p.Email, p.PhoneNumber, p.DateOfBirth);
                }).ToList();
                return Ok(newPeopleList);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"DB is unavailable");
            }
            
        }

        /// <summary>
        /// Gets a person by PersonId.
        /// </summary>
        /// <url>
        /// /api/person/1
        /// </url>
        /// <param name="personId">ID used to retrive the person.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{personId}")]  
        public async Task<ActionResult<PersonDataModel>> GetPerson(long personId)
        {
            if (await _peopleRepository.checkDBConnection())
            {
                var p_found = await _peopleRepository.GetPersonById(personId);
                if (p_found != null)
                {
                    var surname = Encoding.Unicode.GetString(p_found.Surname);
                    PersonDataModel p = new PersonDataModel(p_found.ID, p_found.Name, surname, p_found.Gender, p_found.Email, p_found.PhoneNumber, p_found.DateOfBirth);

                    return Ok(p);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                _logger.LogError("DB is not available");
                return StatusCode(StatusCodes.Status500InternalServerError, "DB is unavailable");
            }
        }

        /// <summary>
        /// Insert's a new person.
        /// </summary>
        /// <url>
        /// /api/people/insert
        /// </url>
        /// <param name="person">Person to be inserted.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert")]    
        public async Task<ActionResult> AddPerson([FromBody] PersonDataModel person)
        {
            if (await _peopleRepository.checkDBConnection())
            {
                try { 
                    byte[] surnameEncoded = Encoding.Unicode.GetBytes(person.Surname);
                    Person personToSave = new Person( person.Name, surnameEncoded, person.Gender, person.Email, person.PhoneNumber, person.DateOfBirth);
                    var result =  await _peopleRepository.AddPerson(personToSave);
                    
                    return Ok(result);
                }catch(Exception ex)
                {
                    _logger.LogError(ex , "Exception has been thrown while trying to insert a new person");
                    return BadRequest("Request failed");
                }
            }
            else
            {
                _logger.LogError("DB is not available");
                return StatusCode(StatusCodes.Status500InternalServerError, "DB is unavailable");
            }

        }

        /// <summary>
        /// Updates a persons details.
        /// </summary>
        /// <url>
        /// /api/people/update/1
        /// </url>
        /// <param name="personId">Person.Id to be updated.</param>
        /// <param name="person">Persons new data.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{personId}")]  
        public async Task<ActionResult<int>> UpdatePerson(long personId,[FromBody] PersonDataModel person)
        {
            if (await _peopleRepository.checkDBConnection())
            {
                var p_found = await _peopleRepository.GetPersonById(personId);
                if (p_found == null)
                {
                    return NotFound();
                }
                else
                {
                    try
                    {
                        byte[] surnameEncoded = Encoding.Unicode.GetBytes(person.Surname);
                        Person personToUpdate = new Person(person.Name, surnameEncoded, person.Gender, person.Email, person.PhoneNumber, person.DateOfBirth);
                        var totalRowsChanged = await _peopleRepository.UpdatePerson(personId, personToUpdate);

                        return Ok(totalRowsChanged);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex , "Exception has been thrown while trying to update a new person");
                        return BadRequest("Request failed");
                    }
                }
            }
            else
            {
                _logger.LogError("DB is not available");
                return StatusCode(StatusCodes.Status500InternalServerError, "DB is unavailable");
            }
        }

        /// <summary>
        /// Delete's a Person.
        /// </summary>
        /// <url>
        /// /api/people/delete/2
        /// </url>
        /// <param name="personId">PersonID used to find and delete the person.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{personId}")] 
        public async Task<ActionResult> DeletePerson(long personId)
        {
            if (await _peopleRepository.checkDBConnection())
            {
                var p_found = await _peopleRepository.GetPersonById(personId);
                if (p_found == null)
                {
                    return NotFound();
                }
                else
                {
                    var totalRowsChanged = await _peopleRepository.DeletePerson(new Person() { ID = personId });
                    return Ok(totalRowsChanged);
                }
            }
            else
            {
                _logger.LogError("DB is not available");
                return StatusCode(StatusCodes.Status500InternalServerError, "DB is unavailable");
            }
        }

    }
}
