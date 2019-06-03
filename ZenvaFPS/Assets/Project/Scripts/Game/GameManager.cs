using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ammoText;
    public Text healthText;
    public Text enemiesText;
    public Text infoText;
    public bool gameOver;

    private Player player;
    private List<Enemy> enemies;
    private float resetTimer = 3f;

    private void Awake()
    {
        enemies = new List<Enemy>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (enemies.Count == 0)
        {
            UpdateInfoText("You win!!!");
            gameOver = true;
        }

        if (player.Dead)
        {
            UpdateInfoText("You lose!!!");
            gameOver = true;
        }

        if (gameOver)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    public void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + player.Ammo;
    }

    public void UpdateHealthText()
    {
        healthText.text = "Health: " + player.Health;
    }

    public void UpdateEnemiesText()
    {
        enemiesText.text = "Enemies: " + enemies.Count;
    }

    public void UpdateInfoText(string message)
    {
        infoText.text = message;
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
