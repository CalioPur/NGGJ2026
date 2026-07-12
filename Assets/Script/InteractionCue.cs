using System;
using UnityEngine;

public class InteractionCue : MonoBehaviour
{
    public GameObject InteractionCueClavier;
    public GameObject InteractionCueController;

    private void Start()
    {
        PlayerInputDeviceDetector.onActiveDeviceChanged += RefreshCue;
    }

    private void RefreshCue(ActiveDevice obj)
    {
        if (InteractionCueClavier.activeSelf || InteractionCueController.activeSelf)
        {
            HideInteractionCue();
            ShowInteractionCue();
        }
    }

    public void ShowInteractionCue()
    {
        GetCorrectInteractionCue().SetActive(true);
    }

    public void HideInteractionCue()
    {
        InteractionCueClavier.SetActive(false);
        InteractionCueController.SetActive(false);
    }
    
    

    private GameObject GetCorrectInteractionCue()
    {
        switch (PlayerInputDeviceDetector.instance.activeDevice)
        {
            case ActiveDevice.Keyboard:
                return InteractionCueClavier;
                break;
            case ActiveDevice.Gamepad:
                return InteractionCueController;
                break;
        }
        return null;
    }
}
