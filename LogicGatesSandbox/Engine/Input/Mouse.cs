using LogicGatesSandbox.Engine.Components;
using Silk.NET.Input;
using System.Collections.Generic;
using System.Numerics;

namespace LogicGatesSandbox.Engine.Input;

public static class Mouse
{
    private static readonly IMouse _mouse = Engine.InputContext.Mice[0];

    private static readonly HashSet<InputCode> _frameDownButtons = [];
    private static readonly HashSet<InputCode> _frameUpButtons = [];

    private static readonly HashSet<InputCode> _handledFrameDownButtons = [];
    private static readonly HashSet<InputCode> _handledFrameUpButtons = [];

    private static float _handledFrameScroll;

    public static Vector2 ScreenPosition => _mouse.Position;
    public static Vector2 WorldPosition => Camera.ScreenToWorldPosition(ScreenPosition);
    public static float FrameScroll { get; private set; }

    internal static void Initialize()
    {
        _mouse.MouseDown += MouseDownHandle;
        _mouse.MouseUp += MouseUpHandle;
        _mouse.Scroll += ScrollHandle;
    }

    internal static void Update()
    {
        _frameDownButtons.Clear();
        _frameUpButtons.Clear();

        _frameDownButtons.UnionWith(_handledFrameDownButtons);
        _frameUpButtons.UnionWith(_handledFrameUpButtons);

        _handledFrameDownButtons.Clear();
        _handledFrameUpButtons.Clear();

        FrameScroll = _handledFrameScroll;
        _handledFrameScroll = 0f;
    }

    public static bool IsButtonDown(InputCode code) =>
        _mouse.IsButtonPressed(code.ToMouseButton());

    public static bool IsButtonFrameDown(InputCode code) =>
        _frameDownButtons.Contains(code);

    public static bool IsButtonFrameUp(InputCode code) =>
        _frameUpButtons.Contains(code);

    private static void MouseDownHandle(IMouse mouse, MouseButton button)
    {
        var inputCode = button.ToInputCode();

        if (inputCode != InputCode.None)
            _handledFrameDownButtons.Add(inputCode);
    }

    private static void MouseUpHandle(IMouse mouse, MouseButton button)
    {
        var inputCode = button.ToInputCode();

        if (inputCode != InputCode.None)
            _handledFrameUpButtons.Add(inputCode);
    }

    private static void ScrollHandle(IMouse mouse, ScrollWheel scrollWheel)
    {
        _handledFrameScroll += scrollWheel.Y;

        if (scrollWheel.Y > 0)
            _handledFrameDownButtons.Add(InputCode.MouseWheelUp);

        if (scrollWheel.Y < 0)
            _handledFrameDownButtons.Add(InputCode.MouseWheelDown);
    }
}