using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{

    public float seeThroughRate = 70f;
    public float minTreetop = 100f;

    bool inside = false;
    bool updating = false;

    SpriteRenderer mainSprite;

    // Start is called before the first frame update
    void Start()
    {
        minTreetop /= 255f;
        mainSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            updating = true;
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            updating = true;
            inside = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (updating)
        {
            // Player is under the tree
            if (inside)
            {
                Color color = mainSprite.color;
                color.a = Mathf.Max(minTreetop, color.a - (Time.deltaTime * seeThroughRate));
                mainSprite.color = color;

                if(Mathf.Equals(minTreetop, mainSprite.color.a))
                {
                    updating = false;
                }
            }

            // Player is not under the tree
            else
            {
                Color color = mainSprite.color;
                color.a = Mathf.Min(1, color.a + (Time.deltaTime * seeThroughRate));
                mainSprite.color = color;

                if (Mathf.Equals(1, mainSprite.color.a))
                {
                    updating = false;
                }
            }
        }
    }
}
