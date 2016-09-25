using System;
using _Scripts.Backend.Interfaces;
using _Scripts.Backend.Models;

namespace _Scripts.Backend.Services
{
    public class UserService : IUserService
    {
        public User UserInfo { get; private set; }

        public void Register(string username, Action<bool> callback)
        {
            throw new NotImplementedException();
        }

        public void Authenticate(Action<bool> callback)
        {
            throw new NotImplementedException();
        }
    }
}