using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 0.05f;
    public float turnSpeed = 3f;
    public GameObject pollenMinigame;
    public GameObject pollenEffect;
    public Color minigameColor;

    public PollenCollectorUI pollenUI;

    Rigidbody2D rigidbody2d;
    Collider2D collider2d;
    Queue<Color> pollenList;

    float horizontal;
    float vertical;
    bool hasControl = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        pollenList = new Queue<Color>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = turnSpeed * Input.GetAxis("Horizontal");
        vertical = moveSpeed * Input.GetAxis("Vertical");

        if (hasControl)
        {

            // Check for flower interaction
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                RaycastHit2D[] results = new RaycastHit2D[1];
                if (collider2d.Raycast(transform.forward, results, 1, 1 << 9) > 0)
                {
                    FlowerBehaviour hit = results[0].transform.GetComponent<FlowerBehaviour>();
                    if (hit.is_collectable())
                    {
                        pause();
                        minigameColor = hit.collect_pollen();
                        Vector3 miniGamePos = new Vector3(transform.position.x, transform.position.y, 0);
                        Instantiate(pollenMinigame, miniGamePos, Quaternion.identity);
                    }
                }
            }

            // Check for pollen dump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                poof();
            }
        }
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

    // Receive pollen and store it in some data structure?
    public void receive_pollen(int polAmount)
    {
        for (int i = 0; i < polAmount; i++)
        {
            pollenList.Enqueue(minigameColor);
            pollenUI.Add(minigameColor);
        }
        unpause();
    }

    // Used to pollinate flowers
    void poof()
    {
        if (pollenList.Count > 0)
        {
            minigameColor = pollenList.Dequeue();
            pollenUI.Remove();
            Instantiate(pollenEffect, transform);

            // How to find the nearest flowers?
            Vector2 castSize = new Vector2(3, 3);
            RaycastHit2D[] results = Physics2D.BoxCastAll(transform.position, castSize, 0, transform.position, 0, 1<<9);
            for (int i = 0; i < results.Length; i++)
            {
                results[i].transform.gameObject.GetComponent<FlowerBehaviour>().pollinate(minigameColor);
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
}
