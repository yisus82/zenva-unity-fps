using UnityEngine;

public class HealthCrate : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public int health = 20;

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
