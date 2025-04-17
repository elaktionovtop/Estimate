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
    public class AuthServiceTests
    {
        [Fact]
        public void Login_WithValidCredentials_ReturnsTrue()
        {
            using var context = TestBase.CreateContext();
            context.Users.Add(new AppUser      
            {
                Login = "admin",
                Password = "123",
                Role = Role.Admin
            });
            context.SaveChanges();
            var service = new AuthService(context);
            bool result = service.IsLoginValid("admin", "123");

            Assert.Equal(true, result);
            Assert.NotNull(service.CurrentUser);
        }

        // желательно добавить еще одного пользователя
        [Theory]
        [InlineData("admin", "123", true)]
        [InlineData("admin", "wrong", false)]
        [InlineData("unqnonw", "123", false)]
        public void Login_WithInvalidCredentials_ReturnsFalse
            (string login, string password, bool expected)
        {
            using var context = TestBase.CreateContext();
            context.Users.Add(new AppUser
            {
                Login = "admin",
                Password = "123",
                Role = Role.Admin
            });
            context.SaveChanges();
            var service = new AuthService(context);
            bool result = service.IsLoginValid(login, password);

            Assert.Equal(expected, result);
            Assert.Equal(expected, service.CurrentUser != null);
        }

        [Theory]
        [InlineData("admin", "123", Role.Admin)]
        [InlineData("manager", "111", Role.Manager)]
        public void Login_SetsCorrectUserRole
            (string login, string password, Role role)
        {
            using var context = TestBase.CreateContext();
            context.Users.Add(new AppUser
            {
                Login = login,
                Password = password,
                Role = role
            });
            context.SaveChanges();
            var service = new AuthService(context);
            service.IsLoginValid(login, password);

            Assert.Equal(role, service.CurrentUser?.Role);
        }
    }
}
