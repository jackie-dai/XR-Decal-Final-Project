using UnityEngine;
using Oculus.Interaction;

public class PokeCannon : MonoBehaviour
{
    private PokeInteractable pokeInteractable;

    [SerializeField]
    private float doublePokeTime = 0.3f; // Maximum time between pokes to register as a double poke

    private float lastPokeTime = 0f; // Tracks the time of the last poke

    void Awake()
    {
        pokeInteractable = GetComponent<PokeInteractable>();

        if (pokeInteractable == null)
        {
            Debug.LogError("PokeInteractable component not found on this object!");
            return;
        }

        pokeInteractable.WhenStateChanged += HandlePokeStateChange;
    }

    private void OnDestroy()
    {
        if (pokeInteractable != null)
        {
            pokeInteractable.WhenStateChanged -= HandlePokeStateChange;
        }
    }

    private void HandlePokeStateChange(InteractableStateChangeArgs args)
    {
        if (args.NewState == InteractableState.Select)
        {
            DetectDoublePoke();
        }
    }

    private void DetectDoublePoke()
    {
        float currentTime = Time.time;

        // Check if this poke is within the double poke time window
        if (currentTime - lastPokeTime <= doublePokeTime)
        {
            Debug.Log("Double poke detected!");
            OnDoublePokeDetected();
        }

        lastPokeTime = currentTime;
    }

    private void OnDoublePokeDetected()
    {
        transform.parent.gameObject.GetComponent<LaunchCannon>().launch();
    }
}
