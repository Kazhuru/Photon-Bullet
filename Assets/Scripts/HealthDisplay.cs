using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI healthText;
    GameSession gameSession;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        healthText = gameObject.GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        int playerHP = player.GetCurrentHealth();
        if (playerHP < 0)
            playerHP = 0;
        healthText.text = "HP: " + player.GetCurrentHealth();
    }
}
