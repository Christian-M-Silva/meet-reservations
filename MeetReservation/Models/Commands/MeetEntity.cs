using MeetReservation.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MeetReservation.Models.Commands
{
    public class MeetEntity: BaseEntity
    {
        [Required(ErrorMessage = "O campo responsável é obrigatório")]
        public string Responsible { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Sala é obrigatório")]
        public int Room { get; set; }
        
        [Required(ErrorMessage = "O campo Tempo inicial é obrigatório")]
        public DateTime StartTime { get; set; }
        
        [Required(ErrorMessage = "O campo Tempo final é obrigatório")]
        public DateTime EndTime { get; set; }
    }
}
