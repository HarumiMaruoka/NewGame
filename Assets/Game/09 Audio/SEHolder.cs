// 日本語対応
using System;
using System.Collections.Generic;
using UnityEngine;

public class SEHolder : MonoBehaviour
{
    [SerializeField]
    private SETypeClipPair[] _audioTypeClipPairs = default;

    private Dictionary<SEType, AudioClip> _seSummary = new Dictionary<SEType, AudioClip>();
    public IReadOnlyDictionary<SEType, AudioClip> SeSummary => _seSummary;

    private void Awake()
    {
        int i = 0;
        try
        {
            for (; i < _audioTypeClipPairs.Length; i++)
            {
                _seSummary.Add(_audioTypeClipPairs[i].AudioType, _audioTypeClipPairs[i].AudioClip);
            }
        }
        catch (ArgumentException)
        {
            Debug.LogWarning($"SETypeが重複しています！修正ください！\n" +
                $"重複しているキー{_audioTypeClipPairs[i].AudioType}");
        }
    }
}
[Serializable]
public class SETypeClipPair
{
    [SerializeField]
    private SEType _audioType = default;
    [SerializeField]
    private AudioClip _audioClip = default;

    public SEType AudioType => _audioType;
    public AudioClip AudioClip => _audioClip;
}
[Serializable]
public enum SEType
{

}
