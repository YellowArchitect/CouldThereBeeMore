using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiveController : MonoBehaviour
{
    public Text messageText;
    public int highScore;

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

    private AudioSource audioSrc;

    private string winnerBee;
    private float nextNightTime;

    private Queue<string> messages = new Queue<string>();
    private bool messageSystemOn;

    static int deliveredPollen = 0;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();

        nextNightTime = Time.time + dayDuration;
        messageSystemOn = false;
        deliveredPollen = 0;
    }

    private void Start()
    {
        audioSrc.Play();
        Vector3 _patch = Patch();
        Message("Go collect pollen bees!", messageDuration);
        foreach (BeeController _bee in registeredBees)
        {
            _bee.GoOut(_patch);
        }
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
        if (messageText)
        {
            messages.Enqueue(message);

            if (!messageSystemOn)
            {
                messageSystemOn = true;
                StartCoroutine(MessageSystem(duration));
            }
        }
    }

    private IEnumerator MessageSystem(float duration)
    {
        if (messageText)
        {
            while (messages.Count > 0)
            {
                string _message = messages.Dequeue();
                messageText.text = _message;

                yield return new WaitForSeconds(duration);

                messageText.text = "";
            }
        }

        messageSystemOn = false;
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
        audioSrc.Stop();
        Message($"Today's Winner!\n{winnerBee}: {highScore}", nightDuration);

        // "Sleep"
        yield return new WaitForSeconds(nightDuration);

        // Send bees out
        audioSrc.Play();
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

        // Get Player score
        PlayerController _player = collision.GetComponent<PlayerController>();
        if (_player)
        {
            int _score = _player.Collect();
            deliveredPollen += _score;
            if (_score > highScore)
            {
                highScore = _score;
                winnerBee = "Player";
            }
        }
    }

    public static int get_delivered_pollen()
    {
        return deliveredPollen;
    }


}
