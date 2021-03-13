using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiveController : MonoBehaviour
{
    public Text score;
    public Text speech;
    public int highScore;

    public BeeController[] registeredBees;
    public Transform[] patches;

    [SerializeField]
    private float dayDuration;

    [SerializeField]
    private float nightDuration;

    private string winnerBee;
    private float nextNightTime;

    private void Awake()
    {
        nextNightTime = 0f;
    }

    private void Update()
    {
        if (nextNightTime < Time.time)
        {
            // Make it night time
            StartCoroutine(NightTime());

            nextNightTime = Time.time + dayDuration;
        }

        if (score)
        {
            score.text = highScore.ToString();
        }

        if (speech)
        {
            speech.text = $"Thank you {winnerBee} for collecting the high score of {highScore}!";
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

    private IEnumerator NightTime()
    {
        // Bring bees home
        foreach (BeeController _bee in registeredBees)
        {
            _bee.ComeHome();
        }

        // "Sleep"
        yield return new WaitForSeconds(nightDuration);

        // Send bees out
        Vector3 _patch = Patch();
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
