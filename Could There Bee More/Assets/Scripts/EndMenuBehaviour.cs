using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuBehaviour : MonoBehaviour
{

    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        int[] results = PlayerController.get_stats();
        string stats = string.Format("Flowers Visited: {0}\nPollen Collected: {1}\nFlowers Pollinated: {2}",
            results[0], results[1], results[2]);
        text.GetComponent<Text>().text = stats;
    }

    public void main_menu_button()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
