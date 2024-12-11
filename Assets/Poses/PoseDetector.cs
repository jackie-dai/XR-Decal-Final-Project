using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class PoseDetector : MonoBehaviour
{
    public List<ActiveStateSelector> poses;
    public TMPro.TextMeshProUGUI text;
    public GameObject roomUI;
    private bool disabled = true;

    void Start()
    {
        foreach (var item in poses)
        {
            Debug.Log(item.gameObject.name);
            item.WhenSelected += () => SetTextToPoseName(item.gameObject.name);
            //item.WhenSelected += () => SetTextToPoseName("");
        }

        text.text = "Waiting for pose";
    }

    private void SetTextToPoseName(string poseName)
    {
        text.text = poseName;
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
