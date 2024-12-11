using UnityEngine;
//using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // Method to load a scene by its name
    public void LoadSceneByName(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); 
    }

    // Method to load a scene by its build index
    public void LoadSceneByIndex(int sceneIndex)
    {
         UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex); 
    }
}
