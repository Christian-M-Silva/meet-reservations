using MediatR;
using MeetReservation.Controllers;
using MeetReservation.Exceptions;
using MeetReservation.Models.Commands;
using MeetReservation.Models.Entities;
using MeetReservation.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace TestMeetReservation
{
    public class MeetControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly MeetController _meetController;

        public MeetControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _meetController = new MeetController(_mediator.Object);
        }

        readonly RegisterMeetCommand meetCommand = new()
        {
            Responsible = "Christian",
            Room = 1,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(1)
        };

        [Fact]
        public async Task ListAllMeetFluxoCertoRetornarOk()
        {
            IEnumerable<MeetEntity> meetingsExpected =
            [
                new() { Id = Guid.NewGuid(), Responsible = "Christian", Room = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) },
                new () { Id =  Guid.NewGuid(), Responsible = "Thayná", Room = 2, StartTime = DateTime.Now.AddHours(1), EndTime = DateTime.Now.AddHours(2) }
            ];

            _mediator.Setup(m => m.Send(It.IsAny<ListAllMeetQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(meetingsExpected);

            ActionResult result = await _meetController.ListAllMeet();

            OkObjectResult meetings = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(meetingsExpected, meetings.Value);

            _mediator.Verify(m => m.Send(It.IsAny<ListAllMeetQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ListAllMeetFluxoErroGnericoRetornarBadRequest() 
        {
            _mediator.Setup(m => m.Send(It.IsAny<ListAllMeetQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Erro inesperado"));

               ActionResult result = await _meetController.ListAllMeet();
    
                Assert.IsType<BadRequestObjectResult>(result);
    
        }

        [Fact]
        public async Task RegisterMeetFluxoCertoRetornarOk()
        {
           

            _mediator.Setup(m => m.Send(meetCommand, It.IsAny<CancellationToken>()))
                .Verifiable();

            ActionResult result = await _meetController.RegisterMeet(meetCommand);

            Assert.IsType<OkObjectResult>(result);

            _mediator.Verify(m => m.Send(meetCommand, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RegisterMeetFluxoErroGnericoRetornarBadRequest()
        {
            _mediator.Setup(m => m.Send(meetCommand, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Erro inesperado"));

            ActionResult result = await _meetController.RegisterMeet(meetCommand);

            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task RegisterMeetFluxoErroSalaOcupadaRetornarBusyRoomException()
        {
            _mediator.Setup(m => m.Send(meetCommand, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new BusyRoomException());

            ActionResult result = await _meetController.RegisterMeet(meetCommand);

            ConflictObjectResult response = Assert.IsType<ConflictObjectResult>(result);

            Assert.Equal("Sala ocupada durante esse periodo, tente outra sala ou um outro horário", response.Value);
        }
    }
}
