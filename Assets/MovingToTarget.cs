using System;
using UnityEngine;

public class MovingToTarget : MonoBehaviour
{
    [SerializeField] Lampadaire lampadaireInteraction;
    [SerializeField] DetectionSystem detectionSystem;
    
    [SerializeField] private Transform target;
    [SerializeField] private Transform rest;

    [SerializeField] private float speed;
    [SerializeField] private float decelerationRatio = 7.5f;
    [SerializeField] private float targetInteractionDuration = 1.5f;
    [SerializeField] private float distanceToFloor = 1f;
    
    private StateTraveling state = StateTraveling.Rest;
    private Transform currentTarget;

    private Rigidbody2D rb;
    private Vector3 velocity;
    private float timer = 0;
    

    private void Start()
    {
        lampadaireInteraction.onLampadaireTurnOff += GoToLampadaire;
        rb = GetComponent<Rigidbody2D>();
    }

    private void GoToLampadaire()
    {
        currentTarget = target;
        detectionSystem.currentState = NPCState.Distracted;
        state = StateTraveling.MovingToTarget;
    }


    private void Update()
    {
        Debug.Log(state);
        if (state == StateTraveling.Rest)
        {
            velocity = Vector2.Lerp(velocity, Vector2.zero, decelerationRatio * Time.deltaTime);
        }
        if (state == StateTraveling.MovingToTarget)
        {
            velocity.x = (transform.position.x < currentTarget.position.x ? 1 : -1) * speed;
            rb.transform.Translate(velocity * Time.deltaTime);
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 3f,  LayerMask.GetMask("Ground"));
            if (hit)
            {
                rb.transform.position = hit.point + Vector2.up * distanceToFloor;
            }
            
            if (Vector3.Distance(transform.position, currentTarget.position) < 1f)
            {
                state = lampadaireInteraction.IsLampadaireOn()? StateTraveling.Rest : StateTraveling.TargetInteraction;
            }
        }
        if (state  == StateTraveling.TargetInteraction)
        {
            timer += Time.deltaTime;
            if (timer >= targetInteractionDuration)
            {
                lampadaireInteraction.TurnBackOn();
                currentTarget = rest;
                timer = 0;
                state = StateTraveling.MovingToTarget;
                detectionSystem.currentState = NPCState.Alert;

            }
            velocity = Vector2.Lerp(velocity, Vector2.zero, decelerationRatio * Time.deltaTime);
        }
    }
}

enum StateTraveling
{
    Rest,
    MovingToTarget,
    TargetInteraction
}
