using System.Collections.Generic;

public class DeletionManager : Singleton<DeletionManager>
{
    private readonly static List<Deletable> _delectables = new();

    public static void RegisterDeletable(Deletable deletable)
    {
        if (!_delectables.Contains(deletable))
            _delectables.Add(deletable);
    }

    public static void UnregisterDeletable(Deletable deletable) =>
        _delectables.Remove(deletable);

    protected override void Awake()
    {
        base.Awake();

        InputManager.Instance.OnDelete += HandleDelete;
    }

    private void HandleDelete()
    {
        foreach (var deletable in _delectables.ToArray())
        {
            if (deletable.IsSelected && deletable.CanBeDeletedByUser)
                deletable.Delete();
        }
    }
}