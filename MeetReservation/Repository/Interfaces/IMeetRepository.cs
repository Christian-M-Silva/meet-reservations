using MeetReservation.Models.Commands;
using MeetReservation.Models.Entities;

namespace MeetReservation.Repository.Interfaces
{
    public interface IMeetRepository
    {
        public Task AddMeet(RegisterMeetCommand meet);
        public Task<IEnumerable<MeetEntity>> ListMeet();
    }
}
