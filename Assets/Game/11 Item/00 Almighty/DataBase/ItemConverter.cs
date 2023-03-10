// 日本語対応
using System;
using UnityEngine;

public static class ItemConverter
{
    /// <summary> 複数の文字列を一つのMeal型のオブジェクトに変換する。 </summary>
    /// <param name="str"> カンマ区切りの一行の文字列 </param>
    /// <returns> Meal型のオブジェクトへの参照 </returns>
    public static Meal StringToMeal(string[] str)
    {
        try
        {
            return new Meal(
                int.Parse(str[0]),
                str[1],
                str[2],
                float.Parse(str[3]),
                int.Parse(str[4]));
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError("食事アイテムの生成に失敗しました");
            throw new Exception();
        }
    }
    /// <summary> 複数の文字列を一つのWeapon型のオブジェクトに変換する。 </summary>
    /// <param name="str"> カンマ区切りの一行の文字列 </param>
    /// <returns> Weapon型のオブジェクトへの参照 </returns>
    public static Weapon StringToWeapon(string[] str)
    {
        Debug.LogError("未実装");
        return null;
    }
    /// <summary> 複数の文字列を一つのArmor型のオブジェクトに変換する。 </summary>
    /// <param name="str"> カンマ区切りの一行の文字列 </param>
    /// <returns> Armor型のオブジェクトへの参照 </returns>
    public static Armor StringToArmor(string[] str)
    {
        Debug.LogError("未実装");
        return null;
    }
    /// <summary> 複数の文字列を一つのKitchenware型のオブジェクトに変換する。 </summary>
    /// <param name="str"> カンマ区切りの一行の文字列 </param>
    /// <returns> Kitchenware型のオブジェクトへの参照 </returns>
    public static Kitchenware StringToKitchenware(string[] str)
    {
        Debug.LogError("未実装");
        return null;
    }
    /// <summary> 複数の文字列を一つのCookingIngredients型のオブジェクトに変換する。 </summary>
    /// <param name="str"> カンマ区切りの一行の文字列 </param>
    /// <returns> CookingIngredients型のオブジェクトへの参照 </returns>
    public static CookingIngredients StringToCookingIngredients(string[] str)
    {
        Debug.LogError("未実装");
        return null;
    }
    /// <summary> 複数の文字列を一つのValuables型のオブジェクトに変換する。 </summary>
    /// <param name="str"> カンマ区切りの一行の文字列 </param>
    /// <returns> Valuables型のオブジェクトへの参照 </returns>
    public static Valuables StringToValuables(string[] str)
    {
        Debug.LogError("未実装");
        return null;
    }
}
