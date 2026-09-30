using UnityEngine;

public interface IRestorableItem
{
    ItemRestoreData GetItemRestoreData(Vector2 relativePosition);
}

public interface IRestorable<T>
{
    T GetRestoreData();
}