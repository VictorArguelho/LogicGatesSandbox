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

    private void Awake()
    {
        var render = GetComponent<SpriteRenderer>();
        render.sprite = AssetsManager.Instance.GetDefaultGateSprite(_gateCode);
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
            case DefaultGateCode.Xor:
                DefaultGateOperators.Xor(_inPortA, _inPortB, _outPort);
                break;
            case DefaultGateCode.Nor:
                DefaultGateOperators.Nor(_inPortA, _inPortB, _outPort);
                break;
            case DefaultGateCode.Nand:
                DefaultGateOperators.Nand(_inPortA, _inPortB, _outPort);
                break;
        }
    }
}