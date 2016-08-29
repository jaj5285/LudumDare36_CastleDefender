using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldController : MonoBehaviour
{
    public static WorldController _instance;
    public float money = 100;
    public Text moneyText;
    public float totalHealth; // determined by all the runestone's hp combined

    public GameObject PlayerObj;
    public GameObject runestoneHealthContainer;
    public GameObject runestoneHealthPrefab;

    public GameObject[] runestones;


    void Awake()
    {
        _instance = this;

        // Runestone health
        totalHealth = 0;
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
        float calculatedHealth =0; 
        // Destroy all current texts
        foreach (Transform child in runestoneHealthContainer.transform)
        {
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

            calculatedHealth += rs.GetComponent<Construction>().curHealth;
        }
        totalHealth = calculatedHealth;
        checkGameOver();
    }

    public void checkGameOver()
    {
        if (totalHealth <= 0)
        {
            // Change to GameOver scene
            Debug.Log("DEAD!");
            Application.LoadLevel("GameOverScreen");
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
