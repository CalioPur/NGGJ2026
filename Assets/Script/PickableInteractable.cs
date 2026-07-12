using UnityEngine;

public class PickableInteractable : MonoBehaviour, Interactable
{
    public string itemID;
    public InteractionCue cue;
    private PlayerController player;
    public GameObject objectToMakeAppear;
    

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
        player.AddItem(this);
        if (objectToMakeAppear != null)
        {
            objectToMakeAppear.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    public float DistanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    public void SetActiveInteractable(bool isActive)
    {
        if (isActive)
        {
            cue.ShowInteractionCue();
        }
        else
        {
            cue.HideInteractionCue();
        }
    }
}
