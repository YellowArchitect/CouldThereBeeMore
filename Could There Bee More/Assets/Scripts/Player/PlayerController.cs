﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 0.05f;
    public float turnSpeed = 3f;
    public Color minigameColor;

    Rigidbody2D rigidbody2d;
    Collider2D collider2d;

    float horizontal;
    float vertical;
    bool hasControl = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = turnSpeed * Input.GetAxis("Horizontal");
        vertical = moveSpeed * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && hasControl)
        {
            RaycastHit2D[] results = new RaycastHit2D[1];
            if (collider2d.Raycast(transform.forward, results, 1, 1 << 9) > 0)
            {
                FlowerBehaviour hit = results[0].transform.GetComponent<FlowerBehaviour>();
                if (hit.is_collectable())
                {
                    pause();
                    minigameColor = hit.collect_pollen();
                }
            }
        }
    }

    void pause()
    {
        hasControl = false;
    }

    void unpause()
    {
        hasControl = true;
    }

    private void FixedUpdate()
    {
        if (hasControl)
        {
            Vector2 position = rigidbody2d.position;
            float rotation = rigidbody2d.rotation;
            position.x -= vertical * Time.fixedDeltaTime * Mathf.Sin((rotation / 180) * Mathf.PI);
            position.y += vertical * Time.fixedDeltaTime * Mathf.Cos((rotation / 180) * Mathf.PI);
            rotation -= horizontal * Time.fixedDeltaTime;
            rigidbody2d.MovePosition(position);
            rigidbody2d.MoveRotation(rotation);
        }
    }
}
