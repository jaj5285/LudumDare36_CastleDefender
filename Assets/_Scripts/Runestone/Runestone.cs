using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Runestone : MonoBehaviour {

    public TextMesh interactTextObj;
    public TextMesh shopTextObj;
    public Light interactionLight;

    public string interactText = "(Y) Interact";
    public string cancelText = "(Y) Cancel";
    public string shopText = "(X) Buy Spell          (B) Buy Construction";
    
    public GameObject worldController;


    void Awake()
    {
        worldController = GameObject.Find("WorldController");

        interactTextObj.gameObject.SetActive(false);
        shopTextObj.gameObject.SetActive(false);
        interactionLight.intensity = 0;

        interactTextObj.text = interactText;
        shopTextObj.text = shopText;
    }

    public virtual void BuyConstruction()
    {
        Debug.Log("Buy construction here ");
    }
    public virtual void BuySpell()
    {
        Debug.Log("Buy spell here ");
    }

    public void SetInteractText(bool isActivated)
    {
        interactTextObj.gameObject.SetActive(isActivated);
        interactTextObj.text = interactText;
    }

    public void SetShopText(bool isActivated)
    {
        shopTextObj.gameObject.SetActive(isActivated);
        if (isActivated)
        {
            interactTextObj.text = cancelText;
            interactionLight.intensity = 6;
        }
        else
        {
            interactTextObj.text = interactText;
            interactionLight.intensity = 3;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.GetComponent<TP_Status>().interactionState == InteractionState.Default)
            {
                // Show interaction message
                interactTextObj.gameObject.SetActive(true);
                interactionLight.intensity = 3;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Hide interaction message
            SetInteractText(false);
            SetShopText(false);
            interactionLight.intensity = 0;
        }

    }
}
