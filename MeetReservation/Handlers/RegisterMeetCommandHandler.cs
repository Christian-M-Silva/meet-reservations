using MediatR;
using MeetReservation.Models.Commands;
using MeetReservation.Repository.Interfaces;

namespace MeetReservation.Handlers
{
    public class RegisterMeetCommandHandler(IMeetRepository meetRepository) : IRequestHandler<RegisterMeetCommand>
    {
        private readonly IMeetRepository _meetRepository = meetRepository;

        public async Task Handle(RegisterMeetCommand request, CancellationToken cancellationToken)
        {
            await _meetRepository.AddMeet(request);
        }
    }
}
