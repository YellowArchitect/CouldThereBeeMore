using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenEffectBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem party = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule temp = party.main; 
        temp.startColor = GetComponentInParent<PlayerController>().minigameColor;
        party.Play();
    }
}
