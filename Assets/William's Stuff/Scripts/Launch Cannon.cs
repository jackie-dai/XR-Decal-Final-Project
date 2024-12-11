using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCannon : MonoBehaviour
{
    public GameObject cannonBall; 
    public bool testLaunch = false;
    public GameObject testobj;

    public float cannonLength = 0.01f;  

    void Start()
    {
        //launch();
    }

    // Update is called once per frame
    void Update()
    {
        //if (testLaunch)
        //{
        //    launch();
        //    testLaunch = false;
        //}
    }

    public void launch()
    {

        if (this.gameObject.tag != "Instantiated") return;

        testobj.GetComponent<SphereCollider>().isTrigger = true;

        Vector3 forwardDir = GetComponent<RotateCannonHead>().getDirection();

        GameObject cannonHead = transform.Find("Cannon Head").gameObject;
        Vector3 frontPos = cannonHead.transform.position + forwardDir * cannonLength;

        GameObject spawnedCannonBall = Instantiate(cannonBall, frontPos, Quaternion.identity);

        spawnedCannonBall.GetComponent<Rigidbody>().AddForce(forwardDir * 5, ForceMode.Impulse);
    }
}
