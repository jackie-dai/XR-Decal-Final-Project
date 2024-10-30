using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCannonHead : MonoBehaviour
{
    private GameObject pivot, cannonHead;
    public GameObject testobj;

    void Start()
    {
        pivot = transform.Find("Pivot").gameObject;
        cannonHead = transform.Find("Cannon Head").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        setRotation(testobj.transform.position);
    }

    public void setRotation(Vector3 point)
    {
        Vector3 pivotPosition = pivot.transform.position;
        Vector3 direction = (point - pivotPosition).normalized;

        // Calculate the target rotation, aligning the cannon's down vector with the target direction
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Set the cannon head's current rotation to match the target rotation around the pivot
        cannonHead.transform.position = pivotPosition;
        cannonHead.transform.rotation = targetRotation;

        // Use RotateAround to rotate the cannon head around the pivot point, preserving the orientation to the target
        Vector3 rotationAxis = Vector3.Cross(Vector3.down, direction); // Adjusts rotation axis to point towards the target
        float angle = Vector3.Angle(Vector3.down, direction); // Get the angle needed to point cannonhead towards the target
        cannonHead.transform.RotateAround(pivotPosition, rotationAxis, angle);

    }
}
