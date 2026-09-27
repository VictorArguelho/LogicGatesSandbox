using UnityEngine;

[RequireComponent(typeof(Wire))]
public class PortWire : MonoBehaviour
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

    [SerializeField] private Color _onSignalMouseOverColor = new(0.75f, 0.75f, 0.75f, 0.9f);
    [SerializeField] private Color _onSignalSelectedColor = new(0.5f, 0.5f, 0.5f, 0.8f);

    [Header("References")]
    [SerializeField] private SelectableAppearance _selectableAppearance;
    private Wire _wire;

    private Port _outPort;
    private Port _inPort;
    private bool _lastSignal;

    public void Initialize(Port outPort, Port inPort)
    {
        _wire = GetComponent<Wire>();

        _outPort = outPort;
        _inPort = inPort;

        Refresh();
    }

    private void Update() =>
        Refresh();

    private void Refresh()
    {
        _wire.SetPoints(_outPort.transform.position, _inPort.transform.position);

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

        _lastSignal = _outPort.Signal;
    }
}