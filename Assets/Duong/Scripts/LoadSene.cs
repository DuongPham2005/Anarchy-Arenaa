using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSene : MonoBehaviour
{
    private float timer = 0f;
    private float delayTime = 3f; // 5 seconds delay

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= delayTime)
        {
            // Get the current scene index and load the next one
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            
            // Check if there is a next scene
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                // If there's no next scene, load the first scene (optional)
                SceneManager.LoadScene(0);
            }
        }
    }
}
