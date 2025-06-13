using MeetReservation.Exceptions;
using MeetReservation.Models.Commands;
using MeetReservation.Models.Entities;
using MeetReservation.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetReservation.Repository
{
    public class MeetRepository(MyDbContext myDbContext) : IMeetRepository
    {
        private readonly MyDbContext _myDbContext = myDbContext;

        public async Task AddMeet(RegisterMeetCommand meet)
        {
            try
            {
                bool isConflictingMetting = await _myDbContext.Meetings.AnyAsync(x => x.Room == meet.Room && 
                    ((x.StartTime >= meet.StartTime && x.StartTime <= meet.EndTime) || 
                     (x.EndTime > meet.StartTime && x.EndTime <= meet.EndTime)));

                if (isConflictingMetting) throw new BusyRoomException();

                await _myDbContext.Meetings.AddAsync(new MeetEntity
                {
                    Responsible = meet.Responsible,
                    Room = meet.Room,
                    StartTime = meet.StartTime,
                    EndTime = meet.EndTime,
                });

                await _myDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<MeetEntity>> ListMeet()
        {
            try
            {
                IEnumerable<MeetEntity> meetings = await _myDbContext.Meetings.ToListAsync();

                return meetings;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
