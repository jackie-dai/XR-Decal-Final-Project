using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // Quit the application
        Application.Quit();

        // If you're testing in the Unity Editor, stop the play mode
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}