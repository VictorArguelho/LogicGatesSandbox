using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public static AssetsManager Instance { get; private set; }

    [SerializeField] private Sprite _andSprite;
    [SerializeField] private Sprite _orSprite;
    [SerializeField] private Sprite _notSprite;

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
            DefaultGateCode.Not => _notSprite,
            _ => null
        };
    }
}