// 日本語対応
using UniRx;

/// <summary>
/// ひとりのアクターを表現するクラス
/// </summary>
public class Actor : ILife
{
    public Actor(int id, string name, float life)
    {
        _id = id; _name = name; _life = new FloatReactiveProperty(life);
    }

    /// <summary> このアクターのID </summary>
    private readonly int _id = -1;
    /// <summary> このアクターの名前 </summary>
    private readonly string _name = "";
    /// <summary> このアクターのライフ </summary>
    private readonly FloatReactiveProperty _life = default;

    /// <summary> このアクターのID </summary>
    public int ID => _id;
    /// <summary> このアクターの名前 </summary>
    public string Name => _name;
    /// <summary> このアクターのライフ </summary>
    public float Life { get => _life.Value; set => _life.Value = value; }
    /// <summary> ライフのリアクティブプロパティ </summary>
    public IReadOnlyReactiveProperty<float> LifeReactiveProperty => _life;
}
