using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenCollector : MonoBehaviour
{
    public string beeName;

    [SerializeField]
    private int pollenCount;

    private void Awake()
    {
        pollenCount = 0;
    }

    public int Collect()
    {
        int _collected = pollenCount;
        pollenCount = 0;

        return _collected;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bee collected pollen!");
        pollenCount += 3;
    }
}
