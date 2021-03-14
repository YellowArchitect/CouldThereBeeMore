using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaysLeftController : MonoBehaviour
{
    private PlayerController player;
    private Text dayText;

    private void Start()
    {
        GameObject _playerGO = GameObject.FindGameObjectWithTag("Player");
        if (_playerGO)
        {
            player = _playerGO.GetComponent<PlayerController>();
        }

        dayText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (player && dayText)
        {
            dayText.text = $"{player.get_days_left()}\ndays left";
        }
    }
}
