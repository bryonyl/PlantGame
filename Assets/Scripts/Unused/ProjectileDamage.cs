using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
        {
            // The projectile destroys itself once it comes into contact with anything that has a collider component and is NOT the player
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
