using UnityEngine;
using UnityEngine.SceneManagement;
using _Scripts.Definitions.ConstantClasses;

namespace _Scripts.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        public GameObject MainMenuContainer;

        [Space(10)]
        public bool InitialiseStates;
        public GameObject MainMenu;
        public GameObject[] OtherMenus;

        void Start()
        {
            if(InitialiseStates)
                SetInitialStates();
        }

        public void LoadGame()
        {
            SceneManager.LoadScene(SceneIndexes.Main);
        }

        public void QuitGame()
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void ToggleVisibility()
        {
            MainMenuContainer.SetActive(!MainMenuContainer.activeSelf);
        }

        private void SetInitialStates()
        {
            MainMenu.SetActive(true);

            foreach (var menu in OtherMenus)
            {
                menu.SetActive(false);
            }
        }
    }
}
