// 日本語対応
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SaveManager
{
    private event Action OnSave = default;
    private event Action OnLoad = default;

    /// <summary>
    /// セーブ、ロードの登録処理
    /// </summary>
    /// <param name="pausable"> 登録するオブジェクト </param>
    public void Register(ISavable savable)
    {
        OnSave += savable.Save;
        OnLoad += savable.Load;
    }
    /// <summary>
    /// セーブ、ロードの解除処理
    /// </summary>
    /// <param name="pausable"> 登録済みのオブジェクト </param>
    public void Lift(ISavable savable)
    {
        OnSave -= savable.Save;
        OnLoad -= savable.Load;
    }

    /// <summary>
    /// セーブの実行処理
    /// </summary>
    public void ExecuteSave(Action onSave = null)
    {
        OnSave?.Invoke();
        onSave?.Invoke();
    }
    /// <summary>
    /// ロードの実行処理
    /// </summary>
    public void ExecuteLoad(Action onLoad)
    {
        OnLoad?.Invoke();
        onLoad?.Invoke();
    }

    // 未解決課題 : IVとKeyをどうするか
    // AES設定値
    // =============================================== //
    private static int AesKeySize = 128;               // 鍵のサイズを指定する
    private static int AesBlockSize = 128;             // 一つのブロックのサイズ
    private static string AesIv = "6KGhH66PeU3cSLS7";  // 初期化ベクトル（Initialization Vectorの略称）
    private static string AesKey = "R38FYEzPyjxv0HrE"; // 鍵
    // =============================================== //

    public static void Save<T>(T targetObj, string fileName, int saveId)
    {
        // ファイルへのパスを取得
        var path = Application.persistentDataPath + "\\" + fileName + saveId + ".json";
        // 保存するデータを作成
        var saveData = JsonUtility.ToJson(targetObj);
        // ジェイソンをUTF8形式の文字列をエンコード（変換）し、そのバイト列を取得する。
        var jsonByte = Encoding.UTF8.GetBytes(saveData);
        // 指定のバイト列を暗号化したものを取得する。
        jsonByte = AesEncrypt(jsonByte);

        // データをファイルに書き込む
        File.WriteAllBytes(path, jsonByte);
    }
    public static T Load<T>(string fileName, int saveId)
    {
        // ファイルへのパスを取得する
        var path = Application.persistentDataPath + "\\" + fileName + saveId + ".json";
        // ファイルを読み込む
        var byteData = File.ReadAllBytes(path);
        // 復号化する
        byteData = AesDecrypt(byteData);
        // バイト列をstring型にエンコード（変換）する
        var json = Encoding.UTF8.GetString(byteData);
        // json形式の文字列をT型に戻し、結果を返す。
        return JsonUtility.FromJson<T>(json);
    }
    /// <summary> AES暗号化 </summary>
    /// <param name="byteText"> 暗号化するテキスト(バイト列) </param>
    /// <returns> 暗号化した結果のバイト列 </returns>
    private static byte[] AesEncrypt(byte[] byteText)
    {
        // AESマネージャー取得
        var aes = GetAesManager(AesKeySize, AesBlockSize, AesIv, AesKey);
        // 暗号化処理を実行し、結果を返す。
        //     結果        = AESマネージャー.対称暗号化オブジェクト.暗号化処理(暗号化対象,0,大きさ)
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// <summary> AES復号化 </summary>
    /// <param name="byteText"> 復号化するテキスト(バイト列) </param>
    /// <returns> 復号化した結果のバイト列 </returns>
    private static byte[] AesDecrypt(byte[] byteText)
    {
        // AESマネージャー取得
        var aes = GetAesManager(AesKeySize, AesBlockSize, AesIv, AesKey);
        // 復号化処理を実行し、結果を返す。
        //     結果        = AESマネージャー.対称復号化オブジェクト.復号化処理(復号化対象,0,大きさ)
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

    /// <summary> セットアップ済みのAesManaged型のオブジェクトを取得 </summary>
    /// <param name="keySize">   暗号化鍵の長さ                                      </param>
    /// <param name="blockSize"> ブロックサイズ                                      </param>
    /// <param name="iv">        初期化ベクトル(半角x文字（8bit * x = [keySize]bit)) </param>
    /// <param name="key">       暗号化鍵　　　(半角x文字（8bit * x = [keySize]bit)) </param>
    private static AesManaged GetAesManager(int keySize, int blockSize, string iv, string key)
    {
        AesManaged aes = new AesManaged();
        aes.KeySize = keySize;                 // 鍵のサイズを指定する（ビット単位）。 この数値が大きければ
                                               // 大きいほど暗号は強くなるが、同時に処理速度が低下する恐れがある。
        aes.BlockSize = blockSize;             // 一つのブロックのサイズを指定する。
        aes.Mode = CipherMode.CBC;             // 暗号化モードを指定する。
        aes.IV = Encoding.UTF8.GetBytes(iv);   // 初期化ベクトルを指定する。
        aes.Key = Encoding.UTF8.GetBytes(key); // 鍵を指定する。
        // PaddingMode列挙型とは https://learn.microsoft.com/ja-jp/dotnet/api/system.security.cryptography.paddingmode?view=net-7.0
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }
}