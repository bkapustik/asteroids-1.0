using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float bullet_speed = 500.0f;
    public float bullet_life_time = 5.0f;
    
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    public void CreateBullet(Vector2 direction)
    {
        rigidbody.AddForce(this.bullet_speed * direction);
        Destroy(this.gameObject, this.bullet_life_time);
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


}
