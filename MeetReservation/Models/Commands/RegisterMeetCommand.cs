using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MeetReservation.Models.Commands
{
    public class RegisterMeetCommand: IRequest
    {
        public string Responsible { get; set; } = string.Empty;
        public int Room { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
