using UnityEngine;

public class CircuitJsonAnalyzer : MonoBehaviour
{
    [SerializeField] private TextAsset _circuitJson;

    [ContextMenu("Analyze Circuit")]
    private void AnalyzeCircuit()
    {
        if (_circuitJson == null)
        {
            Debug.LogWarning("Nenhum arquivo JSON foi selecionado.");
            return;
        }

        var circuit = JsonUtility.FromJson<CircuitData>(_circuitJson.text);

        var items = circuit.RestoreData.Items;
        var cables = circuit.RestoreData.Cables;

        if (items == null)
            items = System.Array.Empty<ItemRestoreData>();

        if (cables == null)
            cables = System.Array.Empty<CableRestoreData>();

        int gates = 0;
        int ports = 0;

        for (int i = 0; i < items.Length; i++)
        {
            var code = items[i].ItemData.Code;

            if (code.ToGateCode() != GateCode.None)
            {
                gates++;
                continue;
            }

            if (code == ItemCode.Port ||
                code == ItemCode.ToggablePort)
            {
                ports++;
            }
        }

        Debug.Log(
            $"Circuit Analysis\n" +
            $"Name: {circuit.Name}\n" +
            $"Gates: {gates}\n" +
            $"Ports: {ports}\n" +
            $"Cables: {cables.Length}\n" +
            $"Total: {gates + ports}"
        );
    }
}