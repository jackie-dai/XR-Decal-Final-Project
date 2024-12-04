using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCannon : MonoBehaviour
{

    public GameObject cannonBall;
    public bool testLaunch = false;

    void Start()
    {
        launch();
    }

    // Update is called once per frame
    void Update()
    {
        if (testLaunch) {
            launch();
            testLaunch = false;
        }
    }

    private void launch()
    {
        Vector3 forwardDir = GetComponent<RotateCannonHead>().getDirection();
        Vector3 pos = transform.position + forwardDir;

        Instantiate(cannonBall, pos, Quaternion.identity);

        cannonBall.GetComponent<Rigidbody>().AddForce(forwardDir * 10);
    }
}
