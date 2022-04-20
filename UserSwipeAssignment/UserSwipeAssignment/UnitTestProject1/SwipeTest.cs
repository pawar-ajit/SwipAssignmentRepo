using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using UserSwipeAssignment;
using UserSwipeAssignment.Controllers;
using UserSwipeAssignment.Models;
using UserSwipeAssignment.Repo.SwipeRepo;
using UserSwipeAssignment.UserRepo;
using UserSwipeAssignment.Utilities;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject1
{
    [TestFixture]
    public class SwipeTest
    {
        private readonly Mock<IUserRepository> _mockUserDAL;
        private readonly Mock<ISwipeRepo> _swipeRepo;
        private readonly SwipeController _swipeController;
        private readonly Mock<TokenManager> _mocktmgr; 

        public SwipeTest()
        {
            _mockUserDAL = new Mock<IUserRepository>();
            _mocktmgr = new Mock<TokenManager>();
            _swipeRepo = new Mock<ISwipeRepo>();
            _swipeController = new SwipeController(_mockUserDAL.Object, _mocktmgr.Object, _swipeRepo.Object);
            _swipeController.Request = new HttpRequestMessage();
            _swipeController.Configuration = new HttpConfiguration();
            
        }

        [Test]
        public void SwipeInTest()
        {
            _mockUserDAL.SetupSequence(x => x.GetUserById(It.IsAny<int>()))
                .Returns((UserDetail)null)
                 .Returns(new UserDetail { UserName = "ajitPAW222", UserId = 1234, ActiveStatus = true })
                .Returns(new UserDetail { UserName = "ajitPAW", UserId = 1234, ActiveStatus = true });

            _mocktmgr.Setup(x => x.ValidateToken(It.IsAny<string>())).Returns("ajitPAW");
            _swipeRepo.Setup(x => x.DoSwipeIn(It.IsAny<SwipeInUserDetails>())).Returns(HttpStatusCode.OK);
           
            var details = new SwipeInUserDetails { UserId = 222, date = DateTime.Now, token = "fasfasfasfasfa" };

            var resp1 = _swipeController.SwipeIn(details);
            var resp2 = _swipeController.SwipeIn(details);
            var resp3 = _swipeController.SwipeIn(details);

            string result1;
            resp1.TryGetContentValue<string>(out result1);

            string result2;
            resp2.TryGetContentValue<string>(out result2);

            Assert.AreEqual("Invalid User!!!", result1);
            Assert.AreEqual("Invalid Token!!!", result2);
            Assert.AreEqual(HttpStatusCode.OK, resp3.StatusCode);
        }
        
    }
}
