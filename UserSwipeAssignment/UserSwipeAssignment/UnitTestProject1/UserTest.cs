using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using UserSwipeAssignment;
using UserSwipeAssignment.Controllers;
using UserSwipeAssignment.Models;
using UserSwipeAssignment.UserRepo;
using UserSwipeAssignment.Utilities;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject1
{
    [TestFixture]
    public class UserTest
    {
        private readonly Mock<IUserRepository> _mockUserDAL;
        private readonly UserController _userController;
        private readonly Mock<TokenManager> _mocktmgr; 

        public UserTest()
        {
            _mockUserDAL = new Mock<IUserRepository>();
            _mocktmgr = new Mock<TokenManager>();
            _userController = new UserController(_mockUserDAL.Object, _mocktmgr.Object);
            _userController.Request = new HttpRequestMessage();
            _userController.Configuration = new HttpConfiguration();
            
        }

        [Test]
        public void GetUser()
        {
            _mockUserDAL.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(new UserDetail { UserName = "ajitPAW", UserId = 1234 });

            var resp = _userController.GetUser(1234);
            UserDetail user;
            resp.TryGetContentValue<UserDetail>(out user);

            Assert.AreEqual("ajitPAW", user.UserName);

        }

        [Test]
        public void UserLogin()
        {
            _mockUserDAL.Setup(x => x.GetUserByName(It.IsAny<UserModel>())).Returns(new UserDetail { UserName = "ajitPAW--", UserId = 1234, ActiveStatus=true });
            
            _mocktmgr.Setup(x => x.GenerateToken(It.IsAny<string>())).Returns("gasgsdagagagagaggda");
            
            UserModel m = new UserModel { UserName = "pratik", Password = "pratik@123" };
            var resp = _userController.UserLogin(m);
            string token;
            resp.TryGetContentValue<string>(out token);

            Assert.AreEqual("gasgsdagagagagaggda", token);

        }

        [Test]
        public void ValidateToken()
        {
            _mockUserDAL.Setup(x => x.GetUserByName(It.IsAny<UserModel>())).Returns(new UserDetail { UserName = "ajitPAW**", UserId = 1234, ActiveStatus = true });

            _mocktmgr.Setup(x => x.ValidateToken(It.IsAny<string>())).Returns("ajitPAW**");

            UserModel m = new UserModel { UserName = "pratik", Password = "pratik@123" };
            var resp = _userController.Validate("gasgsdagagagagaggda", "ajitPAW**");
            
            Assert.AreEqual(System.Net.HttpStatusCode.OK, resp.StatusCode);

        }
    }
}
