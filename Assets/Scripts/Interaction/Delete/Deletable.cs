using System;
using UnityEngine;

[RequireComponent(typeof(Selectable))]
public class Deletable : MonoBehaviour
{
    private Selectable _selectable;

    [SerializeField] private bool _canBeDeletedByUser = true;

    public bool CanBeDeletedByUser => _canBeDeletedByUser;

    public bool IsSelected => _selectable.State == SelectionState.Selected;

    public event Action OnDeleted;

    private bool _deletedInvoked;

    protected virtual void Awake()
    {
        _selectable = GetComponent<Selectable>();
        DeletionManager.RegisterDeletable(this);
    }

    protected virtual void OnDestroy()
    {
        DeletionManager.UnregisterDeletable(this);

        if (!_deletedInvoked)
        {
            _deletedInvoked = true;
            OnDeleted?.Invoke();
        }
    }


    public void Delete()
    {
        if (_deletedInvoked)
            return;

        _deletedInvoked = true;
        OnDeleted?.Invoke();
        Destroy(gameObject);
    }
}