using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FireRunestone : Runestone {

    public string spellText;
    public string constructionText;

    public float flamethrowerCost_Lv1 = 40;
    public float flamethrowerCost_Lv2 = 100;
    public float dragonCost_Lv1 = 500;

	void Start ()
    {
        interactText = "(Y) Interact";
        cancelText = "(Y) Cancel";

        spellText = "(X) Buy Flamethrower $" + flamethrowerCost_Lv1;
        constructionText = "   \n  (B) Buy Dragon $"+ dragonCost_Lv1;
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
        Debug.Log("Buy FIRE spell here ");
        if (wc.money > flamethrowerCost_Lv1) // && player does not already have flamethrower
        {
            wc.SubtractMoney(flamethrowerCost_Lv1);
            // TODO: Enable flamethower
            Debug.Log("TODO: Enable flamethower");

            // Update TextMesh
            spellText = "(X) Upgrade Flamethrower $" + flamethrowerCost_Lv2;
            shopText = spellText + constructionText;
            shopTextObj.text = shopText;
        }
    }

}
