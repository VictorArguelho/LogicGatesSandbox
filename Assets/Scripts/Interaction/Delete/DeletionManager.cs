using System.Collections.Generic;
using UnityEngine;

public class DeletionManager : MonoBehaviour
{
    public static DeletionManager Instance { get; private set; }
    private readonly static List<Deletable> _delectables = new();

    public static void RegisterDeletable(Deletable deletable)
    {
        if (!_delectables.Contains(deletable))
            _delectables.Add(deletable);
    }

    public static void UnregisterDeletable(Deletable deletable) =>
        _delectables.Remove(deletable);

    private void Awake() =>
        Instance = this;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
        {
            foreach (var deletable in _delectables.ToArray())
            {
                if (deletable.IsSelected && deletable.CanBeDeletedByUser)
                    deletable.Delete();
            }
        }
    }
}