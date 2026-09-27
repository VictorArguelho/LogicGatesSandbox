using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public static AssetsManager Instance { get; private set; }

    [SerializeField] private Sprite _andSprite;
    [SerializeField] private Sprite _orSprite;
    [SerializeField] private Sprite _xorSprite;
    [SerializeField] private Sprite _norSprite;
    [SerializeField] private Sprite _nandSprite;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetDefaultGateSprite(DefaultGateCode code)
    {
        return code switch
        {
            DefaultGateCode.And => _andSprite,
            DefaultGateCode.Or => _orSprite,
            DefaultGateCode.Xor => _xorSprite,
            DefaultGateCode.Nor => _norSprite,
            DefaultGateCode.Nand => _nandSprite,
            _ => null
        };
    }
}