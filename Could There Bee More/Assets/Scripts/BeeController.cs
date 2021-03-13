using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public HiveController hive;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float noise;

    private Rigidbody2D rb;
    private Vector3 patchPosition;
    private Vector2 direction;
    private bool goToPatch;
    private int pollenCount;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        goToPatch = true;
        pollenCount = 0;
    }

    public void ComeHome()
    {
        goToPatch = false;
    }

    public void GoOut(Vector3 targetPatch)
    {
        patchPosition = PatchPosition(targetPatch);
        goToPatch = true;
    }

    public int Collect()
    {
        int _pollen = pollenCount;
        pollenCount = 0;

        return _pollen;
    }

    private Vector3 PatchPosition(Vector3 target)
    {
        float _randX = Random.Range(-noise, noise);
        float _randY = Random.Range(-noise, noise);

        Vector3 _position = target;
        _position.x += _randX;
        _position.y += _randY;

        return _position;
    }

    private void Update()
    {
        if (goToPatch)
        {
            direction = (patchPosition - transform.position).normalized;
        }
        else
        {
            direction = (hive.transform.position - transform.position).normalized;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position.x += direction.x * speed;
        position.y += direction.y * speed;

        rb.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bee collected pollen!");
        pollenCount += 3;
    }
}
