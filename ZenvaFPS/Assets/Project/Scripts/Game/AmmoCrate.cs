using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public int ammo = 12;

    private Transform containerTransform;

    private void Start()
    {
        containerTransform = transform.Find("Container");
    }

    private void Update()
    {
        containerTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
