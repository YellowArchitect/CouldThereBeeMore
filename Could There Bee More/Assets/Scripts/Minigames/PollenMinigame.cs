using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenMinigame : MonoBehaviour
{

    public GameObject pollen;
    public int pollenSpawnAmount = 5;
    public float pollenSpawnFrequency = 2f;
    public float pollenSpawnRange = 5.5f;
    public Color pollenColor;

    float timer;
    int counter;
    int caught = 0;
    PlayerController player;
    BasketController basket;
    Collider2D collider2d;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        pollenColor = player.minigameColor;
        basket = GetComponentInChildren<BasketController>();
        timer = pollenSpawnFrequency;
        counter = pollenSpawnAmount;
        collider2d = GetComponent<Collider2D>();
    }

    void spawn_pollen()
    {
        float randX = Random.Range(-pollenSpawnRange, pollenSpawnRange);
        Vector3 newPos = new Vector3(transform.position.x + randX, transform.position.y + 6.5f, 0);
        
        Instantiate(pollen, newPos, Quaternion.identity).transform.parent = transform;
    }

    void end_minigame()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && counter > 0)
        {
            timer = pollenSpawnFrequency;
            counter--;
            spawn_pollen();
        }
        
        if (caught + basket.get_caught() == pollenSpawnAmount)
        {
            end_minigame();
        }
    }
}
