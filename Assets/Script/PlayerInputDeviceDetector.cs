using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDeviceDetector : MonoBehaviour
{
    public static PlayerInputDeviceDetector instance { get; private set; }
    public static Action<ActiveDevice> onActiveDeviceChanged;
    [SerializeField] PlayerInput playerInput;
    
    ActiveDevice oldActiveDevice = ActiveDevice.Keyboard;
    public ActiveDevice activeDevice = ActiveDevice.Keyboard;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        oldActiveDevice = activeDevice;
        
        if (playerInput.currentControlScheme == "Gamepad")
        {
            activeDevice = ActiveDevice.Gamepad;
        }
        else if (playerInput.currentControlScheme == "Keyboard")
        {
            activeDevice = ActiveDevice.Keyboard;
        }

        if (activeDevice != oldActiveDevice)
        {
            onActiveDeviceChanged?.Invoke(activeDevice);
            print("ACTIVE DEVICE CHANGED" +  activeDevice);
        }
    }
}

public enum ActiveDevice
{
    Keyboard,
    Gamepad,
}
