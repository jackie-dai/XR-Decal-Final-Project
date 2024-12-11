//using UnityEngine;

//public class SmartAxisScaling : MonoBehaviour
//{
//    public Transform leftHand;  // Assign the left hand transform in the Inspector
//    public Transform rightHand; // Assign the right hand transform in the Inspector

//    private Vector3 initialScale;       // Initial scale of the object
//    private Vector3 initialHandVector; // Vector between the hands at the start of the grab
//    private string dominantAxis;       // The axis along which scaling occurs
//    private bool isScaling = false;

//    void Update()
//    {
//        if (IsGrabbing(leftHand) && IsGrabbing(rightHand))
//        {
//            if (!isScaling)
//            {
//                BeginScaling(); // Initialize scaling variables
//            }

//            PerformScaling(); // Lock non-dominant axes and scale along the dominant axis
//        }
//        else if (isScaling)
//        {
//            EndScaling(); // Reset after scaling ends
//        }
//    }

//    private bool IsGrabbing(Transform hand)
//    {
//        // Check if the hand is grabbing the object
//        Collider[] colliders = Physics.OverlapSphere(hand.position, 0.05f);
//        foreach (var collider in colliders)
//        {
//            if (collider.gameObject == this.gameObject)
//            {
//                return true;
//            }
//        }
//        return false;
//    }

//    private void BeginScaling()
//    {
//        isScaling = true;
//        initialScale = transform.localScale;
//        initialHandVector = rightHand.position - leftHand.position;

//        // Determine the dominant axis based on the largest component of the hand vector
//        Vector3 absoluteVector = new Vector3(
//            Mathf.Abs(initialHandVector.x),
//            Mathf.Abs(initialHandVector.y),
//            Mathf.Abs(initialHandVector.z)
//        );

//        if (absoluteVector.x > absoluteVector.y && absoluteVector.x > absoluteVector.z)
//        {
//            dominantAxis = "x";
//        }
//        else if (absoluteVector.y > absoluteVector.x && absoluteVector.y > absoluteVector.z)
//        {
//            dominantAxis = "y";
//        }
//        else
//        {
//            dominantAxis = "z";
//        }

//        Debug.Log($"Scaling started. Dominant Axis: {dominantAxis}");
//    }

//    private void PerformScaling()
//    {
//        Debug.Log("performscaling function called");
//        Vector3 currentHandVector = rightHand.position - leftHand.position;
//        float scaleDelta = currentHandVector.magnitude / initialHandVector.magnitude;

//        // Scale only along the dominant axis while locking others
//        switch (dominantAxis)
//        {
//            case "x":
//                transform.localScale = new Vector3(
//                    initialScale.x * scaleDelta, // Scale X
//                    initialScale.y,             // Lock Y
//                    initialScale.z              // Lock Z
//                );
//                break;

//            case "y":
//                transform.localScale = new Vector3(
//                    initialScale.x,             // Lock X
//                    initialScale.y * scaleDelta, // Scale Y
//                    initialScale.z              // Lock Z
//                );
//                break;

//            case "z":
//                transform.localScale = new Vector3(
//                    initialScale.x,             // Lock X
//                    initialScale.y,             // Lock Y
//                    initialScale.z * scaleDelta  // Scale Z
//                );
//                break;
//        }
//    }

//    private void EndScaling()
//    {
//        // Update initial scale to the current scale after release
//        initialScale = transform.localScale;
//        isScaling = false;

//        Debug.Log("Scaling ended. Initial scale reset.");
//    }
//}
