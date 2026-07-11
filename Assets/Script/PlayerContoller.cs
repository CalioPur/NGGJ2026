using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerContoller : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distanceToFloor = 1f;

    private Rigidbody2D rb;
    private Vector3 moveInput;
    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("MoveInput : " +moveInput);
        
        velocity.x = moveInput.x * speed;
    }

    public void Update()
    {
        rb.transform.Translate(velocity * Time.deltaTime);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 3f,  LayerMask.GetMask("Ground"));
        if (hit)
        {
            rb.transform.position = hit.point + Vector2.up * distanceToFloor;
        }
    }
}
