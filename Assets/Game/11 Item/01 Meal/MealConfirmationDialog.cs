// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class MealConfirmationDialog : MonoBehaviour
{
    [SerializeField]
    private Text _name = null;

    private ILife _target = null;
    private int _id = -1;

    public void Setup(ILife target, int id)
    {
        _target = target; _id = id;
        _name.text = ItemDataBase.Instance.Meals[id].Name;
    }
    public void UseItem()
    {
        ItemDataBase.Instance.Meals[_id].Heal(_target);
    }
}
