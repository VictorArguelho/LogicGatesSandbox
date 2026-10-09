using LogicGatesSandbox.Engine.Components;
using LogicGatesSandbox.Engine.Loading;
using LogicGatesSandbox.Engine.Rendering;
using LogicGatesSandbox.Engine.Components.Input;
using LogicGatesSandbox.Engine.Components.Time;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;

namespace LogicGatesSandbox.Engine;

public static class Engine
{
    public static GL GL { get; private set; } = null!;
    public static IInputContext InputContext { get; private set; } = null!;

    public static void Main()
    {
        using IWindow window = Window.Create(GetWindowOptions());

        window.Load += () =>
        {
            WindowIcon.Set(window, "Assets/Icon.png");

            GL = window.CreateOpenGL();

            GL.Enable(EnableCap.Blend);

            GL.BlendFunc(
                BlendingFactor.SrcAlpha,
                BlendingFactor.OneMinusSrcAlpha
            );

            GL.ClearColor(
                0.05f,
                0.15f,
                0.3f,
                1.0f
            );

            GL.Viewport(window.FramebufferSize);

            Camera.Initialize(
                window.FramebufferSize.X,
                window.FramebufferSize.Y
            );

            TextureRenderer.Initialize();

            SpritesLoader.LoadSprites();

            InputContext = window.CreateInput();
            Keyboard.Initialize();
            Mouse.Initialize();
        };

        window.Update += deltaTime =>
        {
            Time.Update(deltaTime);
            FPS.Update();

            Keyboard.Update();
            Mouse.Update();

            Timer.Update();
        };

        window.Render += deltaTime =>
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            RenderManager.Render();

            SpriteRenderQueue.RenderSprites();
        };

        window.FramebufferResize += size =>
        {
            GL.Viewport(size);

            Camera.Resize(
                size.X,
                size.Y
            );
        };

        window.Run();

        GL.Dispose();
    }

    private static WindowOptions GetWindowOptions()
    {
        var options = WindowOptions.Default;

        options.Title = "Logic Gates Sandbox";

        options.WindowState = WindowState.Fullscreen;

        options.VSync = false;

        options.FramesPerSecond = 0;

        return options;
    }
}