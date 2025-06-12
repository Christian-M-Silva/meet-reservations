using MediatR;
using MeetReservation.Exceptions;
using MeetReservation.Models.Commands;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        // GET: api/<MeetController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<MeetController>
        [HttpPost]
        public async Task<ActionResult> RegisterMeet(RegisterMeetCommand meet)
        {
            try
            {
                 await _mediator.Send(meet);
                return Ok("Reunião agendada com sucesso");
            }
            catch (BusyRoomException)
            {
                return Conflict("Sala ocupada durante esse periodo, tente outra sala ou um outro horário");
            }
            catch (Exception err)
            {
                return BadRequest($"Ocorreu um erro ineperado: {err.Message}");
            }
        }
    }
}
