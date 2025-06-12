using MediatR;
using MeetReservation.Models.Entities;

namespace MeetReservation.Models.Queries
{
    public class ListAllMeetQuery: IRequest<IEnumerable<MeetEntity>>
    {
    }
}
