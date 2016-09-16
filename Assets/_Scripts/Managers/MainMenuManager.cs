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
        public GameObject LevelButtonPrefab;

        public GameObject LevelButtonContainer;

        private PrefabFactory _prefabFactory;

        [Inject]
        public void Initialize(AssetsReader assetsReader, PrefabFactory prefabFactory)
        {

#if DEBUG
            AddDebug();
#endif
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


        private void LoadLevel(int level)
        {
            SceneManager.LoadScene(SceneIndexes.Main);
        }

        private void AddDebug()
        {
            var deletePrefBtn = _prefabFactory.Create(LevelButtonPrefab, Vector3.zero, Quaternion.identity);

            Text levelText = deletePrefBtn.GetComponentInChildren<Text>();

            levelText.text = string.Format("Delete Prefs");

            deletePrefBtn.GetComponent<Button>().onClick.AddListener(PlayerPrefs.DeleteAll);

            deletePrefBtn.transform.SetParent(LevelButtonContainer.transform, false);
            deletePrefBtn.transform.SetAsFirstSibling();
        }
    }
}
