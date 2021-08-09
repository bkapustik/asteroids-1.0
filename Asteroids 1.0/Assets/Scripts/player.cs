using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    public float thrust_speed = 5.0f;
    public float rotation_speed = 5.0f;
    private float rotation = 0.0f;

    public bullet BulletPrefab;

    public float width = 18.29f;
    public float height = 10.47f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.x >= width || transform.position.x <= -width)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.y >= height || transform.position.y <= -height)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(thrust_speed * transform.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotation = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotation = -1.0f;
        }
        else rotation = 0.0f;

        if (rotation != 0)
        {
            rigidbody.AddTorque(rotation_speed * rotation);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        bullet Bullet = Instantiate(this.BulletPrefab, this.transform.position, this.transform.rotation);
        Bullet.CreateBullet(this.transform.up);
    }

   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "asteroid")
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.angularVelocity = 0;
            gameObject.SetActive(false);
            FindObjectOfType<gamemanager>().PlayerDied();

        }
    }
}

