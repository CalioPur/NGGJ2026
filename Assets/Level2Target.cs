using UnityEngine;

public class Level2Target : MonoBehaviour, Takedownable
{
    
    PlayerController player;
    [SerializeField] private InteractionCue cue;
    [SerializeField] DetectionSystem barmanDetectionSystem;
    
    public MonoBehaviour[] scriptsToDisable;
    public GameObject [] gameObjectsToDisable;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.SetTakedownTarget(this);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.SetTakedownTarget(null);
        }
    }
    
    public void TakeDownAction()
    {
        if (barmanDetectionSystem.currentState == NPCState.Alert)
        {
            barmanDetectionSystem.SetSpotted(true);
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
            cue.ShowInteractionCue();
        }
        else
        {
            cue.HideInteractionCue();
        }
    }
}
