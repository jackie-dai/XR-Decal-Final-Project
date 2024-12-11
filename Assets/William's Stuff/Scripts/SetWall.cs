using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void turnWallsStatic()
    {
        Debug.Log("Thumbs Up to turn walls static!");
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (var g in allObjects) {
            if (g.tag == "Instantiated" && g.name == "Wall") {
                CleanGameObject(g);
            }
        }

    }

    private void CleanGameObject(GameObject target)
    {
        Component[] components = target.GetComponents<Component>();

        foreach (Component component in components)
        {
            // Check if the component is NOT a MeshRenderer or Collider
            if (!(component is MeshRenderer) && !(component is Collider))
            {
                Destroy(component);
                Debug.Log($"Removed component: {component.GetType().Name}");
            }
        }

        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = target.AddComponent<Rigidbody>();
            rb.useGravity = true;
            Debug.Log("Added Rigidbody and enabled gravity.");
        }
        else
        {
            Debug.Log("Rigidbody already exists, ensuring gravity is enabled.");
            rb.useGravity = true;
        }
    }
}
