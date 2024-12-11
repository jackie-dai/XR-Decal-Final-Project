using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnTable : MonoBehaviour
{
    MeshRenderer tableRenderer;
    bool reallyLateStart = true;

    void Start()
    {

    }

    private void LateUpdate()
    {
        if (reallyLateStart) {
            reallyLateStart = false;

            //tableRenderer = GameObject.Find("Scene Manager").GetComponent<SceneManager>().tableRenderers[0];

            tableRenderer = GameObject.Find("TABLE").GetComponent<MeshRenderer>();

            Debug.Log("Placed on table:" + tableRenderer);

            PlaceObjectOnTable();
        }
    }
    private void PlaceObjectOnTable()
    {
        Bounds tableBounds = tableRenderer.bounds;

        Vector3 tableTopCenter = tableBounds.center;
        tableTopCenter.y = tableBounds.max.y;
        transform.position = tableTopCenter;
    }
}
