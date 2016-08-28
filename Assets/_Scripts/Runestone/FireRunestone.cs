using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FireRunestone : Runestone
{
    void Start()
    {
        // Which runestone specific upgrades (for fire runestone, rock runestone, ect)
        //constructionIndex = 2;
        //spellObj = GameObject.Find("PlayerFlamethrower");


        interactText = "(Y) Interact";
        cancelText = "(Y) Cancel";

        Flamethrower flamethrower = worldController.GetComponent<WorldController>().PlayerObj.GetComponent<TP_Status>().flamethrowerObj.GetComponent<Flamethrower>();

        spellText = "(X) Buy Flamethrower $" + flamethrower.upgradeCost;
        constructionText = "   \n  (B) Buy Dragon $???";
        shopText = spellText + constructionText;

        interactTextObj.text = interactText;
        shopTextObj.text = shopText;
    }

    //public override void BuyConstruction()
    //{
    //    Debug.Log("Buy FIRE construction here ");
    //    BuilderManager bm = worldController.GetComponent<WorldController>().PlayerObj.GetComponent<BuilderManager>();
    //    bm.pickupItem(constructionIndex);
    //}
    //public override void BuySpell()
    //{
    //    WorldController wc = worldController.GetComponent<WorldController>();
    //    //TP_Status tpStatus = worldController.GetComponent<WorldController>().PlayerObj.GetComponent<TP_Status>();
    //    //Attack spell = tpStatus.flamethrowerObj.GetComponent<Attack>();

    //    Attack spell = spellObj.GetComponent<Attack>();

    //    if (wc.money >= spell.upgradeCost)
    //    {
    //        int newLevel = spell.level+1;
    //        Debug.Log("thislevel"+newLevel);

    //        wc.SubtractMoney(spell.upgradeCost);
    //        spell.Upgrade(newLevel);

    //        // Update TextMesh
    //        spellText = "(X) "+ spell.myName +" Lv"+ (newLevel+1) +" $" + spell.upgradeCost;
    //        shopText = spellText + constructionText;
    //        shopTextObj.text = shopText;
    //    }
    //}

}
