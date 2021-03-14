using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    private Text messageText;
    private Image background;

    private Queue<string> messageQueue;
    private bool messageSystemOn;

    private void Awake()
    {
        messageText = GetComponentInChildren<Text>();
        background = GetComponentInChildren<Image>();

        messageText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);

        messageQueue = new Queue<string>();
        messageSystemOn = false;
    }

    public void DisplayMessage(string message, float duration)
    {
        messageQueue.Enqueue(message);

        if (!messageSystemOn)
        {
            messageSystemOn = true;
            StartCoroutine(MessageSystem(duration));
        }
    }

    private IEnumerator MessageSystem(float duration)
    {
        messageText.gameObject.SetActive(messageSystemOn);
        background.gameObject.SetActive(messageSystemOn);

        while (messageQueue.Count > 0)
        {
            string _message = messageQueue.Dequeue();
            messageText.text = _message;

            yield return new WaitForSeconds(duration);

            messageText.text = "";
        }

        messageSystemOn = false;

        messageText.gameObject.SetActive(messageSystemOn);
        background.gameObject.SetActive(messageSystemOn);
    }
}
