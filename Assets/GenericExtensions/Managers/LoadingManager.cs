using GenericExtensions.Interfaces;
using UnityEngine;

namespace GenericExtensions.Managers
{
    public class LoadingManager : MonoBehaviour, ILoader
    {
        public bool State { get; private set; }

        private int _loadingStack;

        void Start()
        {
            _loadingStack = 0;

            gameObject.SetActive(false);
        }

        public void Loading(bool state)
        {
            _loadingStack += state ? 1 : -1;

            State = _loadingStack > 0;

            gameObject.SetActive(State);

            if (!State) _loadingStack = 0;
        }

        public void Flush()
        {
            _loadingStack = 1;

            Loading(false);
        }
    }
}
