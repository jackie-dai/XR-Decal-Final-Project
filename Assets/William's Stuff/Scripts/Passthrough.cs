using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class Passthrough : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        GetComponent<SphereCollider>().isTrigger = false;
    }
}







