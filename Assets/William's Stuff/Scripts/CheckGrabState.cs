using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class CheckGrabState : MonoBehaviour
{
    private HandGrabInteractable grabInteractable; // The HandGrabInteractable component
    private bool isGrabbed = false;               // Tracks if the object is currently grabbed

    void Awake()
    {
        // Get the HandGrabInteractable component
        grabInteractable = GetComponentInChildren<HandGrabInteractable>();

        if (grabInteractable == null)
        {
            Debug.LogError("HandGrabInteractable component not found on this object!");
            return;
        }

        // Subscribe to grab state changes
        grabInteractable.WhenStateChanged += HandleGrabStateChange;
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.WhenStateChanged -= HandleGrabStateChange;
        }
    }

    private void HandleGrabStateChange(InteractableStateChangeArgs args)
    {
        if (args.NewState == InteractableState.Select)
        {
            isGrabbed = true;
        }
        else if (args.PreviousState == InteractableState.Select)
        {
            isGrabbed = false;
        }
    }

    public bool IsGrabbed()
    {
        return isGrabbed;
    }
}
