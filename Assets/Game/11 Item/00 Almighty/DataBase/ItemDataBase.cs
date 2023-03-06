// 日本語対応
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase
{
    #region Singleton
    private static ItemDataBase _instance = new ItemDataBase();
    public static ItemDataBase Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError($"Error! Please correct!");
            }
            return _instance;
        }
    }
    private ItemDataBase(){}
    #endregion
}