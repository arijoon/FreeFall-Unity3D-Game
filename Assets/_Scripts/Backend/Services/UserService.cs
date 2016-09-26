using System;
using UnityEngine;
using _Scripts.Backend.Interfaces;
using _Scripts.Backend.Models;
using _Scripts.Definitions.ConstantClasses;

namespace _Scripts.Backend.Services
{
    public class UserService : IUserService
    {
        public User UserInfo { get; private set; }

        public bool HasUserInfo { get { return PlayerPrefs.HasKey(SaveKeys.DisplayName); } }

        public void Register(string username, string displayName, string password, Action<bool> callback)
        {
           displayName = PlayerPrefs.GetString(SaveKeys.DisplayName);

#if DEBUG
            displayName = "TEST_" + displayName;
#endif
            new GameSparks.Api.Requests.RegistrationRequest()
                .SetDisplayName(displayName)
                .SetUserName(username)
                .SetPassword(password)
                .Send((res) =>
                    {
                        if(callback != null)
                            callback(!res.HasErrors);
                    });
        }

        public void Authenticate(Action<bool> callback)
        {
            string displayName = PlayerPrefs.GetString(SaveKeys.DisplayName);

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = "User" + new System.Random().NextDouble() * 1000000;
            }

#if DEBUG
            displayName = "TEST_" + displayName;
#endif

            new GameSparks.Api.Requests.DeviceAuthenticationRequest()
                .SetDisplayName(displayName)
                .Send((res) =>
                    {
                        if (callback != null)
                            callback(!res.HasErrors);
                    });
        }

        public void ChangeDisplayName(string newDisplayName,Action<bool> callback)
        {
            new GameSparks.Api.Requests.ChangeUserDetailsRequest()
                .SetDisplayName(newDisplayName)
                .Send((res) =>
                {
                    if (callback != null)
                        callback(!res.HasErrors);
                });
        }
    }
}