using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{

    public float moveSpeed = 15f;

    int caught = 0;
    float horizontal;
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public int get_caught()
    {
        return caught;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MinigamePollenBehaviour>() != null)
        {
            caught++;
            Destroy(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x += horizontal * moveSpeed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }
}
