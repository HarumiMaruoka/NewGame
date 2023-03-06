// 日本語対応
using System;
using System.Collections.Generic;
using UnityEngine;

public class BGMHolder : MonoBehaviour
{
    [SerializeField]
    private BGMTypeClipPair[] _audioTypeClipPairs = default;

    private Dictionary<BGMType, AudioClip> _bgmList = new Dictionary<BGMType, AudioClip>();
    public IReadOnlyDictionary<BGMType, AudioClip> BGMList => _bgmList;

    private void Awake()
    {
        int i = 0;
        try
        {
            for (; i < _audioTypeClipPairs.Length; i++)
            {
                _bgmList.Add(_audioTypeClipPairs[i].AudioType, _audioTypeClipPairs[i].AudioClip);
            }
        }
        catch (ArgumentException)
        {
            Debug.LogWarning($"BGMTypeが重複しています！修正ください！\n" +
                $"重複しているキー{_audioTypeClipPairs[i].AudioType}");
        }
        _audioTypeClipPairs = null;
    }
}

[Serializable]
public class BGMTypeClipPair
{
    [SerializeField]
    private BGMType _audioType = default;
    [SerializeField]
    private AudioClip _audioClip = default;

    public BGMType AudioType => _audioType;
    public AudioClip AudioClip => _audioClip;
}
[Serializable]
public enum BGMType
{

}