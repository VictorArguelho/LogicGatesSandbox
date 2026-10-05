using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CircuitLoadManager : Singleton<CircuitLoadManager>
{
    private const string FILE_NAME = "savedcircuits.json";

    private SavedCircuits _savedCircuits;

    protected override void Awake()
    {
        base.Awake();

        Load();
    }

    private void Start()
    {
        if (_savedCircuits.CircuitPaths == null)
            return;

        foreach (var path in _savedCircuits.CircuitPaths)
            if (CircuitLoader.TryLoadCircuitData(path, out var data))
                SelectionMenu.Instance.AddItem(new(data));
    }

    public void RegisterCircuit(string directoryName)
    {
        if (string.IsNullOrWhiteSpace(directoryName))
            return;

        var paths = new List<string>();

        if (_savedCircuits.CircuitPaths != null)
            paths.AddRange(_savedCircuits.CircuitPaths);

        if (paths.Contains(directoryName))
            return;

        paths.Add(directoryName);

        _savedCircuits = new SavedCircuits(paths.ToArray());

        Save();
    }

    private void Load()
    {
        var path = GetSavePath();

        if (!File.Exists(path))
        {
            _savedCircuits = new SavedCircuits(
                System.Array.Empty<string>()
            );

            return;
        }

        try
        {
            var json = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
            {
                _savedCircuits = new SavedCircuits(
                    System.Array.Empty<string>()
                );

                return;
            }

            _savedCircuits = JsonUtility.FromJson<SavedCircuits>(json);

            if (_savedCircuits.CircuitPaths == null)
            {
                _savedCircuits = new SavedCircuits(
                    System.Array.Empty<string>()
                );
            }
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);

            _savedCircuits = new SavedCircuits(
                System.Array.Empty<string>()
            );
        }
    }

    private void Save()
    {
        try
        {
            var json = JsonUtility.ToJson(
                _savedCircuits,
                true
            );

            File.WriteAllText(
                GetSavePath(),
                json
            );
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    private static string GetSavePath() =>
        Path.Combine(
            Application.persistentDataPath,
            FILE_NAME
        );
}