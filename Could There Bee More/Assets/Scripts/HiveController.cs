using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiveController : MonoBehaviour
{
    public Text score;
    public int highScore;

    public GameObject[] registeredBees;

    [SerializeField]
    private float dayDuration;

    private Dictionary<string, int> beeScores = new Dictionary<string, int>();
    private string winnerBee;
    private float nextNightTime;

    private void Awake()
    {
        foreach (GameObject bee in registeredBees)
        {
            PollenCollector pc = bee.GetComponent<PollenCollector>();
            if (pc)
            {
                beeScores.Add(pc.beeName, 0);
            }
        }

        nextNightTime = 30f;
    }

    private void Update()
    {
        if (nextNightTime < Time.time)
        {
            // Make it night time
            // Bring bees home
            foreach (GameObject bee in registeredBees)
            {
                // TODO bring bees home
            }
            nextNightTime = Time.time + dayDuration;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PollenCollector pc = collision.GetComponent<PollenCollector>();
        if (pc)
        {
            beeScores[pc.beeName] += pc.Collect();
            Debug.Log($"Bee {pc.beeName} has collected {beeScores[pc.beeName]}");
        }
    }

    
}
