using api.Controllers;
using api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace api.tests.Unit
{
    public class SessionControllerTests
    {
        [Fact]
        public void Get_MissingSession_Returns404()
        {
            var sessionServiceMock = new Mock<ISessionService>();
            sessionServiceMock.Setup(s => s.Get(It.IsAny<int>())).Returns<Session>(null);

            var sessionsController = new SessionsController(sessionServiceMock.Object);
            var result = sessionsController.Get(0);

            result.Result.Should().BeOfType<NotFoundResult>();

        }
    }
}
