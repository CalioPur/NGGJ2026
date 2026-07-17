using System.Collections;
using UnityEngine;

public class FirstKillableTarget : MonoBehaviour, Interactable, Takedownable
{
    public InteractionCue cueTakedown;
    public InteractionCue cueInteract;
    private PlayerController player;
    private bool isPlayerIsolated = false;
    private Rigidbody2D rb;
    [SerializeField] private DetectionSystem detectionSystem;
    [SerializeField] private DetectionSystem barmanInSight;
    
    public MonoBehaviour[] scriptsToDisable;
    public GameObject [] gameObjectsToDisable;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.PlayerHasItem("Bottle"))
            {
                player.AddInteractable(this);
            }
            else
            {
                player.SetTakedownTarget(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.RemoveInteractable(this);
            player.SetTakedownTarget(null);
        }
    }
    
    public void Interact()
    {
        player.RemoveItem("Bottle");
        StartCoroutine(MoveRight());
        player.RemoveInteractable(this);
    }

    public float DistanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    public void SetActiveInteractable(bool isActive)
    {
        if (isActive)
        {
            cueInteract.ShowInteractionCue();
        }
        else
        {
            cueInteract.HideInteractionCue();
        }
    }

    public void TakeDownAction()
    {
        if (!isPlayerIsolated)
        {
            barmanInSight.SetSpotted(true);
        }
        
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(0.5f, 0.5f, 0.5f);
        sr.sortingLayerName = "Background";
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        transform.position = player.transform.position + new Vector3(1,-0.5f, 0.5f);
        
        foreach (GameObject go in gameObjectsToDisable)
        {
            go.SetActive(false);
        }
        
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }
        this.enabled = false;
    }

    public void ActualizeTakedownability(bool newTakedownOverride = true)
    {
        if (newTakedownOverride)
        {
            cueTakedown.ShowInteractionCue();
        }
        else
        {
            cueTakedown.HideInteractionCue();
        }
    }

    public IEnumerator MoveRight()
    {
        float animTime = 3f;
        float t = 0;
        detectionSystem.currentState = NPCState.Distracted;
        while (t < animTime)
        {
            t += Time.deltaTime;
            rb.transform.Translate(Vector3.right * (2 * Time.deltaTime));
            if(t<(animTime/2)&& !isPlayerIsolated) isPlayerIsolated = true;
            yield return null;
        }
        detectionSystem.currentState = NPCState.Alert;
    }
}
