using UnityEngine;
using Zenject;
using _Scripts.Definitions.Interfaces;

namespace _Scripts.Controllers
{
    public class PlatformController : MonoBehaviour
    {
        private IGameManager _gameManager;

        [Inject]
        public void Initiliaze(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }


    }
}
