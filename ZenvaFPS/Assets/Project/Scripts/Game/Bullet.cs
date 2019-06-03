using UnityEngine;

public class Bullet : Hazard
{
    public float speed = 8f;
    public float lifeSpan = .5f;

    private float lifeTime = 0f;

    public bool ShotByPlayer { get; set; }

    private void OnEnable()
    {
        lifeTime = lifeSpan;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
