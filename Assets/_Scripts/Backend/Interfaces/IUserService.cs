using System;
using _Scripts.Backend.Models;

namespace _Scripts.Backend.Interfaces
{
    public interface IUserService
    {
        User UserInfo { get; }

        bool HasUserInfo { get; }

        void Register(string username, string displayName, string password, Action<bool> callback);

        void Authenticate(Action<bool> callback);

        void ChangeDisplayName(string newDisplayName, Action<bool> callback);
    }
}
