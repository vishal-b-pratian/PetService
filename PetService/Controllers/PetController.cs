using Microsoft.AspNetCore.Mvc;
using PetService.Domain.ApiModel;
using PetService.Domain.Managers;

namespace PetService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetManager _petManager;
        private readonly IPetOwnerManager _petOwnerManager;
        private readonly ILogger<PetController> _logger;

        public PetController(IPetManager petManager, IPetOwnerManager petOwnerManager, ILogger<PetController> logger)
        {
            _petManager = petManager;
            _petOwnerManager = petOwnerManager;
            _logger = logger;
        }

        [HttpGet("pet")]
        [Produces(typeof(IEnumerable<PetApiModel>))]
        public async Task<ActionResult<IEnumerable<PetApiModel>>> GetAsync()
        {
            try
            {
                return new ObjectResult(await _petManager.GetAllPetsAsync());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, ex);
            }
        }
        [HttpGet("pet/{id}")]
        [Produces(typeof(List<PetApiModel>))]
        public async Task<ActionResult<PetApiModel>> GetAsync(int id)
        {
            try
            {
                var pet = await _petManager.GetPetByIdAsync(id);
                if (pet == null)
                {
                    return NotFound();
                }

                return Ok(pet);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPost("pet")]
        public async Task<ActionResult<PetApiModel>> PostAsync([FromBody] PetApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _petManager.AddPetAsync(input));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPut("pet/{id}")]
        public async Task<ActionResult<PetApiModel>> PutAsync(int id, [FromBody] PetApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _petManager.GetPetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                if (await _petManager.UpdatePetAsync(input))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpDelete("pet/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                if (await _petManager.GetPetByIdAsync(id) == null)
                {
                    return NotFound();
                }

                if (await _petManager.DeletePetAsync(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        //PetOwner Controller
        [HttpGet("PetOwner")]
        [Produces(typeof(List<PetOwnerApiModel>))]
        public async Task<ActionResult<List<PetOwnerApiModel>>> GetAsunc()
        {
            try
            {
                return new ObjectResult(await _petOwnerManager.GetAllPetOwnersAsync());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, ex);
            }
        }
        [HttpGet("PetOwner/{id}")]
        [Produces(typeof(List<PetOwnerApiModel>))]
        public async Task<ActionResult<PetOwnerApiModel>> GetOwnerByIdAsync(int id)
        {
            try
            {
                var appointment = await _petOwnerManager.GetPetOwnerByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPost("PetOwner")]
        public async Task<ActionResult<PetOwnerApiModel>> PostAsync([FromBody] PetOwnerApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _petOwnerManager.AddPetOwnerAsync(input));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPut("PetOwner/{id}")]
        public async Task<ActionResult<PetOwnerApiModel>> PutAsync(int id, [FromBody] PetOwnerApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _petOwnerManager.GetPetOwnerByIdAsync(id) == null)
                {
                    return NotFound();
                }
                if (await _petOwnerManager.UpdatePetOwnerAsync(input))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpDelete("PetOwner/{id}")]
        public async Task<ActionResult> DeletePetOwnerAsync(int id)
        {
            try
            {
                if (await _petOwnerManager.GetPetOwnerByIdAsync(id) == null)
                {
                    return NotFound();
                }

                if (await _petOwnerManager.DeletePetOwnerAsync(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }
    }
}

       