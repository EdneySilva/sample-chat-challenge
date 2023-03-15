using AppChat.Domain;
using AppChat.Domain.Abstractions;
using AppChat.Domain.Exceptions;
using MediatR;
using Moq;

namespace AppChat.Tests
{
    public class Rooms_OpenOrStartRooms_Should
    {
        [Fact]
        public async void Throws_UserNotFound_when_a_non_registered_user_has_been_informed()
        {
            var roomManagerMock = new Mock<IRoomManager>();
            var mediatorMock = new Mock<IMediator>();
            Rooms rooms = new Rooms(roomManagerMock.Object, mediatorMock.Object);

            await Assert.ThrowsAsync<UserNotFound>(() =>
            {
                return rooms.OpenOrStartRooms("notfounduser", string.Empty);
            });
        }

        [Fact]
        public async void Set_user_to_online_status()
        {
            var accounts = new List<Account>
                    {
                        new Account
                        {
                            UserName = "edney",
                        }
                    };
            var roomManagerMock = new Mock<IRoomManager>();
            roomManagerMock.Setup(p => p.AllUsersToChatWith(It.IsAny<string>()))
                .Returns((string userName) =>
                {
                    return Task.FromResult(accounts.AsEnumerable());
                });
            var mediatorMock = new Mock<IMediator>();
            Rooms rooms = new Rooms(roomManagerMock.Object, mediatorMock.Object);

            var result = await rooms.OpenOrStartRooms("edney", string.Empty);
            Assert.True(accounts.First().Status == ConnectionStatus.Online);
        }

        [Fact]
        public async void Set_create_a_room_between_two_users()
        {
            var accounts = new List<Account>
                    {
                        new Account
                        {
                            UserName = "edney",
                        },
                        new Account
                        {
                            UserName = "adriane",
                        }
                    };
            var roomManagerMock = new Mock<IRoomManager>();
            roomManagerMock.Setup(p => p.AllUsersToChatWith(It.IsAny<string>()))
                .Returns((string userName) =>
                {
                    return Task.FromResult(accounts.AsEnumerable());
                });
            var mediatorMock = new Mock<IMediator>();
            Rooms rooms = new Rooms(roomManagerMock.Object, mediatorMock.Object);

            var result = await rooms.OpenOrStartRooms("edney", string.Empty);
            Assert.True(accounts.First().Status == ConnectionStatus.Online);
            Assert.True(result.Count() > 0);
        }
    }
}