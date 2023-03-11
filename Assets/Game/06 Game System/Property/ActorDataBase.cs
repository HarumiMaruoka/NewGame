// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ActorDataBase
{
    public ActorDataBase()
    {
        GenerateActor();
    }
    /// <summary> ゲームに登場するアクターの最大人数 </summary>
    public const int MaxActorID = 4;
    /// <summary> アクターデータが入力されたCsvファイルのアドレッサブル名 </summary>
    private readonly string _actorDataCsvAddressableName = "Actor Data";
    /// <summary> 各アクターの情報が格納された配列。添字にアクターIDを指定することで、アクターにアクセスできる。 </summary>
    public Actor[] Actors { get; private set; } = new Actor[MaxActorID];
    /// <summary> 各アクターの生成処理が完了したかどうかを表現する値 </summary>
    public bool IsGenerated { get; private set; } = false;

    /// <summary> アクターの生成処理 </summary>
    private async void GenerateActor()
    {
        if (!IsGenerated)
        {
            try
            {
                var loadData = await GetCsv(_actorDataCsvAddressableName);
                Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                SetActor(Actors, loadData, StringToActor);
                IsGenerated = true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.LogError("アクター生成に失敗しました！");
                IsGenerated = false;
            }
        }
    }
    /// <summary> 文字列をActorオブジェクトに変換する </summary>
    /// <param name="str"> 変換元となる文字列 </param>
    /// <returns> 生成されたアクターオブジェクト。変換に失敗したらnullを返す。 </returns>
    private Actor StringToActor(string[] str)
    {
        return new Actor(
            int.Parse(str[0]),    // ID
            str[1],               // 名前
            float.Parse(str[2])); // ライフの初期値
    }
    /// <summary> 指定された配列にアクターを割り当てる。 </summary>
    /// <param name="storageSite"> 代入する配列 </param>
    /// <param name="sauce"> 読み取り元の文字列 </param>
    /// <param name="converter"> 文字列をオブジェクトに変換する関数 </param>
    private void SetActor(Actor[] storageSite, StringReader sauce, Func<string[], Actor> converter)
    {
        sauce.ReadLine(); // 一行目はヘッダー行なので切り捨てる。
        for (int i = 0; i < storageSite.Length; i++)
        {
            // 格納場所    = 変換器   (入力を一行読み取り、カンマ区切りに加工した文字列を変換器に渡す。)
            storageSite[i] = converter(sauce.ReadLine().Split(','));
        }
    }
    /// <summary> csvファイルをStringReaderオブジェクトに変換する </summary>
    /// <param name="addressableName"></param>
    /// <returns></returns>
    private async UniTask<StringReader> GetCsv(string addressableName)
    {
        var asset = await Addressables.LoadAssetAsync<TextAsset>(addressableName);
        return new StringReader(asset.text);
    }
}
