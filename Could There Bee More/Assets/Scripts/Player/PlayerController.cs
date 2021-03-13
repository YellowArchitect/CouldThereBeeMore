using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 0.05f;
    public float turnSpeed = 3f;

    Rigidbody2D rigidbody2d;
    Collider2D collider2d;

    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = turnSpeed * Input.GetAxis("Horizontal");
        vertical = moveSpeed * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            print("pressed shift");
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        float rotation = rigidbody2d.rotation;
        position.x -= vertical * Mathf.Sin((rotation/180)*Mathf.PI);
        position.y += vertical * Mathf.Cos((rotation / 180) * Mathf.PI);
        rotation -= horizontal;
        rigidbody2d.MovePosition(position);
        rigidbody2d.MoveRotation(rotation);
    }
}
