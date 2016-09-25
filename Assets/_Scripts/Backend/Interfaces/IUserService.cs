using System;
using _Scripts.Backend.Models;

namespace _Scripts.Backend.Interfaces
{
    public interface IUserService
    {
        User UserInfo { get; }

        void Register(string username, Action<bool> callback);

        void Authenticate(Action<bool> callback);
    }
}
