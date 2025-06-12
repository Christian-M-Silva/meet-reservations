using MediatR;
using MeetReservation.Exceptions;
using MeetReservation.Models.Commands;
using MeetReservation.Models.Entities;
using MeetReservation.Models.Queries;
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
        public async Task<ActionResult> ListAllMeet()
        {
            try
            {
                IEnumerable<MeetEntity> meetings = await _mediator.Send(new ListAllMeetQuery());
                return Ok(meetings);
            }
            catch (Exception err)
            {
                return BadRequest($"Ocorreu um erro ineperado: {err.Message}");
            }
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
