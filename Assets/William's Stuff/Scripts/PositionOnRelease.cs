using UnityEngine;

public class PositionOnRelease : MonoBehaviour
{
    public GameObject targetObject; 
    private CheckGrabState grabState; 

    void Start()
    {
        grabState = GetComponent<CheckGrabState>();

        if (grabState == null)
        {
            Debug.LogError("CheckGrabState component not found");
        }
    }

    void Update()
    {
        if (!grabState.IsGrabbed() && targetObject != null)
        {
            transform.position = targetObject.transform.position;
        }
    }
}
