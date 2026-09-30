using UnityEngine;

[RequireComponent(typeof(Wire))]
[RequireComponent(typeof(Deletable))]
[RequireComponent(typeof(SelectableAppearance))]
public class Cable : MonoBehaviour, IRestorable<CableRestoreData>
{
    [Header("Off Signal")]
    [SerializeField] private Color _offSignalDefaultColor = Color.white;
    [SerializeField] private Color _offSignalMouseOverColor = new(0.75f, 0.75f, 0.75f, 0.9f);
    [SerializeField] private Color _offSignalSelectedColor = new(0.5f, 0.5f, 0.5f, 0.8f);

    [Header("On Signal")]
    [SerializeField] private Color _onSignalDefaultColor = new(
        0x6D / 255f,
        0xCC / 255f,
        0xE5 / 255f,
        1f
    );
    [SerializeField] private Color _onSignalMouseOverColor = new(
        0x4D / 255f,
        0x9B / 255f,
        0xB0 / 255f,
        1f
    );
    [SerializeField] private Color _onSignalSelectedColor = new(
        0x29 / 255f,
        0x5C / 255f,
        0x6A / 255f,
        1f
    );

    private SelectableAppearance _selectableAppearance;
    private Deletable _deletable;
    private Wire _wire;

    private Port _outPort;
    private Port _inPort;
    private bool _lastSignal;

    public Port OutPort => _outPort;
    public Port InPort => _inPort;

    private void Awake()
    {
        _selectableAppearance = GetComponent<SelectableAppearance>();
        _deletable = GetComponent<Deletable>();
        _wire = GetComponent<Wire>();
    }

    public void Initialize(Port outPort, Port inPort)
    {
        _outPort = outPort;
        _inPort = inPort;

        _outPort.OnDeleted += OnPortDeleteHandle;
        _inPort.OnDeleted += OnPortDeleteHandle;

        _deletable.OnDeleted += OnDeleteHandle;

        Refresh();
    }

    private void Update() =>
        Refresh();

    private void OnDestroy()
    {
        if (_outPort != null)
            _outPort.OnDeleted -= OnPortDeleteHandle;

        if (_inPort != null)
            _inPort.OnDeleted -= OnPortDeleteHandle;

        _deletable.OnDeleted -= OnDeleteHandle;
    }

    private void Refresh()
    {
        if (_outPort == null || _inPort == null)
            return;

        var offset = new Vector2(0.5f, -0.5f);

        _wire.SetPoints(
            (Vector2)_outPort.transform.position + offset * _outPort.transform.localScale,
            (Vector2)_inPort.transform.position + offset * _inPort.transform.localScale
        );

        if (_outPort.Signal == _lastSignal)
            return;

        if (_outPort.Signal)
        {
            _selectableAppearance.DefaultColor = _onSignalDefaultColor;
            _selectableAppearance.MouseOverColor = _onSignalMouseOverColor;
            _selectableAppearance.SelectedColor = _onSignalSelectedColor;
        } 
        else
        {
            _selectableAppearance.DefaultColor = _offSignalDefaultColor;
            _selectableAppearance.MouseOverColor = _offSignalMouseOverColor;
            _selectableAppearance.SelectedColor = _offSignalSelectedColor;
        }

        _selectableAppearance.RefreshColor();
        _lastSignal = _outPort.Signal;
    }

    private void OnPortDeleteHandle() =>
        _deletable.Delete();

    private void OnDeleteHandle()
    {
        if (_outPort != null && _inPort != null)
            _outPort.TryDisconnectAtOut(_inPort);
        Destroy(gameObject);
    }

    public CableRestoreData GetRestoreData() =>
        new(_outPort.GetRestoreData(), _inPort.GetRestoreData());
}