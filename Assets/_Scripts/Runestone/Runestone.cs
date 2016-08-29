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
    public GameObject spellObj;
    public int constructionIndex = 0;
    
    public string spellText;
    public string constructionText;

    void Awake()
    {
        worldController = GameObject.Find("WorldController");

        interactTextObj.gameObject.SetActive(false);
        shopTextObj.gameObject.SetActive(false);
        interactionLight.intensity = 0;

        // Text
        interactText = "(Y) Interact";
        cancelText = "(Y) Cancel";

        interactTextObj.text = interactText;
        shopTextObj.text = shopText;

        // Dynamic shopping text
        Attack spell = spellObj.GetComponent<Attack>();
        WorldController wc = worldController.GetComponent<WorldController>();
        Construction construction = wc.PlayerObj.GetComponent<BuilderManager>().dropPrefabs[constructionIndex].GetComponent<Construction>();

        spellText = "(X) Buy" + spell.myName + " $" + spell.upgradeCost;
        constructionText = "\n  (B) Buy "+ construction.myName +" $"+ construction.upgradeCost;
        shopText = spellText + constructionText;
        shopTextObj.text = shopText;
    }

    public void BuyConstruction()
    {
        WorldController wc = worldController.GetComponent<WorldController>();
        TP_Status tpStatus = wc.PlayerObj.GetComponent<TP_Status>();
        Construction construction = wc.PlayerObj.GetComponent<BuilderManager>().dropPrefabs[constructionIndex].GetComponent<Construction>();
        if (wc.money >= construction.upgradeCost)
        {
            wc.SubtractMoney(construction.upgradeCost);
            BuilderManager bm = worldController.GetComponent<WorldController>().PlayerObj.GetComponent<BuilderManager>();
            bm.pickupItem(constructionIndex);

            tpStatus.interactionState = InteractionState.Construct;
            SetInteractText(true);
            SetShopText(false);
        }
    }

    public void BuySpell()
    {
        WorldController wc = worldController.GetComponent<WorldController>();
        TP_Status tpStatus = wc.PlayerObj.GetComponent<TP_Status>();
        //TP_Status tpStatus = worldController.GetComponent<WorldController>().PlayerObj.GetComponent<TP_Status>();
        //Attack spell = tpStatus.flamethrowerObj.GetComponent<Attack>();

        Attack spell = spellObj.GetComponent<Attack>();

        if (wc.money >= spell.upgradeCost)
        {
            int newLevel = spell.level + 1;

            wc.SubtractMoney(spell.upgradeCost);
            spell.Upgrade(newLevel);

            // Update TextMesh
            spellText = "(X) " + spell.myName + " Lv" + (newLevel + 1) + " $" + spell.upgradeCost;
            shopText = spellText + constructionText;
            shopTextObj.text = shopText;

            tpStatus.interactionState = InteractionState.Default;
            SetInteractText(true);
            SetShopText(false);
        }
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
