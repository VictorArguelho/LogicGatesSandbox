using System;
using UnityEngine;

public class PlacementSelector : Singleton<PlacementSelector>
{
    [SerializeField] private ItemDataAsset _defaultItem;

    public CreationData SelectedCreationData { get; private set; }

    public event Action<CreationData> OnCreationDataSelected;

    private void Start() =>
        Select(_defaultItem.GetData());

    public void Select(ItemData itemData) =>
        Select(new CreationData(itemData));

    public void Select(CircuitData circuitData) =>
        Select(new CreationData(circuitData));

    public void Select(CreationData creationData)
    {
        if (SelectedCreationData == creationData)
            return;

        SelectedCreationData = creationData;
        OnCreationDataSelected?.Invoke(SelectedCreationData);
    }
}