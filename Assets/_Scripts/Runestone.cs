using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Runestone : MonoBehaviour {

    public TextMesh interactText;


    void Awake()
    {
        interactText.gameObject.SetActive(false);
    }

    public void SetInteractText(bool isActivated)
    {
        interactText.gameObject.SetActive(isActivated);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.GetComponent<TP_Status>().interactionState == InteractionState.Default)
            {
                // Show interaction message
                interactText.gameObject.SetActive(true);
            }
            else if (col.GetComponent<TP_Status>().interactionState == InteractionState.Shop)
            {
                // Show shopOptions message
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Hide interaction message
            SetInteractText(false);
        }

    }
}
