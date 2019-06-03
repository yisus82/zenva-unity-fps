using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject bulletSpawnPoint;

    [Header("Gameplay")]
    public int initialHealth = 100;
    public int initialAmmo = 12;
    public float knockbackForce = 100f;
    public float hurtDuration = 0.5f;
    public float minY = -10f;

    public int Health { get; private set; }
    public int Ammo { get; private set; }
    public bool Dead { get; private set; }

    private GameManager gameManager;
    private bool isHurt;

    private void Start()
    {
        Health = initialHealth;
        Ammo = initialAmmo;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameManager.UpdateAmmoText();
    }

    private void Update()
    {
        if (gameManager.gameOver)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Ammo > 0)
            {
                GameObject bulletObject = ObjectPoolManager.Instance.GetBullet();
                bulletObject.GetComponent<Bullet>().ShotByPlayer = true;
                bulletObject.transform.position = bulletSpawnPoint.transform.position;
                bulletObject.transform.forward = bulletSpawnPoint.transform.forward;
                Ammo--;
                gameManager.UpdateAmmoText();
            }
        }

        if (transform.position.y < minY)
        {
            Dead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AmmoCrate ammoCrate = other.GetComponent<AmmoCrate>();
        if (ammoCrate != null)
        {
            Ammo += ammoCrate.ammo;
            gameManager.UpdateAmmoText();
            Destroy(ammoCrate.gameObject);
        }

        HealthCrate healthCrate = other.GetComponent<HealthCrate>();
        if (healthCrate != null)
        {
            Health += healthCrate.health;
            if (Health > 100)
            {
                Health = 100;
            }
            gameManager.UpdateHealthText();
            Destroy(healthCrate.gameObject);
        }

        if (!isHurt)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !enemy.Dead)
            {
                TakeDamage(enemy);
            }

            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null && !bullet.ShotByPlayer)
            {
                TakeDamage(bullet);
            }
        }
    }

    private void TakeDamage(Hazard hazard)
    {
        Health -= hazard.damage;
        if (Health <= 0 && !Dead)
        {
            Health = 0;
            OnKill();
            Dead = true;
        }

        if (hazard is Bullet)
        {
            hazard.gameObject.SetActive(false);
        }

        gameManager.UpdateHealthText();
        Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
        Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
        GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockbackForce);
        isHurt = true;
        StartCoroutine(Hurting());
    }

    private void OnKill() { }

    private IEnumerator Hurting()
    {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
    }
}
