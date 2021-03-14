using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HiveController : MonoBehaviour
{
    public UnityAction<string, float> OnMessage;

    public BeeController[] registeredBees;
    public Transform[] patches;

    [SerializeField]
    private float dayDuration;

    [SerializeField]
    private float nightDuration;

    [SerializeField]
    private float messageDuration;

    [SerializeField]
    private float waitForReturnDuration;

    private int highScore;
    private string winnerBee;
    private float nextNightTime;

    private Queue<string> messages = new Queue<string>();
    private bool messageSystemOn;

    private void Awake()
    {
        nextNightTime = 0f;
        messageSystemOn = false;
    }

    private void Update()
    {
        if (nextNightTime < Time.time)
        {
            // Make it night time
            StartCoroutine(NightTime());

            nextNightTime = Time.time + dayDuration;
        }
    }

    private void ClearHighScore()
    {
        highScore = 0;
        winnerBee = "";
    }

    private Vector3 Patch()
    {
        if (patches.Length > 0)
        {
            int _patch = Random.Range(0, patches.Length);

            return patches[_patch].position;
        }

        return transform.position;
    }

    private void Message(string message, float duration)
    {
        // TODO invoke an event
        //OnMessage.Invoke(message, duration);   
    }

    private IEnumerator NightTime()
    {
        // Bring bees home
        Message("Come home bees!", messageDuration);
        foreach (BeeController _bee in registeredBees)
        {
            _bee.ComeHome();
        }

        // Wait for all the bees to come home before announcing the winner
        yield return new WaitForSeconds(waitForReturnDuration);
        Message($"Today's Highscore! {winnerBee}: {highScore}", nightDuration);

        // "Sleep"
        yield return new WaitForSeconds(nightDuration);

        // Send bees out
        Vector3 _patch = Patch();
        Message("Go collect pollen bees!", messageDuration);
        foreach (BeeController _bee in registeredBees)
        {
            _bee.GoOut(_patch);
        }

        // Clear score (after we collect all the scores)
        ClearHighScore();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get scores of bees
        BeeController _bee = collision.GetComponent<BeeController>();
        if (_bee)
        {
            int _score = _bee.Collect();
            if (_score > highScore)
            {
                highScore = _score;
                winnerBee = _bee.beeName;
            }
        }
    }


}
