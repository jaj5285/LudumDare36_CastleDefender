using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TP_Status : MonoBehaviour {

    public static TP_Status _instance;
    public float health = 100;
    public float money = 0;
    public bool hasFlamethrower = false;

    public Text moneyText;


    void Awake () {
        _instance = this;
	}
    

    public void AddMoney(float amount)
    {
        // Add money
        money += amount;

        // Update text box
        moneyText.text = "Gold: " + money;
    }
}
