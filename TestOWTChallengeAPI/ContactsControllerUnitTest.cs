using OWTChallenge.API.Controllers;
using Xunit;
using Moq;
using OWTChallenge.Core.Interfaces;
using OWTChallenge.Infrastructure.Data;
using OWTChallenge.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestOWTChallengeAPI
{
    public class ContactsControllerUnitTest
    {
        [Fact]
        public async void ShouldReturnListOfContacts()
        {
            //ARRANG
            var mockUoW = new Mock<IUnitOfWork>();
            var controller = new ContactsController(mockUoW.Object);

            //ACT
            var data = await controller.GetContacts();

            //ASSERT
            Assert.NotNull(data);
        }
    }
}