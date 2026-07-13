using System;
using UnityEngine;

public class MovingNPC : MonoBehaviour
{
    [SerializeField] private bool isGoingRight;
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    [SerializeField] private BoxCollider2D detectionCollider;
    private Rigidbody2D rb;
    [SerializeField] private float distanceToFloor;
    [SerializeField] private float decelerationRatio = 7.5f;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopTimeWhenLimitReached;
    private bool isStopped;
    private float timer;
    private Vector3 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (isGoingRight)
        {
            detectionCollider.offset = new Vector2(-detectionCollider.offset.x, detectionCollider.offset.y);
        }
    }

    private void Update()
    {
        if (isStopped)
        {
            timer += Time.deltaTime;
            if (timer >= stopTimeWhenLimitReached)
            {
                isStopped = false;
                timer = 0;
                isGoingRight = !isGoingRight;
                detectionCollider.offset = new Vector2(-detectionCollider.offset.x, detectionCollider.offset.y);
                
            }
            velocity = Vector2.Lerp(velocity, Vector2.zero, decelerationRatio * Time.deltaTime);
        }
        else
        {
            velocity.x = (isGoingRight ? 1 : -1) * moveSpeed;
            rb.transform.Translate(velocity * Time.deltaTime);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 3f,  LayerMask.GetMask("Ground"));
            if (hit)
            {
                rb.transform.position = hit.point + Vector2.up * distanceToFloor;
            }
            
            if (transform.position.x < leftLimit.position.x &&!isGoingRight)
            {
                isStopped = true;
            }

            if (transform.position.x > rightLimit.position.x  &&isGoingRight)
            {
                isStopped = true;
            }

        }
        rb.transform.Translate(velocity * Time.deltaTime);
    }
}
