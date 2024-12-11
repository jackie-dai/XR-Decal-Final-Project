using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class PoseDetector : MonoBehaviour
{
    public List<ActiveStateSelector> poses;
    public GameObject roomUI;
    private bool disabled = true;

    void Start()
    {

    }

    public void ThumbsUp()
    {
        Debug.Log("thumbs up!!!");
    }

    public void EnableRoomUI()
    {
        if (disabled)
        {
            roomUI.SetActive(true);
            disabled = false;
        }
        else
        {
            roomUI.SetActive(false);
            disabled = true;
        }
    }
}
