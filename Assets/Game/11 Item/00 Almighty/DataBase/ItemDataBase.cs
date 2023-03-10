// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;


/// <summary>
/// 各アイテムの情報を集積するところ
/// </summary>
public class ItemDataBase : ISavable
{
    #region Singleton
    private static ItemDataBase _instance = new ItemDataBase();
    public static ItemDataBase Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError($"Error! Please correct!");
            }
            return _instance;
        }
    }
    private ItemDataBase() { }
    #endregion

    private PlayerInventoryData _playerInventoryData = null;

    private Meal[] _meals = new Meal[MaxID_Meal];
    private Weapon[] _weapons = new Weapon[MaxID_Weapon];
    private Armor[] _armors = new Armor[MaxID_Armor];
    private Kitchenware[] _kitchenwares = new Kitchenware[MaxID_Kitchenware];
    private CookingIngredients[] _cookingIngredients = new CookingIngredients[MaxID_CookingIngredients];
    private Valuables[] _valuables = new Valuables[MaxID_Valuables];

    public PlayerInventoryData PlayerPossessionData => _playerInventoryData;

    public Meal[] Meals => _meals;
    public Weapon[] Weapons => _weapons;
    public Armor[] Armors => _armors;
    public Kitchenware[] Kitchenwares => _kitchenwares;
    public CookingIngredients[] CookingIngredients => _cookingIngredients;
    public Valuables[] Valuables => _valuables;


    /// <summary> 料理の最大ID </summary>
    public const int MaxID_Meal = 20;
    /// <summary> 武器の最大ID </summary>
    public const int MaxID_Weapon = 0;
    /// <summary> 防具の最大ID </summary>
    public const int MaxID_Armor = 0;
    /// <summary> 料理道具の最大ID </summary>
    public const int MaxID_Kitchenware = 0;
    /// <summary> 料理素材の最大ID </summary>
    public const int MaxID_CookingIngredients = 0;
    /// <summary> 貴重品の最大ID </summary>
    public const int MaxID_Valuables = 0;

    /// <summary> 料理データの格納しているcsvファイルのアドレッサブル名 </summary>
    private readonly string _mealCsvAddressableName = "Meal Data";
    /// <summary> 武器データの格納しているcsvファイルのアドレッサブル名 </summary>
    private readonly string _weaponCsvAddressableName = "";
    /// <summary> 防具データの格納しているcsvファイルのアドレッサブル名 </summary>
    private readonly string _armorCsvAddressableName = "";
    /// <summary> 料理道具データの格納しているcsvファイルのアドレッサブル名 </summary>
    private readonly string _kitchenwareCsvAddressableName = "";
    /// <summary> 料理素材データの格納しているcsvファイルのアドレッサブル名 </summary>
    private readonly string _cookingIngredientsCsvAddressableName = "";
    /// <summary> 貴重品データの格納しているcsvファイルのアドレッサブル名 </summary>
    private readonly string _valuablesCsvAddressableName = "";

    public bool IsLoaded { get; private set; } = false;

    /// <summary> アイテムデータを読み込む </summary>
    public async UniTask<bool> LoadItemData()
    {
        try
        {
            // 料理データを取得,設定する
            var loadData = await GetCsv(_mealCsvAddressableName);
            SetItem(_meals, loadData, ItemConverter.StringToMeal);
            //// 武器データを取得,設定する
            //loadData = await GetCsv(_weaponCsvAddressableName);
            //SetItem(_weapons, loadData, ItemConverter.StringToWeapon);
            //// 防具データを取得,設定する
            //loadData = await GetCsv(_armorCsvAddressableName);
            //SetItem(_armors, loadData, ItemConverter.StringToArmor);
            //// 料理道具データを取得,設定する
            //loadData = await GetCsv(_kitchenwareCsvAddressableName);
            //SetItem(_kitchenwares, loadData, ItemConverter.StringToKitchenware);
            //// 料理素材データを取得,設定する
            //loadData = await GetCsv(_cookingIngredientsCsvAddressableName);
            //SetItem(_cookingIngredients, loadData, ItemConverter.StringToCookingIngredients);
            //// 貴重品データを取得,設定する
            //loadData = await GetCsv(_valuablesCsvAddressableName);
            //SetItem(_valuables, loadData, ItemConverter.StringToValuables);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError("アイテムの読み込みに失敗しました。");
            return false;
        }
        IsLoaded = true;
        return true;
    }

    public void Save()
    {
        SaveManager.Save<PlayerInventoryData>(_playerInventoryData, "Inventory data", 0);
    }
    public void Load()
    {
        _playerInventoryData = SaveManager.Load<PlayerInventoryData>("Inventory data", 0);
        if (_playerInventoryData == null)
        {
            _playerInventoryData = new PlayerInventoryData();
            _playerInventoryData.Init();
        }
    }
    /// <summary> アイテムを割り当てる </summary>
    /// <param name="storageSite"> アイテムの格納場所 </param>
    /// <param name="sauce"> 入力（主にCSVファイル） </param>
    /// <param name="converter"> 文字をアイテムオブジェクトに変換する関数 </param>
    private void SetItem(Item[] storageSite, StringReader sauce, Func<string[], Item> converter)
    {
        sauce.ReadLine(); // 一行目はヘッダー行なので切り捨てる。
        for (int i = 0; i < storageSite.Length; i++)
        {
            // 格納場所    = 変換器   (入力を一行読み取り、カンマ区切りに加工した文字列を変換器に渡す。)
            storageSite[i] = converter(sauce.ReadLine().Split(','));
        }
    }
    // メモ : ラムダ式によるヒープアロケーションを避けるため、ローカル関数を使用する。 https://learn.microsoft.com/ja-jp/dotnet/csharp/programming-guide/classes-and-structs/local-functions#:~:text=%E3%83%AD%E3%83%BC%E3%82%AB%E3%83%AB%E9%96%A2%E6%95%B0%E3%81%A7%E3%81%AF%E3%80%81%E3%81%9D%E3%81%AE%E4%BD%BF%E7%94%A8%E3%81%AB%E5%BF%9C%E3%81%98%E3%81%A6%E3%80%81%E3%83%A9%E3%83%A0%E3%83%80%E5%BC%8F%E3%81%A7%E3%81%AF%E5%B8%B8%E3%81%AB%E5%BF%85%E8%A6%81%E3%81%AA%E3%83%92%E3%83%BC%E3%83%97%E3%81%AE%E5%89%B2%E3%82%8A%E5%BD%93%E3%81%A6%E3%82%92%E5%9B%9E%E9%81%BF%E3%81%A7%E3%81%8D%E3%81%BE%E3%81%99%E3%80%82%20%E3%83%AD%E3%83%BC%E3%82%AB%E3%83%AB%E9%96%A2%E6%95%B0%E3%81%8C%E3%83%87%E3%83%AA%E3%82%B2%E3%83%BC%E3%83%88%E3%81%AB%E5%A4%89%E6%8F%9B%E3%81%95%E3%82%8C%E3%81%A6%E3%81%8A%E3%82%89%E3%81%9A%E3%80%81%E3%83%AD%E3%83%BC%E3%82%AB%E3%83%AB%E9%96%A2%E6%95%B0%E3%81%A7%E3%82%AD%E3%83%A3%E3%83%97%E3%83%81%E3%83%A3%E3%81%95%E3%82%8C%E3%81%9F%E3%81%84%E3%81%9A%E3%82%8C%E3%81%AE%E5%A4%89%E6%95%B0%E3%82%82%E3%80%81%E3%83%87%E3%83%AA%E3%82%B2%E3%83%BC%E3%83%88%E3%81%AB%E5%A4%89%E6%8F%9B%E3%81%95%E3%82%8C%E3%81%9F%E4%BB%96%E3%81%AE%E3%83%A9%E3%83%A0%E3%83%80%E3%82%84%E3%83%AD%E3%83%BC%E3%82%AB%E3%83%AB%E9%96%A2%E6%95%B0%E3%81%A7%E3%82%AD%E3%83%A3%E3%83%97%E3%83%81%E3%83%A3%E3%81%95%E3%82%8C%E3%81%A6%E3%81%84%E3%81%AA%E3%81%84%E5%A0%B4%E5%90%88%E3%81%AF%E3%80%81%E3%82%B3%E3%83%B3%E3%83%91%E3%82%A4%E3%83%A9%E3%81%AB%E3%82%88%E3%81%A3%E3%81%A6%E3%83%92%E3%83%BC%E3%83%97%E3%81%AE%E5%89%B2%E3%82%8A%E5%BD%93%E3%81%A6%E3%82%92%E5%9B%9E%E9%81%BF%E3%81%A7%E3%81%8D%E3%81%BE%E3%81%99%E3%80%82
    private async UniTask<StringReader> GetCsv(string addressableName)
    {
        var asset = await Addressables.LoadAssetAsync<TextAsset>(addressableName);
        return new StringReader(asset.text);
    }

}