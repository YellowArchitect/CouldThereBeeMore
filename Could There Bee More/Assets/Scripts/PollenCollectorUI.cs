using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollenCollectorUI : MonoBehaviour
{
    public GameObject pollenPrefab;

    private Queue<GameObject> pollenQueue;
    private float offset;

    private void Awake()
    {
        pollenQueue = new Queue<GameObject>();
        offset = 0f;
    }

    public void Add(Color c)
    {
        // Create an image
        GameObject _newPollen = Instantiate(pollenPrefab, transform);
        _newPollen.GetComponent<Image>().color = c;

        // Set the position of the flower
        RectTransform _rect = _newPollen.GetComponent<RectTransform>();
        if (_rect)
        {
            _rect.position = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
            offset += 15f;
        }

        // Add it to the queue
        pollenQueue.Enqueue(_newPollen);
    }

    public void Remove()
    {
        Destroy(pollenQueue.Dequeue());

        // Reset the position of the pollen in the UI
        offset = 0f;
        GameObject[] _pollen = pollenQueue.ToArray();
        for (int i = 0; i < _pollen.Length; i++)
        {
            RectTransform _rect = _pollen[i].GetComponent<RectTransform>();
            if (_rect)
            {
                _rect.position = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
            }
            offset += 15f;
        }
    }
}
