using UnityEngine;

[RequireComponent(typeof(GridDraggable))]
[RequireComponent(typeof(SelectableAppearance))]
[RequireComponent(typeof(Deletable))]
[RequireComponent(typeof(SpriteRenderer))]
public class DefaultGate : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private DefaultGateCode _gateCode;

    [Header("In")]
    [SerializeField] private Port _inPortA;
    [SerializeField] private Port _inPortB;

    [Header("Out")]
    [SerializeField] private Port _outPort;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        SetGateCode(_gateCode);
    }

    private void Update()
    {
        switch (_gateCode)
        {
            case DefaultGateCode.And:
                DefaultGateOperators.And(_inPortA, _inPortB, _outPort);
                break;
            case DefaultGateCode.Or:
                DefaultGateOperators.Or(_inPortA, _inPortB, _outPort);
                break;
            case DefaultGateCode.Not:
                DefaultGateOperators.Not(_inPortA, _outPort);
                break;
            case DefaultGateCode.Xor:
                DefaultGateOperators.Xor(_inPortA, _inPortB, _outPort);
                break;
            case DefaultGateCode.Nor:
                DefaultGateOperators.Nor(_inPortA, _inPortB, _outPort);
                break;
            case DefaultGateCode.Nand:
                DefaultGateOperators.Nand(_inPortA, _inPortB, _outPort);
                break;
            case DefaultGateCode.Xnor:
                DefaultGateOperators.Xnor(_inPortA, _inPortB, _outPort);
                break;
        }
    }

    public void SetGateCode(DefaultGateCode code)
    {
        _gateCode = code;
        _renderer.sprite = AssetsManager.Instance.GetDefaultGateSprite(_gateCode);
    }

    public PortGateCode GetPortCode(Port port)
    {
        if (port == _inPortA)
            return PortGateCode.InputA;

        if (port == _inPortB)
            return PortGateCode.InputB;

        if (port == _outPort)
            return PortGateCode.Output;

        return (PortGateCode)(-1);
    }

    public Port GetPortByCode(PortGateCode portCode)
    {
        if (portCode == PortGateCode.InputA)
            return _inPortA;

        if (portCode == PortGateCode.InputB)
            return _inPortB;

        if (portCode == PortGateCode.Output)
            return _outPort;

        return null;
    }
}