using UnityEngine;
using System.Collections;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class LockCannonRotationOnGrab : MonoBehaviour
{
    [SerializeField]
    private Rigidbody cannonRigidbody; 

    private HandGrabInteractable grabInteractable; 
    private Quaternion initialRotation;
    private bool isGrabbed = false; 
    public GameObject spherePos;

    void Awake()
    {
        
        if (cannonRigidbody == null)
        {
            Debug.LogError("Cannon Rigidbody is not assigned!");
            return;
        }

        grabInteractable = GetComponentInChildren<HandGrabInteractable>();

        if (grabInteractable == null)
        {
            Debug.LogError("HandGrabInteractable component not found on this object or its children!");
            return;
        }

        Debug.Log("HandGrabInteractable successfully assigned.");

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
            OnGrabBegin();
        }
        else if (args.PreviousState == InteractableState.Select) 
        {
            OnGrabEnd();
        }
    }

    private void LateUpdate()
    {
        if (isGrabbed)
        {
            transform.rotation = initialRotation;
            GetComponent<RotateCannonHead>().testobj.transform.position = spherePos.transform.position;
            //Vector3 offset = transform.forward * 0.25f; // Adjust the distance as needed
            //GetComponent<RotateCannonHead>().testobj.transform.position = transform.position + offset;
        }
    }

    private void OnGrabBegin()
    {
        initialRotation = transform.rotation;
        isGrabbed = true;

        //GetComponent<RotateCannonHead>().enabled = false;

        Debug.Log("Cannon rotation locked.");
    }

    private void OnGrabEnd()
    {
        isGrabbed = false;

        // Call SnapBackToCannonHead from the test object
        //GetComponent<RotateCannonHead>().testobj.GetComponent<SnapBackToCannon>().SnapBackToCannonHead();

        GetComponent<RotateCannonHead>().testobj.transform.position = spherePos.transform.position;

        // Re-enable the RotateCannonHead component
        //GetComponent<RotateCannonHead>().enabled = true;

        Debug.Log("Cannon rotation unlocked.");
    }
}
