using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the Y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}