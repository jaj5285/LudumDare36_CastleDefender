using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldController : MonoBehaviour
{
    public static WorldController _instance;
    public float money = 100;
    public Text moneyText;

    public GameObject PlayerObj;
    public GameObject runestoneHealthContainer;
    public GameObject runestoneHealthPrefab;

    public GameObject[] runestones;


    void Awake()
    {
        _instance = this;

        // Runestone health
       DisplayRuneStoneHealth();
        // Update text box
        moneyText.text = "Gold: " + money;

    }

    void Update()
    {
        // Game Over
    }

    // TODO: So much hack. fix it
    public void DisplayRuneStoneHealth()
    {
        Debug.Log("RuneStones Updating");

        // Destroy all current texts
        foreach (Transform child in runestoneHealthContainer.transform)
        {
            Debug.Log("test");
            Destroy(child.gameObject);
        }

        // Just recreate all current texts
        foreach (GameObject rs in runestones)
        {
            // Make Texts with health of each
            GameObject rsHealthObj = Instantiate(runestoneHealthPrefab);
            rsHealthObj.transform.parent = runestoneHealthContainer.transform;
            Vector3 scale = new Vector3(1, 1, 1);
            rsHealthObj.GetComponent<RectTransform>().localScale = scale;
            Text m_nameText = rsHealthObj.GetComponent<Text>();
            m_nameText.text = "HP " + rs.GetComponent<Construction>().curHealth;
        }
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
