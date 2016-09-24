using GenericExtensions.Factories;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Services;

namespace _Scripts.Managers
{
    public class MainMenuManager : MonoBehaviour
    {

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

    }
}
