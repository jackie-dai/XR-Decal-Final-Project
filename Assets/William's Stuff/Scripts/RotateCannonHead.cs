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

        //pivot.transform.position = cannonHead.transform.position - cannonHead.transform.forward * 0.5f;
    }

    void Update()
    {
        setRotation(testobj.transform.position);
    }

    public void setRotation(Vector3 targetPosition)
    {
        Vector3 pivotPosition = pivot.transform.position;
        Vector3 direction = (targetPosition - /*pivotPosition*/ transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.Cross(cannonHead.transform.right, -direction), -direction);

        //cannonHead.transform.position = pivotPosition; // Ensure cannon head stays centered on the pivot
        cannonHead.transform.rotation = targetRotation;

        Debug.DrawLine(pivot.transform.position, testobj.transform.position, Color.red); 
        Debug.DrawRay(cannonHead.transform.position, -cannonHead.transform.up * 5, Color.green); 

    }

    public Vector3 getDirection()
    {
        return (testobj.transform.position - transform.position).normalized;
    }

    public Vector3 pivotPosition()
    {
        return pivot.transform.position;
    }

}
