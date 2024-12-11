using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoseDetection : MonoBehaviour
{
    [SerializeField] private OVRHand leftHand;
    [SerializeField] private OVRHand rightHand;

    void Update()
    {
        if (leftHand != null)
        {
            DetectPose(leftHand, "Left Hand");
        }

        if (rightHand != null)
        {
            DetectPose(rightHand, "Right Hand");
        }
    }

    void DetectPose(OVRHand hand, string handName)
    {
        float thumbPinch = hand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb);
        float indexPinch = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        float middlePinch = hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        float ringPinch = hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        float pinkyPinch = hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);

        if ((indexPinch > 0.7f && middlePinch > 0.7f &&
            ringPinch < 0.2f && pinkyPinch < 0.2f))
        {
            Debug.Log($"{handName}: SCISSORS pose detected!");
            
        }

    }
}
