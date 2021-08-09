using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{

    public float max_size = 1.5f;
    public float min_size = 0.5f;
    public Sprite[] sprites;
    private Rigidbody2D Rigidbody;
    private SpriteRenderer spriterenderer;
    public float size;
    public float speed = 5.0f;
    public float lifetime = 20.0f;
    public float split_speed = 20.0f;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();


    }

    private void Start()
    {
        spriterenderer.sprite = sprites[(int)Random.Range(0, sprites.Length)];

        

        transform.localScale = new Vector3(transform.localScale.x * size, transform.localScale.y * size, transform.localScale.z);

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360);

        Rigidbody.mass = size;
    }

    public void SetDirection(Vector2 direction)
    {
        Rigidbody.AddForce(speed * direction);
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.tag == "bullet")
        {
            
            if (size >= 1.0f)
            {
                for (int i = 0; i <= 1; i++)
                {
                    Split();
                }
                
            }

            FindObjectOfType<gamemanager>().AsteroidDestroyed(this); 
            Destroy(gameObject);
        }
    }
    

    private void Split()
    {
        
        

        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        asteroid new_Asteroid = Instantiate(this, position, transform.rotation);

        
        new_Asteroid.SetDirection(Random.insideUnitCircle.normalized * split_speed);
        
        new_Asteroid.size = this.size * 0.5f;
    
    }

}
