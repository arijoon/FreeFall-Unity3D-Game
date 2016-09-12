using System.IO;
using System.Text;
using UnityEngine;

namespace _Scripts.Services
{
    public class AssetsReader
    {
        public string ReadAsset(string path)
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, path);

            if (fullPath.Contains("://"))
            {
                WWW www = new WWW(fullPath);

                while (!www.isDone)
                {
                }

                return www.text;
            }
            else
            {
                return File.ReadAllText(fullPath);
            }
        }

        public void WriteAsset(string path, string content)
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, path);

            if (fullPath.Contains("://"))
            {
                Debug.LogError("[!] Cannot write to streaming assets on Android");
            }
            else
            {
                File.WriteAllText(fullPath, content, Encoding.ASCII);
            }
        }
    }
}
