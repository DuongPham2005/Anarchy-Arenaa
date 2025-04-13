using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public string menuName;
    public bool open;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Open()
    {

        open = true;
        gameObject.SetActive(true);
    }
    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
}
