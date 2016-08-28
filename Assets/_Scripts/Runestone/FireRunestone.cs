using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FireRunestone : Runestone
{

    public string spellText;
    public string constructionText;
    
    void Start()
    {
        interactText = "(Y) Interact";
        cancelText = "(Y) Cancel";


        Flamethrower flamethrower = worldController.GetComponent<WorldController>().PlayerObj.GetComponent<TP_Status>().flamethrowerObj.GetComponent<Flamethrower>();

        spellText = "(X) Buy Flamethrower $" + flamethrower.upgradeCost;
        constructionText = "   \n  (B) Buy Dragon $???";
        shopText = spellText + constructionText;

        interactTextObj.text = interactText;
        shopTextObj.text = shopText;
    }

    public override void BuyConstruction()
    {
        Debug.Log("Buy FIRE construction here ");
    }
    public override void BuySpell()
    {
        WorldController wc = worldController.GetComponent<WorldController>();
        TP_Status tpStatus = worldController.GetComponent<WorldController>().PlayerObj.GetComponent<TP_Status>();
        Flamethrower flamethrower = tpStatus.flamethrowerObj.GetComponent<Flamethrower>();

        if (wc.money >= flamethrower.upgradeCost)
        {
            int newLevel = flamethrower.level++;

            wc.SubtractMoney(newLevel);
            flamethrower.Upgrade(newLevel);

            // Update TextMesh
            spellText = "(X) Flamethrower Lv"+ (newLevel+1) +" $" + flamethrower.upgradeCost;
            shopText = spellText + constructionText;
            shopTextObj.text = shopText;
        }
    }

}
