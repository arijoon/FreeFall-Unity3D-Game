using UnityEngine;

namespace _Scripts.Managers.Ui
{
    public class IncreaseHeight : MonoBehaviour
    {
        void Update()
        {
            RectTransform t =  GetComponent<RectTransform>();
            t.sizeDelta = new Vector2(0, t.rect.height +1);
        }
    }
}
