using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public Text coinsText; //ui text to display number of coins collected
    public Text alertText; //ui text to alert player on when gates open
    public Text goldGateText; //the text for telling player how to open golden gate

    public GameObject greenGate, blueGate, redGate, goldGate; //each of the color gates

    private int coinsCollected = 0; //number of coins collected

    private void Start()
    {
        coinsText.text = $"Coins Collected: {coinsCollected}"; //displays how many coins have been collected so far, game just started to count is 0
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin") //if player collides with a coin
        {
            Destroy(other.gameObject); //destroys the coin game object
            coinsCollected++; //increases the number coins collected by 1
            coinsText.text = $"Coins Collected: {coinsCollected}"; //updates display

            if (coinsCollected == 30) //if all 30 coins have been collected
            {
                StartCoroutine(OpenGold()); //opens gold gate
            }
        }

        if (other.gameObject.tag == "GreenKey") //if player collides with green key
        {
            Destroy(other.gameObject); //destroys key
            StartCoroutine(OpenGreen()); //opens gate
        }

        if (other.gameObject.tag == "BlueKey") //same as above
        {
            Destroy(other.gameObject);
            StartCoroutine(OpenBlue());
        }

        if (other.gameObject.tag == "RedKey") //same as above
        {
            Destroy(other.gameObject);
            StartCoroutine(OpenRed());
        }

        if (other.gameObject.tag == "InfinityKey") //same as above
        {
            Destroy(other.gameObject);
            StartCoroutine(GameEnd()); //maze complete, closes game
        }
    }

    IEnumerator OpenGreen() //opens green gate
    {
        greenGate.SetActive(false); //sets green gate inactive

        alertText.text = "The <color=green>Green Gate</color> has been opened!"; //alerts player the gate is open

        yield return new WaitForSeconds(5f);

        alertText.text = "";
    }

    IEnumerator OpenBlue() //same as above
    {
        blueGate.SetActive(false);

        alertText.text = "The <color=blue>Blue Gate</color> has been opened!";

        yield return new WaitForSeconds(5f);

        alertText.text = "";
    }

    IEnumerator OpenRed() //same as above
    {
        redGate.SetActive(false);
        goldGateText.text = "Collect 30 coins to\nopen the Gold Gate";

        alertText.text = "The <color=red>Red Gate</color> has been opened!";

        yield return new WaitForSeconds(5f);

        alertText.text = "";
    }

    IEnumerator OpenGold() //same as above
    {
        goldGate.SetActive(false);

        alertText.text = "The <color=yellow>Gold Gate</color> has been opened!";

        yield return new WaitForSeconds(5f);

        alertText.text = "";
    }

    IEnumerator GameEnd() //tells player via alert text that game has ended and closes game
    {
        alertText.text = "You have completed the maze and gained Infinity!";

        yield return new WaitForSeconds(5f);

        alertText.text = "Thank you for playing!";

        yield return new WaitForSeconds(5f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
