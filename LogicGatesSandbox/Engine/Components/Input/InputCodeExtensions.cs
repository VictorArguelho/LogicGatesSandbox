using Silk.NET.Input;

namespace LogicGatesSandbox.Engine.Components.Input;

public static class InputCodeExtensions
{
    public static InputCode ToInputCode(this Key key) =>
        key switch
        {
            Key.A => InputCode.KeyA,
            Key.B => InputCode.KeyB,
            Key.C => InputCode.KeyC,
            Key.D => InputCode.KeyD,
            Key.E => InputCode.KeyE,
            Key.F => InputCode.KeyF,
            Key.G => InputCode.KeyG,
            Key.H => InputCode.KeyH,
            Key.I => InputCode.KeyI,
            Key.J => InputCode.KeyJ,
            Key.K => InputCode.KeyK,
            Key.L => InputCode.KeyL,
            Key.M => InputCode.KeyM,
            Key.N => InputCode.KeyN,
            Key.O => InputCode.KeyO,
            Key.P => InputCode.KeyP,
            Key.Q => InputCode.KeyQ,
            Key.R => InputCode.KeyR,
            Key.S => InputCode.KeyS,
            Key.T => InputCode.KeyT,
            Key.U => InputCode.KeyU,
            Key.V => InputCode.KeyV,
            Key.W => InputCode.KeyW,
            Key.X => InputCode.KeyX,
            Key.Y => InputCode.KeyY,
            Key.Z => InputCode.KeyZ,

            Key.Number0 => InputCode.Key0,
            Key.Number1 => InputCode.Key1,
            Key.Number2 => InputCode.Key2,
            Key.Number3 => InputCode.Key3,
            Key.Number4 => InputCode.Key4,
            Key.Number5 => InputCode.Key5,
            Key.Number6 => InputCode.Key6,
            Key.Number7 => InputCode.Key7,
            Key.Number8 => InputCode.Key8,
            Key.Number9 => InputCode.Key9,

            Key.Keypad0 => InputCode.Numpad0,
            Key.Keypad1 => InputCode.Numpad1,
            Key.Keypad2 => InputCode.Numpad2,
            Key.Keypad3 => InputCode.Numpad3,
            Key.Keypad4 => InputCode.Numpad4,
            Key.Keypad5 => InputCode.Numpad5,
            Key.Keypad6 => InputCode.Numpad6,
            Key.Keypad7 => InputCode.Numpad7,
            Key.Keypad8 => InputCode.Numpad8,
            Key.Keypad9 => InputCode.Numpad9,

            Key.F1 => InputCode.KeyF1,
            Key.F2 => InputCode.KeyF2,
            Key.F3 => InputCode.KeyF3,
            Key.F4 => InputCode.KeyF4,
            Key.F5 => InputCode.KeyF5,
            Key.F6 => InputCode.KeyF6,
            Key.F7 => InputCode.KeyF7,
            Key.F8 => InputCode.KeyF8,
            Key.F9 => InputCode.KeyF9,
            Key.F10 => InputCode.KeyF10,
            Key.F11 => InputCode.KeyF11,
            Key.F12 => InputCode.KeyF12,

            Key.Space => InputCode.KeySpace,
            Key.Escape => InputCode.KeyEscape,
            Key.Enter => InputCode.KeyEnter,
            Key.Tab => InputCode.KeyTab,

            _ => InputCode.None
        };

    public static Key ToKey(this InputCode inputCode) =>
        inputCode switch
        {
            InputCode.KeyA => Key.A,
            InputCode.KeyB => Key.B,
            InputCode.KeyC => Key.C,
            InputCode.KeyD => Key.D,
            InputCode.KeyE => Key.E,
            InputCode.KeyF => Key.F,
            InputCode.KeyG => Key.G,
            InputCode.KeyH => Key.H,
            InputCode.KeyI => Key.I,
            InputCode.KeyJ => Key.J,
            InputCode.KeyK => Key.K,
            InputCode.KeyL => Key.L,
            InputCode.KeyM => Key.M,
            InputCode.KeyN => Key.N,
            InputCode.KeyO => Key.O,
            InputCode.KeyP => Key.P,
            InputCode.KeyQ => Key.Q,
            InputCode.KeyR => Key.R,
            InputCode.KeyS => Key.S,
            InputCode.KeyT => Key.T,
            InputCode.KeyU => Key.U,
            InputCode.KeyV => Key.V,
            InputCode.KeyW => Key.W,
            InputCode.KeyX => Key.X,
            InputCode.KeyY => Key.Y,
            InputCode.KeyZ => Key.Z,

            InputCode.Key0 => Key.Number0,
            InputCode.Key1 => Key.Number1,
            InputCode.Key2 => Key.Number2,
            InputCode.Key3 => Key.Number3,
            InputCode.Key4 => Key.Number4,
            InputCode.Key5 => Key.Number5,
            InputCode.Key6 => Key.Number6,
            InputCode.Key7 => Key.Number7,
            InputCode.Key8 => Key.Number8,
            InputCode.Key9 => Key.Number9,

            InputCode.Numpad0 => Key.Keypad0,
            InputCode.Numpad1 => Key.Keypad1,
            InputCode.Numpad2 => Key.Keypad2,
            InputCode.Numpad3 => Key.Keypad3,
            InputCode.Numpad4 => Key.Keypad4,
            InputCode.Numpad5 => Key.Keypad5,
            InputCode.Numpad6 => Key.Keypad6,
            InputCode.Numpad7 => Key.Keypad7,
            InputCode.Numpad8 => Key.Keypad8,
            InputCode.Numpad9 => Key.Keypad9,

            InputCode.KeyF1 => Key.F1,
            InputCode.KeyF2 => Key.F2,
            InputCode.KeyF3 => Key.F3,
            InputCode.KeyF4 => Key.F4,
            InputCode.KeyF5 => Key.F5,
            InputCode.KeyF6 => Key.F6,
            InputCode.KeyF7 => Key.F7,
            InputCode.KeyF8 => Key.F8,
            InputCode.KeyF9 => Key.F9,
            InputCode.KeyF10 => Key.F10,
            InputCode.KeyF11 => Key.F11,
            InputCode.KeyF12 => Key.F12,

            InputCode.KeySpace => Key.Space,
            InputCode.KeyEscape => Key.Escape,
            InputCode.KeyEnter => Key.Enter,
            InputCode.KeyTab => Key.Tab,

            _ => Key.Unknown
        };

    public static InputCode ToInputCode(this MouseButton button) =>
        button switch
        {
            MouseButton.Left => InputCode.MouseLeft,
            MouseButton.Right => InputCode.MouseRight,
            MouseButton.Middle => InputCode.MouseMiddle,
            MouseButton.Button4 => InputCode.MouseBack,
            MouseButton.Button5 => InputCode.MouseForward,

            _ => InputCode.None
        };

    public static MouseButton ToMouseButton(this InputCode inputCode) =>
        inputCode switch
        {
            InputCode.MouseLeft => MouseButton.Left,
            InputCode.MouseRight => MouseButton.Right,
            InputCode.MouseMiddle => MouseButton.Middle,
            InputCode.MouseBack => MouseButton.Button4,
            InputCode.MouseForward => MouseButton.Button5,

            _ => MouseButton.Unknown
        };
}