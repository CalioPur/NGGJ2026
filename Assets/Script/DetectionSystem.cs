using System;
using UnityEngine;
using UnityEngine.UI;

public class DetectionSystem : MonoBehaviour
{
    public NPCState currentState;

    [Range(0, 1)] public float detectionJaugePercent;
    public float jaugeProgressionSpeedMult = 1;
    
    bool isPlayerInSight;
    
    private PlayerController player;

    [SerializeField] private Image jaugeBackground;
    [SerializeField] private Image jaugeFill;
    [SerializeField] private Image jaugeSpotted;
    
    private bool playerSpotted = false;
    
    private float oldJaugeValue = 0;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInSight = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInSight = false;
        }
    }

    private void Update()
    {
        oldJaugeValue = detectionJaugePercent;
        if (isPlayerInSight && !player.isHidden &&currentState == NPCState.Alert)
        {
            if(player.isHidden) return;
            detectionJaugePercent += jaugeProgressionSpeedMult * Time.deltaTime * (player.isDisguised?0.3f:1);
        }
        else
        {
            if (detectionJaugePercent > 0)
            {
                detectionJaugePercent -= jaugeProgressionSpeedMult*0.5f * Time.deltaTime;
            }
        }

        if (!Mathf.Approximately(oldJaugeValue, detectionJaugePercent))
        {
            DisplayJauge();
        }
    }

    public void DisplayJauge()
    {
        if (playerSpotted) return;
        if (currentState == NPCState.Alert)
        {
            if (detectionJaugePercent > 0)
            {
                if (detectionJaugePercent >= 1)
                {
                    playerSpotted = true;
                    jaugeFill.gameObject.SetActive(false);
                    jaugeBackground.gameObject.SetActive(false);
                    jaugeSpotted.gameObject.SetActive(true);
                    //Lose Logic 
                }
                else
                {
                    jaugeFill.gameObject.SetActive(true);
                    jaugeBackground.gameObject.SetActive(true);
                    jaugeFill.fillAmount = detectionJaugePercent;
                }
            }
            else
            {
                jaugeFill.gameObject.SetActive(false);
                jaugeBackground.gameObject.SetActive(false);
            }
        }
        else
        {
           
           
        }
    }
}

public enum NPCState
{
    Alert,
    Distracted,
}
