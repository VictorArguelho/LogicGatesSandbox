using UnityEngine;

[RequireComponent(typeof(GridDraggable))]
[RequireComponent(typeof(SelectableAppearance))]
[RequireComponent(typeof(Deletable))]
public class NotGate : MonoBehaviour
{
    [Header("In")]
    [SerializeField] private Port _inPort;

    [Header("Out")]
    [SerializeField] private Port _outPort;

    private void Update() =>
        _outPort.SetSignal(!_inPort.Signal);
}