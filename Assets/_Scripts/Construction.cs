using UnityEngine;
using System.Collections;

public class Construction : MonoBehaviour {

    // TODO: Write in collision detection for Player upgrade interaction
    
    public string myName = "Construction---";
    public float upgradeCost; // how much money for the next upgrade
    public float maxHealth = 100f;	// [0, inf]
	public float curHealth = 100f;	// [0, maxHealth]

	public bool isKillable = true;
    public bool isRunestone = false;

    public GameObject worldController;

    void Awake()
    {
        worldController = GameObject.Find("WorldController");
    }

	void OnTriggerEnter (Collider other) {
		if (this.isKillable && other.gameObject.tag == "Enemy") {
			// Cause Enemy to attack
			other.gameObject.GetComponent<EnemyBehavior>().setTarget(this.gameObject);
		}
	}

	public void receiveAttack (float damage) {
		this.curHealth -= damage;

        if (isRunestone)
        {
            worldController.GetComponent<WorldController>().DisplayRuneStoneHealth();
        }

        if (this.curHealth <= 0f)
        {
            this.curHealth = 0;
            if (isRunestone)
            {
                worldController.GetComponent<WorldController>().DisplayRuneStoneHealth();
            }

            // Do Destroy actions
            //Destroy(this.gameObject, 1f);
			this.tag = "Untagged";
			this.gameObject.active = false;
		}
	}
}
