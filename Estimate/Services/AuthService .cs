using Estimate.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using Estimate.Models;

namespace Estimate.Services
{
    public interface IAuthService
    {
        AppUser? CurrentUser { get; }
        bool IsLoginValid(string login, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private int _count;

        public AppUser? CurrentUser { get; private set; }

        public AuthService(AppDbContext db) => _db = db;

        public bool IsLoginValid(string login, string password)
        {
            CurrentUser = _db.Users?.FirstOrDefault(u =>
                u.Login == login && u.Password == password);
            return !(CurrentUser == null);
        }
    }
}


