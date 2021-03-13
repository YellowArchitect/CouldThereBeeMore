using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePollenBehaviour : MonoBehaviour
{

    public float minimumSpinSpeed = 1;
    public float maximumSpinSpeed = 10;

    float spinSpeed;
    Rigidbody2D rb2d;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = GetComponentInParent<PollenMinigame>().pollenColor;
        spinSpeed = Random.Range(minimumSpinSpeed, maximumSpinSpeed);
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float rotation = rb2d.rotation;
        rotation += spinSpeed;
        rb2d.MoveRotation(rotation);
    }
}
