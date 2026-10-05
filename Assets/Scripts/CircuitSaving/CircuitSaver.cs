using System;
using System.IO;
using UnityEngine;

public static class CircuitSaver
{
    private const string CIRCUITS_DIRECTORY = "Circuits";
    private const string DATA_FILE_NAME = "circuit.json";
    private const string ICON_FILE_NAME = "icon";

    public static CircuitSaveResult Save(CircuitData data, string imagePath)
    {
        var directoryName = GetValidDirectoryName(data.Name);

        if (string.IsNullOrEmpty(directoryName))
            return CircuitSaveResult.InvalidName;

        directoryName = directoryName.ToUpperInvariant();

        var circuitsDirectory = Path.Combine(
            Application.persistentDataPath,
            CIRCUITS_DIRECTORY
        );

        var circuitDirectory = Path.Combine(
            circuitsDirectory,
            directoryName
        );

        if (Directory.Exists(circuitDirectory))
        {
            return new CircuitSaveResult(
                CircuitSaveStatus.AlreadyExists,
                directoryName
            );
        }

        if (!File.Exists(imagePath))
        {
            return new CircuitSaveResult(
                CircuitSaveStatus.InvalidImage,
                directoryName
            );
        }

        try
        {
            Directory.CreateDirectory(circuitDirectory);

            data = new CircuitData(
                data.RestoreData,
                data.Category,
                data.Name,
                data.Description,
                directoryName
            );

            SaveData(data, circuitDirectory);
            SaveImage(imagePath, circuitDirectory);

            return new CircuitSaveResult(
                CircuitSaveStatus.Success,
                directoryName
            );
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);

            return new CircuitSaveResult(
                CircuitSaveStatus.Failed,
                directoryName
            );
        }
    }

    private static void SaveData(
        CircuitData data,
        string circuitDirectory
    )
    {
        var json = JsonUtility.ToJson(data, true);

        var filePath = Path.Combine(
            circuitDirectory,
            DATA_FILE_NAME
        );

        File.WriteAllText(filePath, json);
    }

    private static void SaveImage(
        string imagePath,
        string circuitDirectory
    )
    {
        var extension = Path.GetExtension(imagePath);

        if (string.IsNullOrEmpty(extension))
            extension = ".png";

        var destinationPath = Path.Combine(
            circuitDirectory,
            ICON_FILE_NAME + extension
        );

        File.Copy(
            imagePath,
            destinationPath,
            false
        );
    }

    private static string GetValidDirectoryName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;

        var invalidCharacters = Path.GetInvalidFileNameChars();
        var result = name.Trim();

        foreach (var character in invalidCharacters)
            result = result.Replace(character, '_');

        result = result.TrimEnd('.', ' ');

        if (string.IsNullOrEmpty(result))
            return string.Empty;

        if (IsReservedWindowsName(result))
            result = $"_{result}";

        return result;
    }

    private static bool IsReservedWindowsName(string name)
    {
        var nameWithoutExtension = Path.GetFileNameWithoutExtension(name);

        return nameWithoutExtension.Equals("CON", StringComparison.OrdinalIgnoreCase) ||
               nameWithoutExtension.Equals("PRN", StringComparison.OrdinalIgnoreCase) ||
               nameWithoutExtension.Equals("AUX", StringComparison.OrdinalIgnoreCase) ||
               nameWithoutExtension.Equals("NUL", StringComparison.OrdinalIgnoreCase) ||
               nameWithoutExtension.StartsWith("COM", StringComparison.OrdinalIgnoreCase) &&
               int.TryParse(nameWithoutExtension[3..], out var comNumber) &&
               comNumber is >= 1 and <= 9 ||
               nameWithoutExtension.StartsWith("LPT", StringComparison.OrdinalIgnoreCase) &&
               int.TryParse(nameWithoutExtension[3..], out var lptNumber) &&
               lptNumber is >= 1 and <= 9;
    }
}