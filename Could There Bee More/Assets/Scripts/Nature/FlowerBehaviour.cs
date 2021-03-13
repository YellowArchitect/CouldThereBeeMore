using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBehaviour : MonoBehaviour
{

    public Color flowerColor;

    bool collected;

    SpriteRenderer sr;

    void Start()
    {
        collected = false;
        sr = GetComponent<SpriteRenderer>();
        update_color();
    }


    public bool is_collectable()
    {
        return !collected;
    }

    public Color collect_pollen()
    {
        print("flower collected");
        collected = true;
        return flowerColor;
    }

    // Call to update the flower color
    public void update_color()
    {
        sr.color = flowerColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
