using UnityEngine;

public class ConstantDistanceFromWall : MonoBehaviour
{
    public Transform wallObject;  
    public float distanceFromWall = 1.0f; 
    public float rayMaxDistance = 100f;
    public int maxIterations = 10; 

    private Vector3 rayDirection; 

    void Start()
    {

    }

    void Update()
    {
        UpdatePositionWithRaycast();
    }

    private void UpdatePositionWithRaycast()
    {
        // Calculate the ray direction dynamically
        rayDirection = (wallObject.position - transform.position).normalized;

        // Initialize variables for the raycast
        Vector3 currentPosition = transform.position;
        int iterations = 0;
        bool wallHit = false;

        while (iterations < maxIterations && !wallHit)
        {
            // Perform a raycast from the current position toward the wall
            Ray ray = new Ray(currentPosition, rayDirection);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayMaxDistance))
            {
                if (hit.transform == wallObject)
                {
                    Vector3 hitPoint = hit.point;
                    Vector3 offset = rayDirection * -distanceFromWall; 
                    Vector3 targetPosition = hitPoint + offset;

                    transform.position = targetPosition;

                    wallHit = true;
                }
                else
                {
                    currentPosition = hit.point + rayDirection * 0.01f; // Small offset to prevent re-hitting the same object
                }
            }
            else
            {
                // No object was hit stop the loop
                Debug.LogWarning("Ray no hit");
                break;
            }

            iterations++;
        }

        if (!wallHit)
        {
            Debug.LogWarning("Ray failed to hit");
        }
    }
}
