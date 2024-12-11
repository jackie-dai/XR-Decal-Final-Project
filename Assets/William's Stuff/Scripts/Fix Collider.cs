using UnityEngine;

public class FixCollider : MonoBehaviour
{
    private BoxCollider boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider not found on this object!");
        }
    }

    void Update()
    {
        if (boxCollider != null)
        {
            // Update the collider's center to match the object's local position
            boxCollider.center = transform.InverseTransformPoint(transform.position);

            // Optionally, update the collider's size to match the object's local scale
            //boxCollider.size = new Vector3(
            //    transform.lossyScale.x,
            //    transform.lossyScale.y,
            //    transform.lossyScale.z
            //);
        }
    }
}
