using UnityEngine;

[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(SelectableAppearance))]
[RequireComponent(typeof(Deletable))]
[RequireComponent(typeof(SpriteRenderer))]
public class Gate : MonoBehaviour, IHasPorts, IRestorableItem
{
    [Header("In")]
    [SerializeField] private Port _inPortA;
    [SerializeField] private Port _inPortB;

    [Header("Out")]
    [SerializeField] private Port _outPort;

    private SpriteRenderer _renderer;

    private GateCode _gateCode;

    private void Awake() =>
        _renderer = GetComponent<SpriteRenderer>();

    private void Update()
    {
        switch (_gateCode)
        {
            case GateCode.And:
                GateOperators.And(_inPortA, _inPortB, _outPort);
                break;
            case GateCode.Or:
                GateOperators.Or(_inPortA, _inPortB, _outPort);
                break;
            case GateCode.Not:
                GateOperators.Not(_inPortA, _outPort);
                break;
            case GateCode.Xor:
                GateOperators.Xor(_inPortA, _inPortB, _outPort);
                break;
            case GateCode.Nor:
                GateOperators.Nor(_inPortA, _inPortB, _outPort);
                break;
            case GateCode.Nand:
                GateOperators.Nand(_inPortA, _inPortB, _outPort);
                break;
            case GateCode.Xnor:
                GateOperators.Xnor(_inPortA, _inPortB, _outPort);
                break;
        }
    }

    public void Initialize(ItemData itemData)
    {
        _gateCode = itemData.Code.ToGateCode();
        _renderer.sprite = AssetsManager.Instance.TryGetSprite(itemData.SpriteCode);

        if (_gateCode == GateCode.Not)
        {
            _inPortB.gameObject.SetActive(false);
            _inPortB = null;

            _inPortA.transform.localPosition = new(-0.25f, -0.75f, 0);
        }
    }

    public Port GetPort(ItemPortData itemPortData)
    {
        if (itemPortData.Direction == PortDirection.In)
        {
            if (itemPortData.Index == 0)
                return _inPortA;
            if (itemPortData.Index == 1)
                return _inPortB;
        }

        if (itemPortData.Direction == PortDirection.Out && itemPortData.Index == 0)
            return _outPort;

        return null;
    }

    public ItemPortData GetItemPortData(Port port)
    {
        if (port == _inPortA)
            return new(PortDirection.In, 0);

        if (port == _inPortB)
            return new(PortDirection.In, 1);

        if (port == _outPort)
            return new(PortDirection.Out, 0);

        return ItemPortData.Invalid;
    }

    public ItemRestoreData GetItemRestoreData(Vector2 relativePosition)
    {
        if (TryGetComponent<SpawnedItem>(out var spawnedComponent))
            return new(spawnedComponent.Id, relativePosition, spawnedComponent.Data);
        return ItemRestoreData.Invalid;
    }
}