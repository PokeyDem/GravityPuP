using UnityEngine;

public class SandStorm : MonoBehaviour
{
    [SerializeField] private float xForce = 0.01f;
    [SerializeField] private float yForce = 0.01f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player") {
            Vector3 movement = new Vector3(xForce, yForce, 0f); 
            other.transform.position += movement;
        }
    }
}
