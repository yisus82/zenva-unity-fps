using UnityEngine;

public class Enemy : Hazard
{
    public int health = 5;

    public bool Dead { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet != null && bullet.ShotByPlayer)
        {
            TakeDamage(bullet);
        }
    }

    private void TakeDamage(Hazard hazard)
    {
        health -= hazard.damage;
        if (health <= 0 && !Dead)
        {
            OnKill();
            Dead = true;
        }

        if (hazard is Bullet)
        {
            hazard.gameObject.SetActive(false);
        }
        else
        {
            Destroy(hazard.gameObject);
        }
    }

    protected virtual void OnKill() { }
}
