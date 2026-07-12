using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distanceToFloor = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 xClamp;

    private Rigidbody2D rb;
    private Vector3 moveInput;
    private Vector3 velocity;
    [HideInInspector] public bool isHidden = false;
    [HideInInspector] public bool isDisguised = false;
    
    private List<Interactable> interactables  = new List<Interactable>();
    private List<string> ItemHeld = new List<string>();
    private Interactable closestInteractable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(isHidden) return; //can't move when hidden
        
        moveInput = context.ReadValue<Vector2>();
        //Debug.Log("MoveInput : " +moveInput);
        
        velocity.x = moveInput.x * speed;
        
    }

    public void Interact(InputAction.CallbackContext context)
    {
        //Debug.Log("Interact");
        //Debug.Log("isClosestValid : " + (closestInteractable!=null));
        if (context.performed && closestInteractable != null)
        {
            closestInteractable.Interact();
        }
    }

    public void Update()
    {
        rb.transform.Translate(velocity * Time.deltaTime);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 3f,  LayerMask.GetMask("Ground"));
        if (hit)
        {
            rb.transform.position = hit.point + Vector2.up * distanceToFloor;
        }
        
        if (transform.position.x < xClamp.x)
        {
            transform.position = new Vector3(xClamp.x, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xClamp.y)
        {
            transform.position = new Vector3(xClamp.y, transform.position.y, transform.position.z);
        }
        
        CheckCloserInteractable();
        
        
    }

    private void CheckCloserInteractable()
    {
        if (interactables.Count > 0)
        {
            if (interactables.Count > 1)
            {
                Interactable closestInteractable =  interactables[0];
                foreach (Interactable interactable in interactables)
                {
                    if (interactable.DistanceFromPlayer() < closestInteractable.DistanceFromPlayer())
                    {
                        closestInteractable = interactable;
                    }
                }
            }
            else closestInteractable = interactables[0];
        }
        else closestInteractable = null;

        foreach (Interactable interactable in interactables)
        {
            interactable.SetActiveInteractable(interactable==closestInteractable);
        }
    }

    public void AddInteractable(Interactable interactable)
    {
        interactables.Add(interactable);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        interactables.Remove(interactable);
        if (closestInteractable == interactable)
        {
            interactable.SetActiveInteractable(false);
        }
    }
    
    //SpecifiqueInteraction
    public void Hide(Vector2 pos)
    {
        if (isHidden)
        {
            isHidden = false;
            spriteRenderer.sortingLayerName = "Default";
        }
        else 
        {
            isHidden = true;
            spriteRenderer.sortingLayerName = "Hidden";
            velocity = Vector2.zero;
            transform.position = pos;
        }
    }

    public void AddItem(PickableInteractable pickableInteractable)
    {
        ItemHeld.Add(pickableInteractable.itemID);
        RemoveInteractable(pickableInteractable);
    }
}
