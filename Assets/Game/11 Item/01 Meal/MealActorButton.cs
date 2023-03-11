// 日本語対応
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealActorButton : MonoBehaviour
{
    private ILife _owner = null;

    public ILife Owner => _owner;

    public void SetOwner(ILife owner)
    {
        _owner = owner;
    }
}
