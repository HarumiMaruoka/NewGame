// 日本語対応
using System;
using UnityEngine;

/// <summary>
/// プレイヤーが所持している物のデータ
/// </summary>
[Serializable]
public class PlayerPossessionData
{
    /// <summary> いくつ武器を所持できるか を表す値 </summary>
    public const int MaxWeaponPossessionNumber = 20;
    /// <summary> いくつ防具を所持できるか を表す値 </summary>
    public const int MaxArmorPossessionNumber = 20;

    private int[] _mealData = new int[ItemDataBase.MaxID_Meal];
    private int[] _weaponData = new int[MaxWeaponPossessionNumber];
    private int[] _armorData = new int[MaxArmorPossessionNumber];
    private bool[] _kitchenwareData = new bool[ItemDataBase.MaxID_Kitchenware];
    private int[] _cookingIngredientsData = new int[ItemDataBase.MaxID_CookingIngredients];
    private bool[] _valuablesData = new bool[ItemDataBase.MaxID_Valuables];

    /// <summary> 食事の所持数。添字にIDを指定することで、そのIDの料理の所持数が取得できる。 </summary>
    public int[] MealData => _mealData;
    /// <summary> 所持している武器の状況。添字に格納場所を指定することで格納している武器のIDを取得できる。 </summary>
    public int[] WeaponData => _weaponData;
    /// <summary> 所持している防具の状況。添字に格納場所を指定することで格納している防具のIDを取得できる。 </summary>
    public int[] ArmorData => _armorData;
    /// <summary> 調理器具の所持状況。添字に調理器具のIDを指定することで、その調理器具を所持しているかどうか を表す値を取得できる。 </summary>
    public bool[] KitchenwareData => _kitchenwareData;
    /// <summary> 料理素材の所持数。添字にIDを指定する事で、そのIDの素材の所持数が取得できる。 </summary>
    public int[] CookingIngredientsData => _cookingIngredientsData;
    /// <summary> 貴重品の所持状況。添字に貴重品のIDを指定することで、その貴重品を所持しているかどうか を表す値を取得できる。 </summary>
    public bool[] ValuablesData => _valuablesData;

    /// <summary> 指定された料理の所持数を変更する </summary>
    /// <param name="id"> 料理のID </param>
    /// <param name="value"> 変更する量 </param>
    public void ChangeMealPossessionData(int id, int value)
    {
        _mealData[id] += value;
    }
    /// <summary> 
    /// 指定されたインデックスに補完されている武器のIDを取得する。
    /// 取得後、そのインデックスは -1が格納される。
    /// </summary>
    /// <param name="index"> 武器の格納場所を表すインデックス </param>
    /// <returns> そのインデックスに格納されている武器のID </returns>
    public int PullWeaponPossessionData(int index)
    {
        Debug.LogWarning("未実装");
        return -1; // 未実装のため、ダミーを返す。
    }
    /// <summary> 指定されたインデックスに武器のIDを格納する。 </summary>
    /// <param name="index"> 武器の格納場所を表すインデックス。 </param>
    /// <param name="id"> 格納する武器のID。 </param>
    /// <returns> 指定されたインデックスに既に武器が格納されていた場合。falseを返す。 </returns>
    public bool PushWeaponPossessionData(int index, int id)
    {
        Debug.LogWarning("未実装");
        return true; // 未実装のため、ダミーを返す。
    }
    /// <summary> 
    /// 指定されたインデックスに補完されている防具のIDを取得する。
    /// 取得後、そのインデックスは -1が格納される。
    /// </summary>
    /// <param name="index"> 防具の格納場所を表すインデックス。 </param>
    /// <returns>　そのインデックスに格納されている防具のID。　</returns>
    public int PullArmorPossessionData(int index)
    {
        Debug.LogWarning("未実装");
        return -1; // 未実装のため、ダミーを返す。
    }
    /// <summary> 指定されたインデックスに防具のIDを格納する。 </summary>
    /// <param name="index"> 防具の格納場所を表すインデックス </param>
    /// <param name="id"> 格納する防具のID </param>
    /// <returns> 指定されたインデックスに既に防具が格納されていた場合。falseを返す。 </returns>
    public bool PushArmorPossessionData(int index, int id)
    {
        Debug.LogWarning("未実装");
        return true; // 未実装のため、ダミーを返す。
    }
    /// <summary> 指定されたIDの調理器具を取得する。 </summary>
    /// <param name="id"> 取得する調理器具のID </param>
    public void GetKitchenwareData(int id)
    {
        try
        {
            _kitchenwareData[id] = true;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogWarning(e.Message);
            Debug.LogWarning($"配列の範囲外が指定されました！修正してください！\n" +
                $"指定されたID : {id}");
        }
    }
    /// <summary> 指定されたIDの調理器具を喪失する。 </summary>
    /// <param name="id"> 取得する調理器具のID </param>
    public void LostKitchenwareData(int id)
    {
        try
        {
            _kitchenwareData[id] = false;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogWarning(e.Message);
            Debug.LogWarning($"配列の範囲外が指定されました！修正してください！\n" +
                $"指定されたID : {id}");
        }
    }
    /// <summary> 指定された料理素材の所持数を変更する </summary>
    /// <param name="id"> 料理素材のID </param>
    /// <param name="value"> 変更する量 </param>
    public void ChangeCookingIngredientsPossessionData(int id, int value)
    {
        _cookingIngredientsData[id] += value;
    }

    /// <summary> 指定されたIDの貴重品を取得する。 </summary>
    /// <param name="id"> 取得する貴重品のID </param>
    public void GetValuablesData(int id)
    {
        try
        {
            _valuablesData[id] = true;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogWarning(e.Message);
            Debug.LogWarning($"配列の範囲外が指定されました！修正してください！\n" +
                $"指定されたID : {id}");
        }
    }
    /// <summary> 指定されたIDの貴重品を喪失する。 </summary>
    /// <param name="id"> 取得する貴重品のID </param>
    public void LostValuablesData(int id)
    {
        try
        {
            _valuablesData[id] = false;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogWarning(e.Message);
            Debug.LogWarning($"配列の範囲外が指定されました！修正してください！\n" +
                $"指定されたID : {id}");
        }
    }
}