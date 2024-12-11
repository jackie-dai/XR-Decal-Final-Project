using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObj;

    void Start()
    {
        Instantiate(spawnObj, this.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        Instantiate(spawnObj, this.transform);

        foreach (Transform go in spawnObj.transform) {
            go.gameObject.tag = "Instantiated";
        }

        other.gameObject.tag = "Instantiated";
    }
}
