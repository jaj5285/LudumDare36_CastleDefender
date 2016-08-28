using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldController : MonoBehaviour
{
    public static WorldController _instance;
    public float money = 0;
    public Text moneyText;

    void Awake()
    {
        _instance = this;
    }
    
    public void AddMoney(float amount)
    {
        // Add money
        money += amount;

        // Update text box
        moneyText.text = "Gold: " + money;
    }

    public void SubtractMoney(float amount)
    {
        money -= amount;
        moneyText.text = "Gold: " + money;
    }
}
