﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public string beeName;
    public HiveController hive;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private float noise;

    [SerializeField]
    private int pollenCount;

    private Rigidbody2D rb;
    private Vector3 patchPosition;
    private Vector2 direction;
    private bool goToPatch;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        goToPatch = false;
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

            if (Vector3.Distance(patchPosition, transform.position) < 0.5f)
            {
                // Do nothing, smh
            }
        }
        else
        {
            direction = (hive.transform.position - transform.position).normalized;
        }
    }

    private void FixedUpdate()
    {
        Vector2 _position = rb.position;

        _position.x += direction.x * moveSpeed;
        _position.y += direction.y * moveSpeed;

        float _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion _rotation = Quaternion.Euler(new Vector3(0, 0, _angle - 90));

        rb.MovePosition(_position);
        transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, turnSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bee collected pollen!");

        if (collision.CompareTag("Flower"))
        {
            pollenCount++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
