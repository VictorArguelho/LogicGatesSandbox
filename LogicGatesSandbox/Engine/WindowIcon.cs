using Silk.NET.Core;
using Silk.NET.Windowing;
using StbImageSharp;
using System.IO;

namespace LogicGatesSandbox.Engine;

public static class WindowIcon
{
    public static void Set(IWindow window, string path)
    {
        using FileStream stream = File.OpenRead(path);

        ImageResult image = ImageResult.FromStream(
            stream,
            ColorComponents.RedGreenBlueAlpha
        );

        RawImage rawImage = new(
            image.Width,
            image.Height,
            image.Data
        );

        window.SetWindowIcon(ref rawImage);
    }
}