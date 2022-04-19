using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using UserSwipeAssignment;
using UserSwipeAssignment.Controllers;
using UserSwipeAssignment.DAL;
using UserSwipeAssignment.Models;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject1
{
    [TestFixture]
    public class UserTest
    {
        private readonly Mock<UsersDAL> _mockUserDAL;
        private readonly UserController _userController;

        public UserTest()
        {
            _mockUserDAL = new Mock<UsersDAL>();
            _userController = new UserController(_mockUserDAL.Object);
        }

        [Test]
        public void GetUser()
        {
            _userController.Request = new HttpRequestMessage();
            _userController.Configuration = new HttpConfiguration();

            _mockUserDAL.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(new UserDetail { UserName = "ajitPAW", UserId = 1234 });

            var resp = _userController.GetUser(1234);
            UserDetail user;
            resp.TryGetContentValue<UserDetail>(out user);

            Assert.AreEqual("ajitPAW", user.UserName);
            
        }
    }
}
