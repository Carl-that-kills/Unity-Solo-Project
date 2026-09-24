using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public PlayerController player;
    public WeaponsBehavior WeaponsBehavior;

    public Image Healthbar;
    public TextMeshProUGUI TextAmmo;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        Healthbar = GameObject.Find("Healthbar").GetComponent<Image>();

        TextAmmo = GameObject.Find("TextAmmo").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.fillAmount = (float)player.Health / (float)player.maxHealth;

        if (player.currentWeapon)
        {
                TextAmmo.text = "Ammo" + player.currentWeapon.clip + "/" + player.currentWeapon.clipSize;
        }
        else
        {
            TextAmmo.text = "";
        }
    }
}
