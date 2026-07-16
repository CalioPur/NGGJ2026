using System;
using UnityEngine;

public class Lampadaire : MonoBehaviour, Interactable
{
    
    public InteractionCue cue;
    private PlayerController player;
    public Action onLampadaireTurnOff;

    private bool isOff = false;

    [SerializeField] private GameObject ampoule;

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

    public bool IsLampadaireOn()
    {
        return !isOff;
    }
    
    public void Interact()
    {
        if (!isOff)
        {
            onLampadaireTurnOff?.Invoke();
            cue.HideInteractionCue();
            ampoule.SetActive(false);
            isOff = true;
        }
        
    }

    public void TurnBackOn()
    {
        ampoule.SetActive(true);
        isOff = false;
    }
    
    public float DistanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    public void SetActiveInteractable(bool isActive)
    {
        if (isActive && !isOff)
        {
            cue.ShowInteractionCue();
        }
        else
        {
            cue.HideInteractionCue();
        }
    }
}
