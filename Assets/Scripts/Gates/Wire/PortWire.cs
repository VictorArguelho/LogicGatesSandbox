using UnityEngine;

[RequireComponent(typeof(Wire))]
public class PortWire : MonoBehaviour
{
    private readonly Color ON_SIGNAL_COLOR = new(
        0x6D / 255f,
        0xCC / 255f,
        0xE5 / 255f,
        1f
    );

    private readonly Color OFF_SIGNAL_COLOR = new(
        0.9f,
        0.9f,
        0.9f,
        1f
    );

    private Wire _wire;
    private Port _outPort;
    private Port _inPort;

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

        if (_outPort.Signal)
            _wire.SetColor(ON_SIGNAL_COLOR);
        else
            _wire.SetColor(OFF_SIGNAL_COLOR);
    }
}