using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public static GameManagerScript Instance;
    
    [SerializeField] GameMenu[] GMenus;

     void Awake()
    {
        Instance = this;
    }
    public void OpenMenu(string menuName)
    {
        for(int i = 0; i< GMenus.Length ;i++)
        {
            if (GMenus[i].menuName == menuName)
            {
                OpenMenu(GMenus[i]);
                
            }
            else if (GMenus[i].open)
            {
                CloseMenu(GMenus[i]);
            }
        }
    }


    public void OpenMenu(GameMenu gmenu)
    {
        for (int i = 0; i < GMenus.Length; i++)
        {
            
            if (GMenus[i].open)
            {
                CloseMenu(GMenus[i]);
            }
        }
        gmenu.Open();
    }

    public void CloseMenu(GameMenu menu)
    {
        menu.Close();
    }

}
