using Estimate.Data;
using Estimate.Models;
using Estimate.Services;

using Microsoft.EntityFrameworkCore;

namespace EstimateTest
{
    public class AuthServiceTests
    {
        [Fact]
        public void Login_WithValidCredentials_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Auth")
            .Options;

            using(var context = new AppDbContext(options))
            {
                context.Users.Add(new AppUser { Login = "admin", Password = "123", Role = Role.Admin });
                context.SaveChanges();
            }

            var service = new AuthService(new AppDbContext(options));

            // Act
            bool result = service.Login("admin", "123");

            // Assert (RED)
            Assert.True(result); // Тест упадёт, если AuthService не реализован
        }

        [Theory]
        [InlineData("admin", "wrong", false)]
        [InlineData("unknown", "123", false)]
        public void Login_WithInvalidCredentials_ReturnsFalse(string login, string password, bool expected)
        {
            // Arrange (используем ту же InMemory-БД)
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Auth")
                .Options;

            using(var context = new AppDbContext(options))
            {
                context.Users.Add(new AppUser { Login = "admin", Password = "123", Role = Role.Admin });
                context.SaveChanges();
            }

            var service = new AuthService(new AppDbContext(options));

            // Act
            bool result = service.Login(login, password);

            // Assert
            Assert.Equal(expected, result);
            Assert.Equal(expected, service.CurrentUser != null);
        }

        [Theory]
        [InlineData("admin", "123", Role.Admin)]
        [InlineData("manager", "456", Role.Manager)]
        public void Login_SetsCorrectUserRole(string login, string password, Role role)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Auth_Roles")
                .Options;

            using(var context = new AppDbContext(options))
            {
                context.Users.Add(new AppUser { Login = login, Password = password, Role = role });
                context.SaveChanges();
            }

            var service = new AuthService(new AppDbContext(options));

            // Act
            service.Login(login, password);

            // Assert
            Assert.Equal(role, service.CurrentUser?.Role);
        }

    }
}
