// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class MealButton : MonoBehaviour
{
    public void Setup()
    {
        _nameText = transform.GetChild(0).gameObject.GetComponent<Text>();
        _numberText = transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    private Text _nameText = default;
    private Text _numberText = default;

    public Text NameText => _nameText;
    public Text NumberText => _numberText;

}