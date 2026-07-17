using UnityEngine;

public class BottleFall : MonoBehaviour, Interactable
{
    public InteractionCue cue;
    private PlayerController player;
    [SerializeField] private Transform brokenPos;
    [SerializeField] private DetectionSystem[] detectionSystemsToDistract;
    
    private bool isBroken;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.AddInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.RemoveInteractable(this);
        }
    }

    public void Interact()
    {
        if (!isBroken)
        {
            isBroken = true;
            transform.position = brokenPos.position;
            transform.rotation = brokenPos.rotation;
            cue.HideInteractionCue();
            foreach (DetectionSystem detectionSystem in detectionSystemsToDistract)
            {
                if (detectionSystem != null)
                {
                    detectionSystem.currentState = NPCState.Distracted;
                }
            }
        }
    }

    public float DistanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    public void SetActiveInteractable(bool isActive)
    {
        if (isActive && !isBroken)
        {
            cue.ShowInteractionCue();
        }
        else
        {
            cue.HideInteractionCue();
        }
    }
}
