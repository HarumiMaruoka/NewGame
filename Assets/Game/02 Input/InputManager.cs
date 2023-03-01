// 日本語対応
using System;
using UnityEngine;
using Input;

public class InputManager : InputManagerBase<GameInput, InputType>
{
    protected override void Setup()
    {
        SetAction<Vector2>(_inputActionCollection.Player.Move, InputType.Move);
        SetAction<float>(_inputActionCollection.Player.Jump, InputType.Jump);
    }
}
public enum InputType
{
    Move, Jump
}