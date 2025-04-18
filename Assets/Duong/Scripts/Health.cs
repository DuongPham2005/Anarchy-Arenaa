using UnityEngine;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;
    public bool islocalPlayer;

    public RectTransform healthBar;
    private float originalHealthBarSize;

    [Header("UI")]
    public TextMeshProUGUI healthText;


    private void Start()
    {
        originalHealthBarSize = healthBar.sizeDelta.x;
    }

    private void Update()
    {
        // healthBar.sizeDelta = new Vector2(originalHealthBarSize * health /100f, healthBar.sizeDelta.y);
    }
    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.sizeDelta = new Vector2(originalHealthBarSize * health / 100f, healthBar.sizeDelta.y);


        healthText.text= health.ToString();

        if (health <= 0)
        {
            if (islocalPlayer)
            {
                RoomManager.instance.SpawnPlayer();

                RoomManager.instance.deaths++;
                RoomManager.instance.SetHashes();

            }





            Destroy(gameObject);
        }
    }
}
