using UnityEngine;

public class SongBG : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isCharacterSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        
        // Make sure we have an AudioSource component
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if character is spawned (you'll need to implement this check based on your game logic)
        if (!isCharacterSpawned && IsCharacterSpawned())
        {
            isCharacterSpawned = true;
            StopMusic();
        }
    }

    private bool IsCharacterSpawned()
    {
        // Implement your character spawn check logic here
        // For example, you might check for a specific GameObject or tag
        // This is just a placeholder - you'll need to adjust this based on your game's logic
        return GameObject.FindGameObjectWithTag("Player") != null;
    }

    private void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
