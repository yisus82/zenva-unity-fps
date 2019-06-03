using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    public GameObject bulletPrefab;
    public int bulletAmount = 10;

    private List<GameObject> bullets;

    private void Awake()
    {
        Instance = this;

        bullets = new List<GameObject>(bulletAmount);

        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.transform.SetParent(transform);
            bulletInstance.SetActive(false);
            bullets.Add(bulletInstance);
        }
    }

    public GameObject GetBullet()
    {
        foreach (var bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject bulletInstance = Instantiate(bulletPrefab);
        bulletInstance.transform.SetParent(transform);
        bullets.Add(bulletInstance);
        return bulletInstance;
    }
}
