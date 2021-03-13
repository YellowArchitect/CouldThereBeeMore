using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 0.05f;
    public float turnSpeed = 3f;

    Rigidbody2D rb;

    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = turnSpeed * Input.GetAxis("Horizontal");
        vertical = moveSpeed * Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        float rotation = rb.rotation;
        position.x -= vertical * Mathf.Sin((rotation/180)*Mathf.PI);
        position.y += vertical * Mathf.Cos((rotation / 180) * Mathf.PI);
        rotation -= horizontal;
        rb.MovePosition(position);
        rb.MoveRotation(rotation);
    }
}
