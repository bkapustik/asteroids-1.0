using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    
    private SpriteRenderer spriterenderer;

    private Rigidbody2D rigidbody;

    public float size = 1.0f;

    public float minSize = 0.5f;

    public float maxSize = 1.5f;

    public float speed = 50.0f;

    public float maxLifetime = 30.0f;
    

    private void Awake()
    {

        spriterenderer = GetComponent<SpriteRenderer>();

        rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Start()
    {
        spriterenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        rigidbody.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            if (this.size / 2 >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<gamemanager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed * 1.2f);
    }
}
