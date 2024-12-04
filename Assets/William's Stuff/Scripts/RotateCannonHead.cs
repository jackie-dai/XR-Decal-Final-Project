using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCannonHead : MonoBehaviour
{
    private GameObject pivot, cannonHead;
    public GameObject testobj;

    void Start()
    {
        // Locate the pivot and cannon head in the hierarchy
        pivot = transform.Find("Pivot").gameObject;
        cannonHead = transform.Find("Cannon Head").gameObject;

        // Ensure the pivot is placed correctly at the back of the cannon head
        pivot.transform.position = cannonHead.transform.position - cannonHead.transform.forward * (cannonHead.GetComponent<Renderer>().bounds.size.z / 2);
    }

    void Update()
    {
        setRotation(testobj.transform.position);
    }

    public void setRotation(Vector3 targetPosition)
    {
        // Get the pivot position
        Vector3 pivotPosition = pivot.transform.position;

        // Calculate the direction from the pivot to the target
        Vector3 direction = (targetPosition - pivotPosition).normalized;

        // Compute the rotation that aligns -up (negative up vector) with the direction
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.Cross(cannonHead.transform.right, -direction), -direction);

        // Apply the rotation to the cannon head
        cannonHead.transform.position = pivotPosition; // Ensure cannon head stays centered on the pivot
        cannonHead.transform.rotation = targetRotation;

        Debug.DrawLine(pivot.transform.position, testobj.transform.position, Color.red); // Shows the direction to the target
        Debug.DrawRay(cannonHead.transform.position, -cannonHead.transform.up * 5, Color.green); // Shows the cannon's negative up vector

    }

    public Vector3 getDirection()
    {
        return (testobj.transform.position - pivot.transform.position).normalized;
    }
}
