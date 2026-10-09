using Silk.NET.OpenGL;
using StbImageSharp;
using System.IO;

namespace LogicGatesSandbox.Engine.Loading;

internal static class ImageLoader
{
    private static readonly GL _gl = Engine.GL;

    internal static Resources.Texture LoadImageToTexture(string path)
    {
        var image = GetImage(path);
        uint handle = _gl.GenTexture();

        _gl.BindTexture(
            TextureTarget.Texture2D,
            handle
        );

        SetTextureParameters();

        _gl.TexImage2D(
            TextureTarget.Texture2D,
            0,
            InternalFormat.Rgba,
            (uint)image.Width,
            (uint)image.Height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            image.Data
        );

        _gl.BindTexture(
            TextureTarget.Texture2D,
            0
        );

        return new Resources.Texture(
            handle,
            image.Width,
            image.Height
        );
    }

    private static ImageResult GetImage(string path)
    {
        using FileStream stream = File.OpenRead(path);

        var image = ImageResult.FromStream(
            stream,
            ColorComponents.RedGreenBlueAlpha
        );

        return image;
    }

    private static void SetTextureParameters()
    {
        _gl.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMinFilter,
            (int)GLEnum.Linear
        );

        _gl.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMagFilter,
            (int)GLEnum.Linear
        );

        _gl.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureWrapS,
            (int)GLEnum.ClampToEdge
        );

        _gl.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureWrapT,
            (int)GLEnum.ClampToEdge
        );
    }
}