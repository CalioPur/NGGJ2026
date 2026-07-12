using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    public NPCState currentState;

    [Range(0, 1)] public float detectionJaugePercent;
    public float jaugeProgressionSpeedMult = 1;
    

}

public enum NPCState
{
    Alert,
    distracted,
}
