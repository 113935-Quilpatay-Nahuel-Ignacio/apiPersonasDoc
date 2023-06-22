using ApiPersonasDoc.DTOs;
using ApiPersonasDoc.Exceptions;
using ApiPersonasDoc.Services.PersonaService.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ApiPersonasDoc.Services.PersonaService.Commands.CreatePersona;

namespace ApiPersonasDoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getPersonas")]
        public async Task<List<PersonaDTO>> GetPersonas()
        {
            var personas = await _mediator.Send(new GetPersonasQuery());
            return personas;
        }

        [HttpPost]
        [Route("crearPersona")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreatePersona(CreatePersonaCommand command)
        {
            string validationErrors = null;

            try
            {
                var respuesta = await _mediator.Send(command);
                return CreatedAtAction(nameof(CreatePersona), respuesta);
            }
            catch (BadRequestException ex)
            {
                if (ex.Data.Contains("ValidationErrors"))
                {
                    validationErrors = ex.Data["ValidationErrors"].ToString();
                }
                return BadRequest(validationErrors);
            }
        }
    }
}
