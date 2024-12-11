using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; 

public class TogglePausePanel : MonoBehaviour
{
    public GameObject pausePanel;
    private InputAction pauseAction;

    void Awake()
    {
        pauseAction = new InputAction(binding: "<Keyboard>/escape");
        pauseAction.performed += _ => TogglePause();
        pauseAction.Enable();
    }

    void TogglePause()
    {
        //Debug.Log("Escape key pressed!");
        pausePanel.SetActive(!pausePanel.activeSelf);
    }

    void OnDestroy() 
    {
        pauseAction.Disable();
    }
}
