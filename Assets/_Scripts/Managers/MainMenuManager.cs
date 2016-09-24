using UnityEngine;
using UnityEngine.SceneManagement;
using _Scripts.Definitions.ConstantClasses;

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
