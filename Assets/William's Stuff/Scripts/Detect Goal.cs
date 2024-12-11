using UnityEngine;

public class DetectGoal : MonoBehaviour
{
    public GameObject confettiObject;
    public float requiredTimeInTrigger = 3.0f; 

    private float timeInTrigger = 0f; 
    private bool cannonballInTrigger = false;
    private GameObject cannonball; 
    private bool confettiLaunched = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannonball"))
        {
            cannonballInTrigger = true;
            cannonball = other.gameObject; 
            Debug.Log("Cannonball entered the trigger!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cannonball"))
        {
            cannonballInTrigger = false;
            timeInTrigger = 0f;
            cannonball = null;
            Debug.Log("Cannonball left the trigger!");
        }
    }

    private void Update()
    {
        if (cannonballInTrigger)
        {
            timeInTrigger += Time.deltaTime;

            if (timeInTrigger >= requiredTimeInTrigger && !confettiLaunched)
            {
                LaunchConfetti();
                DestroyCannonball();
            }
        }
    }

    private void LaunchConfetti()
    {
        if (confettiObject != null && cannonball != null)
        {
            confettiObject.transform.position = cannonball.transform.position;

            var confettiParticle = confettiObject.GetComponent<ParticleSystem>();
            if (confettiParticle != null && !confettiParticle.isPlaying)
            {
                confettiObject.SetActive(true);
                confettiParticle.Play();
            }

            confettiLaunched = true;
            Debug.Log("Confetti launched at cannonball's position!");
        }
    }

    private void DestroyCannonball()
    {
        if (cannonball != null)
        {
            Destroy(cannonball);
            Debug.Log("Cannonball destroyed!");
        }
    }
}
