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
        collected = true;
        return flowerColor;
    }

    // Call to update the flower color
    public void update_color()
    {
        sr.color = flowerColor;
    }

    public void pollinate(Color color)
    {
        flowerColor.r = ((flowerColor.r*2) + color.r) / 3;
        flowerColor.g = ((flowerColor.g*2) + color.g) / 3;
        flowerColor.b = ((flowerColor.b*2) + color.b) / 3;
        collected = false;
        update_color();
    }
}
