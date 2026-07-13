using System;
using UnityEngine;

public class TakeDown : MonoBehaviour, Takedownable
{
    PlayerController player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }

    public void TakeDownAction()
    {
        throw new System.NotImplementedException();
    }

    public void SetActiveInteractable(bool isActive)
    {
        throw new System.NotImplementedException();
    }
}

public interface Takedownable
{
    public void TakeDownAction();
    public void SetActiveInteractable(bool isActive);
}
