using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public Text coinsText;
    public Text alertText;

    public GameObject greenGate, blueGate, redGate;

    private int coinsCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            coinsCollected++;
            coinsText.text = $"Coins Collected: {coinsCollected}";
        }

        if (other.gameObject.tag == "GreenKey")
        {
            Destroy(other.gameObject);
            StartCoroutine(OpenGreen());
        }

        if (other.gameObject.tag == "BlueKey")
        {
            Destroy(other.gameObject);
            StartCoroutine(OpenBlue());
        }

        if (other.gameObject.tag == "RedKey")
        {
            Destroy(other.gameObject);
            StartCoroutine(OpenRed());
        }
    }

    IEnumerator OpenGreen()
    {
        greenGate.SetActive(false);

        alertText.text = "The <color=green>Green Gate</color> has been opened!";

        yield return new WaitForSeconds(5f);

        alertText.text = "";
    }

    IEnumerator OpenBlue()
    {
        blueGate.SetActive(false);

        alertText.text = "The <color=blue>Blue Gate</color> has been opened!";

        yield return new WaitForSeconds(5f);

        alertText.text = "";
    }

    IEnumerator OpenRed()
    {
        redGate.SetActive(false);

        alertText.text = "The <color=red>Red Gate</color> has been opened!";

        yield return new WaitForSeconds(5f);

        alertText.text = "";
    }
}
