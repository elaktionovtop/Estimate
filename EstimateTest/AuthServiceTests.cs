using Estimate.Data;
using Estimate.Models;
using Estimate.Services;
using Estimate.ViewModels;
using Estimate.Views;

using Microsoft.EntityFrameworkCore;

using Moq;

using System.Windows;

namespace EstimateTest
{
    public class AuthServiceTests : TestBase
    {
        [Fact]
        public void Login_WithValidCredentials_ReturnsTrue()
        {
            using var context = CreateTestContext();
            var service = new AuthService(context);
            AuthReturn result = service.Login(AdminLogin, AdminPassword);

            Assert.Equal(AuthReturn.Success, result);
            Assert.NotNull(service.CurrentUser);
        }

        [Theory]
        [InlineData(AdminLogin, "wrong", AuthReturn.Error)]
        [InlineData("unknown", "123", AuthReturn.Error)]
        public void Login_WithInvalidCredentials_ReturnsFalse
            (string login, string password, AuthReturn expected)
        {
            using var context = CreateTestContext();
            var service = new AuthService(context);
            AuthReturn result = service.Login(login, password);

            Assert.Equal(expected, result);
            //Assert.Equal(expected, service.CurrentUser != null);
        }

        [Theory]
        [InlineData(AdminLogin, AdminPassword, AdminRole)]
        [InlineData(EmployeeLogin, EmployeePassword, EmployeeRole)]
        public void Login_SetsCorrectUserRole
            (string login, string password, Role role)
        {
            using var context = CreateTestContext();
            var service = new AuthService(context);
            service.Login(login, password);

            Assert.Equal(role, service.CurrentUser?.Role);
        }

        // запустить не удается без запуска App
        //[WpfFact]
        //public void CheckLogin_WithValidCredentials_ReturnsTrue()
        //{
        //    var mockAuthService = new Mock<IAuthService>();
        //    mockAuthService.Setup(dl => dl.Login(string.Empty, string.Empty))
        //        .Returns(true);

        //    var viewModal = new LoginViewModel(mockAuthService.Object);
        //    new LoginWindow().Show();
        //    viewModal.CheckLoginCommand.Execute(new System.Windows.Controls.PasswordBox());

        //    Assert.Single(Application.Current.Windows);
        //    Assert.Equal("MainWindow", Application.Current.Windows[0].Title);
        //}
    }
}
