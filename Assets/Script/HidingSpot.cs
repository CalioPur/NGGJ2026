using System;
using UnityEngine;

public class HidingSpot : MonoBehaviour, Interactable
{
    public GameObject hidingSpotLocation;
    public InteractionCue cue;
    private PlayerController player;
    //implement touchIndicator

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().AddInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().RemoveInteractable(this);
        }
    }


    public void Interact()
    {
        print("Hiding Spot");
        player.Hide(hidingSpotLocation.transform.position);
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

public interface Interactable
{
    public void Interact();
    public float DistanceFromPlayer();
    public void SetActiveInteractable(bool isActive);
}
