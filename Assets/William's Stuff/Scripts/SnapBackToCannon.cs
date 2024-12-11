using UnityEngine;
using Oculus.Interaction.HandGrab;

public class SnapBackToCannon : MonoBehaviour
{
    public Transform cannonHead;  
    private HandGrabInteractable handGrabInteractable;

    private bool isGrabbed = false;
    public GameObject spherePos;

    void Awake()
    {
        handGrabInteractable = GetComponentInChildren<HandGrabInteractable>();
    }

    void Update()
    {
        if (handGrabInteractable != null)
        {
            bool currentlyGrabbed = handGrabInteractable.Interactors.Count > 0;

            if (currentlyGrabbed && !isGrabbed)
            {
                OnGrabBegin();
            }
            else if (!currentlyGrabbed && isGrabbed)
            {
                OnGrabEnd();
            }

            isGrabbed = currentlyGrabbed;
        }
    }

    private void OnGrabBegin()
    {
        Debug.Log("Object grabbed");
    }

    private void OnGrabEnd()
    {
        Debug.Log("Object released");

        SnapBackToCannonHead();
    }

    public void SnapBackToCannonHead()
    {
        // Temporarily disable cannon rotation adjustments to avoid conflicts during snapback
        //cannonHead.GetComponent<RotateCannonHead>().enabled = false;

        // Get the direction vector from the RotateCannonHead script

        //Vector3 direction = cannonHead.GetComponent<RotateCannonHead>().getDirection();
        //transform.position = cannonHead.GetComponent<RotateCannonHead>().pivotPosition() + direction * 0.25f;

        // Snap the object to the cannon head's position along the direction vector

        transform.position = spherePos.transform.position;

        // Re-enable the cannon rotation adjustments
        //cannonHead.GetComponent<RotateCannonHead>().enabled = true;
    }
}
