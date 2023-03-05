// 日本語対応
using UnityEngine;

[DisallowMultipleComponent]
public class FirstDisableObj : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.SetActive(false);
    }
}
