using System;
using Unity.VisualScripting;
using UnityEngine;

public class TakeDown : MonoBehaviour, Takedownable
{
    PlayerController player;
    public bool isPlayerInRange;
    public InteractionCue cue;

    public MonoBehaviour[] scriptsToDisable;
    public GameObject [] gameObjectsToDisable;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerInRange = true;
            player.SetTakedownTarget(this);
            ActualizeTakedownability();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerInRange = false;
            ActualizeTakedownability();
            
        }
    }
    
    public void ActualizeTakedownability(bool newTakedownOverride = true)
    {
        if (isPlayerInRange && player.isHidden && newTakedownOverride)
        {
            cue.ShowInteractionCue();
        }
        else
        {
            cue.HideInteractionCue();
        }
    }
    
    public void TakeDownAction()
    {
        if (isPlayerInRange && player.isHidden)
        {
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
    }
}

public interface Takedownable
{
    
    public void TakeDownAction();
    public void ActualizeTakedownability(bool newTakedownOverride = true);
}
