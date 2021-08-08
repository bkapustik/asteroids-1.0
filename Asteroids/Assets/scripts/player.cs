using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update

    public bullet bulletprefab;
    private Rigidbody2D rigidbody;
    
    
    public float thrustspeed = 1.0f;
    public float turnspeed = 1.0f;
    private bool thrusting;
    private float turn_direction;
    
    

    // Update is called once per frame

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turn_direction = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turn_direction = -1.0f;
        }
        else 
        {
            turn_direction = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        // fixedupdate sa updatuje s casom nie so snimkami
        if (thrusting)
        {
            rigidbody.AddForce(this.transform.up * this.thrustspeed);
            // fyzike objektu pridam tieto atributy

        }

        if (turn_direction != 0.0f)
        {
            rigidbody.AddTorque(turn_direction * this.turnspeed);
        }
    }

    

    private void Shoot()
    {
        bullet bullet1 = Instantiate(this.bulletprefab, this.transform.position, this.transform.rotation);
        // instantiate sluzi na klonovanie toho prefabu a this.transform.nieco nastavi aby tento objekt mal rovnake nejake atributy ako ten objekt hrac
        bullet1.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "asteroid") 
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);

            FindObjectOfType<gamemanager>().PlayerDied();
        }
    }
}
