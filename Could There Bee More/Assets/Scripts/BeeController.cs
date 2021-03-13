using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public HiveController hive;
    public Transform patch;

    [SerializeField]
    private float speed;

    private Rigidbody2D rb;
    private Vector2 direction;
    private bool goToPatch;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        goToPatch = true;
    }

    private void Update()
    {
        if (goToPatch)
        {
            direction = (patch.position - transform.position).normalized;

            if (Vector2.Distance(transform.position, patch.position) < 0.1)
            {
                goToPatch = false;
            }
        }
        else
        {
            direction = (hive.transform.position - transform.position).normalized;

            if (Vector2.Distance(transform.position, hive.transform.position) < 0.1)
            {
                goToPatch = true;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position.x += direction.x * speed;
        position.y += direction.y * speed;

        rb.MovePosition(position);
    }
}
