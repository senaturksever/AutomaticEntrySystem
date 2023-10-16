using AutomaticEntrySystem.Dtos;
using AutomaticEntrySystem.Dtos.RegisterDto;
using AutomaticEntrySystem.Library;
using AutomaticEntrySystem.Manager;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.Common;
using Moq;
using NUnit.Framework;
using System.Data.SqlClient;

namespace EntrySystemTest
{
    [TestFixture]
    public class RegisterTests
    {


        [Test]
        public void SuccessfulTest()
        {
            var databaseWrapperMock = new Mock<DatabaseTest>();
            var jwtModelOptions = Options.Create(new JwtModel
            {
                Key = "mySecretKey",
                Issuer = "myIssuer",
                Audience = "myAudience"
            });
            var mobileManager = new MobileManager(jwtModelOptions);
            databaseWrapperMock.Setup(d => d.ExecuteNonQueryWithParameters(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
                              .Returns(1);

            var requestDto = new RegisterRequestDto
            {
                Email = "test@example.com",
                Password = "password",
                UserName = "testuser"
            };

            // Act
            var response = mobileManager.Register(requestDto);

            // Assert
            Assert.IsTrue(response.Status);
            Assert.AreEqual(1, response.statusCode);
            Assert.AreEqual("Kayýt iþlemi baþarýlý", response.StatusMessage);
            //{// Arrange
            //    var jwtModelOptions = Options.Create(new JwtModel
            //    {
            //        Key = "mySecretKey",
            //        Issuer = "myIssuer",
            //        Audience = "myAudience"
            //    });
            //    var mobileManager = new MobileManager(jwtModelOptions);

            //    var requestDto = new RegisterRequestDto
            //    {
            //        Email = "test@example.com",
            //        Password = "password",
            //        UserName = "testuser"
            //    };

            //    // Act
            //    var response = mobileManager.Register(requestDto);

            //    // Assert
            //    Assert.IsTrue(response.Status);
            //    Assert.AreEqual(1, response.statusCode);
            //    Assert.AreEqual("Kayýt iþlemi baþarýlý", response.StatusMessage);
        }



        [Test]
        public void UnsuccessfulTest()
        {

            var databaseWrapperMock = new Mock<DatabaseTest>();
            var jwtModelOptions = Options.Create(new JwtModel
            {
                Key = "mySecretKey",
                Issuer = "myIssuer",
                Audience = "myAudience"
            });
            var mobileManager = new MobileManager(jwtModelOptions);
            databaseWrapperMock.Setup(d => d.ExecuteNonQueryWithParameters(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
                              .Returns(0);

            var requestDto = new RegisterRequestDto
            {
                Email = "test@example.com",
                Password = "password",
                UserName = "testuser"
            };

            // Act
            var response = mobileManager.Register(requestDto);
            // Assert
            Assert.IsFalse(response.Status);
            Assert.AreEqual(0, response.statusCode);
            Assert.AreEqual("Tekrar Eden Kayýt", response.StatusMessage);
        }
    }
}