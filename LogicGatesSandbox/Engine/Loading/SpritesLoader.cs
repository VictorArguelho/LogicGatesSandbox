using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LogicGatesSandbox.Engine.Rendering;

namespace LogicGatesSandbox.Engine.Loading;

public static class SpritesLoader
{
    private const string SPRITES_FOLDER = "Assets/Sprites";
    private static readonly string[] IMAGE_EXTENSIONS = [
        ".png", 
        ".jpg", 
        ".jpeg"
    ];

    private readonly static Dictionary<string, Texture> _sprites = [];

    public static Texture GetSpriteTexture(string name)
    {
        if (!_sprites.TryGetValue(name, out Texture texture))
            throw new ArgumentException($"Sprite '{name}' não encontrado.");

        return texture;
    }

    public static void LoadSprites()
    {
        foreach (var imagePath in EnumerateImagePaths())
        {
            if (!IsImageFile(imagePath))
                continue;

            TryLoadSprite(imagePath);
        }
    }

    public static void DisposeSprites()
    {
        foreach (var sprite in _sprites.Values)
            sprite.Dispose();

        _sprites.Clear();
    }

    private static IEnumerable<string> EnumerateImagePaths() =>
        Directory.EnumerateFiles(
            SPRITES_FOLDER,
            "*",
            SearchOption.AllDirectories
        );

    private static bool IsImageFile(string path) =>
        IMAGE_EXTENSIONS.Contains(
            Path.GetExtension(path),
            StringComparer.OrdinalIgnoreCase
        );

    private static void TryLoadSprite(string imagePath)
    {
        string imageName = Path.GetFileNameWithoutExtension(imagePath);

        var texture = ImageLoader.LoadImageToTexture(imagePath);

        if (!_sprites.TryAdd(imageName, texture))
        {
            Console.WriteLine(
                $"Um sprite com o nome '{imageName}' já foi carregado."
            );

            texture.Dispose();
        }
    }
}