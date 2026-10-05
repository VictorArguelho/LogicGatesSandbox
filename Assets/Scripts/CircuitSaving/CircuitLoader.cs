using System.IO;
using UnityEngine;

public static class CircuitLoader
{
    private const string CIRCUITS_DIRECTORY = "Circuits";
    private const string DATA_FILE_NAME = "circuit.json";
    private const string ICON_FILE_NAME = "icon";

    public static bool TryLoadCircuitData(
        string directoryName,
        out CircuitData data
    )
    {
        data = CircuitData.Invalid;

        if (string.IsNullOrWhiteSpace(directoryName))
            return false;

        var circuitDirectory = GetCircuitDirectory(directoryName);

        var dataPath = Path.Combine(
            circuitDirectory,
            DATA_FILE_NAME
        );

        if (!File.Exists(dataPath))
            return false;

        try
        {
            var json = File.ReadAllText(dataPath);

            if (string.IsNullOrWhiteSpace(json))
                return false;

            data = JsonUtility.FromJson<CircuitData>(json);

            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            return false;
        }
    }

    public static bool TryLoadCircuitSprite(
        string directoryName,
        out Sprite sprite
    )
    {
        sprite = null;

        if (string.IsNullOrWhiteSpace(directoryName))
            return false;

        var circuitDirectory = GetCircuitDirectory(directoryName);
        var imagePath = GetIconPath(circuitDirectory);

        if (imagePath == null)
            return false;

        Texture2D texture = null;

        try
        {
            var imageData = File.ReadAllBytes(imagePath);

            if (imageData.Length == 0)
                return false;

            texture = new Texture2D(2, 2);

            if (!texture.LoadImage(imageData))
                return false;

            sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );

            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            return false;
        }
        finally
        {
            if (sprite == null && texture != null)
                Object.Destroy(texture);
        }
    }

    private static string GetCircuitDirectory(string directoryName) =>
        Path.Combine(
            Application.persistentDataPath,
            CIRCUITS_DIRECTORY,
            directoryName
        );

    private static string GetIconPath(string circuitDirectory)
    {
        var pngPath = Path.Combine(
            circuitDirectory,
            ICON_FILE_NAME + ".png"
        );

        if (File.Exists(pngPath))
            return pngPath;

        var jpgPath = Path.Combine(
            circuitDirectory,
            ICON_FILE_NAME + ".jpg"
        );

        if (File.Exists(jpgPath))
            return jpgPath;

        var jpegPath = Path.Combine(
            circuitDirectory,
            ICON_FILE_NAME + ".jpeg"
        );

        if (File.Exists(jpegPath))
            return jpegPath;

        return null;
    }
}