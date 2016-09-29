using UnityEngine;
using Zenject;
using _Scripts.Backend.Interfaces;
using _Scripts.Definitions.ConstantClasses.ThirdParty;
using _Scripts.Services.Interfaces;

namespace _Scripts.Services
{
    public class PersistentService : MonoBehaviour, IPersistentServices
    {
        public static IPersistentServices Instance { get; private set; }

        public ILeaderBoard LeaderBoard { get; private set; }
        public IUserService UserService { get; private set; }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        [Inject]
        public void Initialize(ILeaderBoard leaderBoard, IUserService userService)
        {
            LeaderBoard = leaderBoard;
            UserService = userService;

            ConfigureBackend();

            Invoke("DelayedStart", .1f);
        }

        void DelayedStart()
        {
            UserService.Authenticate(null);
        }

        private void ConfigureBackend()
        {
            GameSparksSettings.ApiKey = GameSparksCodes.Credentials.Key;
            GameSparksSettings.ApiSecret = GameSparksCodes.Credentials.Secret;
        }

    }
}
