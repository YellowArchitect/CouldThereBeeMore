using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int lifetime = 20;
    public float staminaInSeconds = 30f;
    public float moveSpeed = 0.05f;
    public float turnSpeed = 3f;
    public GameObject pollenMinigame;
    public GameObject pollenEffect;
    public Color minigameColor;

    static int pollenCollected = 0;
    static int flowersPollinated = 0;
    static int minigamesPlayed = 0;

    public PollenCollectorUI pollenUI;
    public Slider staminaSlider;

    public GameObject hive;

    Rigidbody2D rigidbody2d;
    Collider2D collider2d;
    Queue<Color> pollenList;

    int daysLeft;
    float stamina;
    float horizontal;
    float vertical;
    bool hasControl = true;
    readonly int flowerHiveMask = (1 << 9) | (1 << 13);

    // Start is called before the first frame update
    void Start()
    {
        pollenCollected = 0;
        flowersPollinated = 0;
        minigamesPlayed = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        pollenList = new Queue<Color>();
        stamina = staminaInSeconds;
        daysLeft = lifetime;

        if (staminaSlider)
        {
            staminaSlider.maxValue = stamina;
            staminaSlider.value = stamina;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = turnSpeed * Input.GetAxis("Horizontal");
        vertical = moveSpeed * Input.GetAxis("Vertical");

        if (hasControl)
        {

            if (stamina <= 0)
            {
                //pause();
                //transition
                transform.position = hive.transform.position;
                pollenList.Clear();
                pollenUI.Clear();
                daysLeft--;
                stamina = staminaInSeconds;
            }

            stamina -= Time.deltaTime;
            print(stamina);

            // Check for flower interaction
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                RaycastHit2D[] results = new RaycastHit2D[1];
                if (collider2d.Raycast(transform.forward, results, 1, flowerHiveMask) > 0)
                {
                    FlowerBehaviour flowerHit = results[0].transform.GetComponent<FlowerBehaviour>();
                    if (flowerHit != null && flowerHit.is_collectable())
                    {
                        pause();
                        minigameColor = flowerHit.collect_pollen();
                        Vector3 miniGamePos = new Vector3(transform.position.x, transform.position.y, 0);
                        Instantiate(pollenMinigame, miniGamePos, Quaternion.identity);
                        minigamesPlayed++;
                    }
                    else if (stamina < (staminaInSeconds - 10f))
                    {
                        //pause();
                        //transition
                        daysLeft--;
                        stamina = staminaInSeconds;
                    }
                }
            }

            if(daysLeft <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
            }

            // Check for pollen dump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                poof();
            }

            if (staminaSlider)
            {
                staminaSlider.value = stamina;
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
        pollenCollected += polAmount;
        for (int i = 0; i < polAmount; i++)
        {
            pollenList.Enqueue(minigameColor);
            pollenUI.Add(minigameColor);
        }
        unpause();
    }

    public static int[] get_stats()
    {
        int[] ret = new int[3];
        ret[0] = minigamesPlayed;
        ret[1] = pollenCollected;
        ret[2] = flowersPollinated;
        return ret;
    }

    public int get_days_left()
    {
        return daysLeft;
    }

    public float get_stamina()
    {
        return stamina;
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
            flowersPollinated += results.Length;
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
