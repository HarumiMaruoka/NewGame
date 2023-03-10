// 日本語対応
using System;
using UnityEngine.UI;

[Serializable]
public class Meal : Item
{
    public Meal(int id, string name, string explanatoryText, float recoveryAmount, int mealSpecialEffects) :
        base(id, name, explanatoryText)
    {
        _recoveryAmount = recoveryAmount;
        _mealSpecialEffects = (MealSpecialEffects)mealSpecialEffects;
    }
    public override ItemType Type => ItemType.Meal;

    public override void OnButtonClick()
    {
        throw new NotImplementedException();
    }

    private readonly float _recoveryAmount = 0f;
    private readonly MealSpecialEffects _mealSpecialEffects = MealSpecialEffects.None;

    /// <summary> 回復量 </summary>
    public float RecoveryAmount => _recoveryAmount;
    /// <summary> 特殊効果 </summary>
    public MealSpecialEffects AealSpecialEffects => _mealSpecialEffects;

    /// <summary>
    /// この料理を使用したときの特殊効果
    /// </summary>
    [Serializable, Flags]
    public enum MealSpecialEffects
    {
        None = 0,
        Everything = -1,
        IncreaseAttackPower = 1, // 攻撃力上昇
        DefenseIncrease = 2,     // 防御力上昇
        QuickRise = 4,           // 素早さ上昇
    }
}