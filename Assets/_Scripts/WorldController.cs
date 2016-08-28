using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldController : MonoBehaviour
{
    public float money = 0;
    public Text moneyText;
    
    public void AddMoney(float amount)
    {
        // Add money
        money += amount;

        // Update text box
        moneyText.text = "Gold: " + money;
    }
}
