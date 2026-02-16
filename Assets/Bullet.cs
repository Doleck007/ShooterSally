using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private Rigidbody rb;

   // [SerializeField] private float speed = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       // rb.linearVelocity = transform.forward * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Here you can add logic for what happens when the bullet hits something
        // For example, you could check if it hit an enemy and apply damage
        // Or you could create a bullet impact effect

        // Destroy the bullet after it collides with something
        rb.constraints = RigidbodyConstraints.FreezeAll; // Stop the bullet's movement
        //Destroy(gameObject);
    }

}

