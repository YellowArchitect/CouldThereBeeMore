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
    }

    private void Update()
    {

    }

    public void Add(Color c)
    {
        // Create an image
        GameObject _newPollen = Instantiate(pollenPrefab, transform);
        _newPollen.GetComponent<Image>().color = c;

        GameObject[] array = pollenQueue.ToArray();
        for (int i = 0; i < array.Length; i++)
        {
            RectTransform _rect = array[i].GetComponent<RectTransform>();
            if (_rect)
            {
                _rect.position = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
                offset += 10f;
            }
        }

        // Add it to the queue
        pollenQueue.Enqueue(_newPollen);
    }

    public void Remove()
    {
        Destroy(pollenQueue.Dequeue());
    }
}
