using UnityEngine;

public class RandomMovementController : MonoBehaviour
{
    public Vector3 Center;
    public float Radius = 1f;
    public float Speed = 1f;

    private Vector3 targetPosition;

    private void Start()
    {
        // Choose a random target position within the radius
        float angle = Random.Range(0f, 360f);
        float x = Center.x + Mathf.Cos(angle) * Radius;
        float y = Center.y + Mathf.Sin(angle) * Radius;
        targetPosition = new Vector3(x, y, 0f);
    }

    private void Update()
    {
        // Move the object towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);

        // If the object has reached the target position, choose a new one
        if (transform.position == targetPosition)
        {
            float angle = Random.Range(0f, 360f);
            float x = Center.x + Mathf.Cos(angle) * Radius;
            float y = Center.y + Mathf.Sin(angle) * Radius;
            targetPosition = new Vector3(x, y, 0f);
        }
    }
}