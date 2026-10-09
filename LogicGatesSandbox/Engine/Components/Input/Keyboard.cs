using Silk.NET.Input;
using System.Collections.Generic;

namespace LogicGatesSandbox.Engine.Components.Input;

public static class Keyboard
{
    private static readonly IKeyboard _keyboard = Engine.InputContext.Keyboards[0];

    private static readonly HashSet<InputCode> _frameDownKeys = [];
    private static readonly HashSet<InputCode> _frameUpKeys = [];

    private static readonly HashSet<InputCode> _handledFrameDownKeys = [];
    private static readonly HashSet<InputCode> _handledFrameUpKeys = [];

    public static void Initialize()
    {
        _keyboard.KeyDown += KeyDownHandle;
        _keyboard.KeyUp += KeyUpHandle;
    }

    public static void Update()
    {
        _frameDownKeys.Clear();
        _frameUpKeys.Clear();

        _frameDownKeys.UnionWith(_handledFrameDownKeys);
        _frameUpKeys.UnionWith(_handledFrameUpKeys);

        _handledFrameDownKeys.Clear();
        _handledFrameUpKeys.Clear();
    }

    public static bool IsKeyDown(InputCode code) =>
        _keyboard.IsKeyPressed(code.ToKey());

    public static bool IsKeyFrameDown(InputCode code) =>
        _frameDownKeys.Contains(code);

    public static bool IsKeyFrameUp(InputCode code) =>
        _frameUpKeys.Contains(code);

    private static void KeyDownHandle(
        IKeyboard keyboard,
        Key key,
        int scancode
    )
    {
        var inputCode = key.ToInputCode();

        if (inputCode != InputCode.None)
            _handledFrameDownKeys.Add(inputCode);
    }

    private static void KeyUpHandle(
        IKeyboard keyboard,
        Key key,
        int scancode
    )
    {
        var inputCode = key.ToInputCode();

        if (inputCode != InputCode.None)
            _handledFrameUpKeys.Add(inputCode);
    }
}