using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public static AssetsManager Instance { get; private set; }

    [SerializeField] private Sprite _andSprite;
    [SerializeField] private Sprite _orSprite;
    [SerializeField] private Sprite _notSprite;
    [SerializeField] private Sprite _xorSprite;
    [SerializeField] private Sprite _norSprite;
    [SerializeField] private Sprite _nandSprite;
    [SerializeField] private Sprite _xnorSprite;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetDefaultGateSprite(GateCode code)
    {
        return code switch
        {
            GateCode.And => _andSprite,
            GateCode.Or => _orSprite,
            GateCode.Not => _notSprite,
            GateCode.Xor => _xorSprite,
            GateCode.Nor => _norSprite,
            GateCode.Nand => _nandSprite,
            GateCode.Xnor => _xnorSprite,
            _ => null
        };
    }
}