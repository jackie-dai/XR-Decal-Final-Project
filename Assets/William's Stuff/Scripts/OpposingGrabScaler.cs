using UnityEngine;

public class OpposingGrabScaler : MonoBehaviour
{
    public Transform wallObject; 

    private CheckGrabState leftTracker;
    private CheckGrabState rightTracker;
    private CheckGrabState upTracker;
    private CheckGrabState downTracker;
    private CheckGrabState forwardTracker;
    private CheckGrabState backwardTracker;

    private Vector3 initialWallSize;   
    private float initialDistance;     
    private bool isScaling = false;

    void Start()
    {
        leftTracker = FindSiblingTracker("Left");
        rightTracker = FindSiblingTracker("Right");
        upTracker = FindSiblingTracker("Up");
        downTracker = FindSiblingTracker("Down");
        forwardTracker = FindSiblingTracker("Forward");
        backwardTracker = FindSiblingTracker("Backward");

        if (!leftTracker || !rightTracker || !upTracker || !downTracker || !forwardTracker || !backwardTracker)
        {
            Debug.LogError("One or more sibling grab trackers are missing!");
        }
    }

    void Update()
    {
        if (leftTracker.IsGrabbed() && rightTracker.IsGrabbed())
        {
            HandleScaling(leftTracker.transform, rightTracker.transform, "x");
        }
        else if (upTracker.IsGrabbed() && downTracker.IsGrabbed())
        {
            HandleScaling(upTracker.transform, downTracker.transform, "y");
        }
        else if (forwardTracker.IsGrabbed() && backwardTracker.IsGrabbed())
        {
            HandleScaling(forwardTracker.transform, backwardTracker.transform, "z");
        }
        else if (isScaling)
        {
            EndScaling();
        }
    }

    private CheckGrabState FindSiblingTracker(string name)
    {
        Transform sibling = transform.Find(name);
        if (sibling != null)
        {
            return sibling.GetComponent<CheckGrabState>();
        }
        return null;
    }

    private void HandleScaling(Transform grabbed1, Transform grabbed2, string axis)
    {
        if (!isScaling)
        {
            StartScaling(grabbed1, grabbed2);
        }

        PerformScaling(grabbed1, grabbed2, axis);
    }

    private void StartScaling(Transform grabbed1, Transform grabbed2)
    {
        isScaling = true;
        initialWallSize = wallObject.localScale; 
        initialDistance = Vector3.Distance(grabbed1.position, grabbed2.position); 
    }

    private void PerformScaling(Transform grabbed1, Transform grabbed2, string axis)
    {
        
        float currentDistance = Vector3.Distance(grabbed1.position, grabbed2.position);
        float scaleMultiplier = currentDistance / initialDistance;

        Vector3 newSize = initialWallSize;

        
        if (axis == "x") newSize.x *= scaleMultiplier;
        else if (axis == "y") newSize.y *= scaleMultiplier;
        else if (axis == "z") newSize.z *= scaleMultiplier;

        wallObject.localScale = newSize;
    }

    private void EndScaling()
    {
        isScaling = false;
        initialWallSize = wallObject.localScale; 
    }
}
