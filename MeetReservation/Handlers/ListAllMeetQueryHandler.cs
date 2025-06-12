using MediatR;
using MeetReservation.Models.Entities;
using MeetReservation.Models.Queries;
using MeetReservation.Repository.Interfaces;

namespace MeetReservation.Handlers
{
    public class ListAllMeetQueryHandler(IMeetRepository meetRepository) : IRequestHandler<ListAllMeetQuery, IEnumerable<MeetEntity>>
    {
        private readonly IMeetRepository _meetRepository = meetRepository;
        public async Task<IEnumerable<MeetEntity>> Handle(ListAllMeetQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<MeetEntity> meetings = await _meetRepository.ListMeet();

            return meetings;
        }
    }
}
