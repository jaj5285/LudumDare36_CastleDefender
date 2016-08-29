using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{

    public float attackDamage = 10f;
    public float attackInterval = 2f;
    public float attackDuration = 0.5f;

    public GameObject prevNode;
    public GameObject nextNode;

    private TrackNode prevNodeCode;
    private TrackNode nextNodeCode;

    public GameObject curDestination;
    public GameObject curTarget;

    public float travelSpeed;           // units per second

    public float curProgress = 0f;      // [0, 1]
    public float distToNextNode = 1f;   // (0, inf]

    public bool isAttacking = false;

	public float brambleSlowRatio = 0.5f; // [0, 1]
	public bool isBrambleSlowed = false;

    // Statuses
    public float health = 25;
    public float goldDrop = 50;
    public AttackType resistance = AttackType.Fire;
    public float resistanceMuter = 2; // Never set this to 0! Higher means less damage taken when resistant to attacktype
    public AttackType weakness;
    public float weaknessMultiplier = 1.5f; // Never set this to 0! Higher means more damage taken when weak to attacktype
    public bool allowContinuedAttack = false;

    float continuedAttackCounter = 0.3f; // Makes it so attack happen every _ seconds
    float continuedAttackTimer; // Timer to count up to the continuedAttackCounter

    public TextMesh healthText;
    public GameObject worldController;


    void Start()
    {
        this.prevNodeCode = this.prevNode.GetComponent<TrackNode>();
        this.selectNextNode();
        this.selectDestination();

        healthText.text = "HP: " + health;
        worldController = GameObject.Find("WorldController");
        continuedAttackCounter = 0.3f;
    }

    void FixedUpdate()
    {
        if (this.isAttacking)
        {
			if (this.curTarget == null || !this.curTarget.active) { this.removeTarget(); }
        }
        else {
            this.moveUnitTowardDest();
        }
    }

    void Update()
    {
        // Kill self
        if (health <= 0)
        {
            // Give gold
            worldController.GetComponent<WorldController>().AddMoney(goldDrop);

            // Die
            Destroy(this.gameObject);
        }

        // Let continued attack happen every 0.25 seconds
        if (!allowContinuedAttack)
        {
            continuedAttackTimer += Time.deltaTime;
            Debug.Log(continuedAttackTimer);
            if (continuedAttackTimer >= continuedAttackCounter)
            {
                allowContinuedAttack = true;
                continuedAttackTimer = 0;
            }
        }
    }

    public void setTarget(GameObject target)
    {
        this.isAttacking = true;
        this.curTarget = target;
        StartCoroutine(this.handleAttack());
    }

    public void removeTarget()
    {
        this.isAttacking = false;
    }

    private void moveUnitTowardDest()
    {
        this.curProgress = Mathf.Clamp01(this.curProgress + (travelSpeed / distToNextNode) * Time.deltaTime);

        if (this.curProgress == 1f)
        {
            this.selectNextNode();
        }

        if (curDestination == null)
        {
            this.selectDestination();
        }

        this.transform.LookAt(this.nextNode.transform.position);
        Vector3 idealPos = Vector3.Lerp(this.prevNode.transform.position, this.nextNode.transform.position, this.curProgress);

        this.transform.position = idealPos;
    }

    IEnumerator handleAttack()
    {
        // Repeat Attack Actions
        while (this.isAttacking)
        {
			this.GetComponentsInChildren<Animation>()[0].Play("WK_heavy_infantry_08_attack_B");

            yield return new WaitForSeconds(this.attackDuration);
			if (this.curTarget != null) {
				this.curTarget.GetComponent<Construction> ().receiveAttack (this.attackDamage);
			}

            yield return new WaitForSeconds(this.attackInterval);
        }

        // Exit Attack State
		this.GetComponentsInChildren<Animation>()[0].Play("WK_heavy_infantry_04_charge");
    }

    private void selectDestination()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        if (targets.Length == 0)
        {
            this.curDestination = null;
        }
        else {
            this.curDestination = targets[Random.Range(0, targets.Length)];
        }
    }

    private void selectNextNode()
    {
        if (this.nextNode != null)
        {
            this.prevNode = this.nextNode;
            this.prevNodeCode = this.nextNodeCode;
        }

		if (this.prevNodeCode.preferredNext != -1) {
			this.nextNode = this.prevNodeCode.connectedNodes [this.prevNodeCode.preferredNext];
		} else if (this.curDestination != null) {
            float minDist = Vector3.Distance(this.curDestination.transform.position, this.prevNodeCode.connectedNodes[0].transform.position);
            int minDistIndex = 0;

            for (int i = 1; i < this.prevNodeCode.connectedNodes.Length; i++)
            {

                // Find the connected Node that is closest to the current target
                if (minDist > Vector3.Distance(this.curDestination.transform.position, this.prevNodeCode.connectedNodes[i].transform.position))
                {
                    minDist = Vector3.Distance(this.curDestination.transform.position, this.prevNodeCode.connectedNodes[i].transform.position);
                    minDistIndex = i;
                }

                this.nextNode = this.prevNodeCode.connectedNodes[minDistIndex];
            }
        } else {
            this.nextNode = this.prevNodeCode.connectedNodes[Random.Range(0, this.prevNodeCode.connectedNodes.Length)];
        }

        this.nextNodeCode = this.nextNode.GetComponent<TrackNode>();

        this.distToNextNode = Vector3.Distance(this.prevNode.transform.position, this.nextNode.transform.position);
        this.curProgress = 0f;
    }

    public void ReceiveDamage(Collider col)
    {
        Attack colAttack = col.gameObject.GetComponent<Attack>();
        float calculatedDamage = colAttack.power;

        // Calculate damage taken using weakness and resistance
        if (colAttack.attackType == resistance)
        {
            calculatedDamage = calculatedDamage / resistanceMuter;
        }
        if (colAttack.attackType == weakness)
        {
            calculatedDamage = calculatedDamage * weaknessMultiplier;
        }
        health -= calculatedDamage;
        // Update text box
        healthText.text = "HP: " + health;

    }

    //void OnTriggerEnter(Collider col)
    //{
    //    // Non-continuous attack
    //    if (col.gameObject.CompareTag("Attack"))
    //    {
    //        Attack colAttack = col.gameObject.GetComponent<Attack>();
    //        if (colAttack.isActive && !colAttack.isContinuousAttack)
    //        {
    //            Debug.Log("pow!");
    //            ReceiveDamage(col);
    //        }
    //    }
    //}
    void OnTriggerStay(Collider col)
    {
        // Continuous attacks
        if (col.gameObject.CompareTag("Attack"))
        {
            Attack colAttack = col.gameObject.GetComponent<Attack>();
            if (colAttack.isActive)
            {
                if (allowContinuedAttack)
                {
                    allowContinuedAttack = false;
                    ReceiveDamage(col);
                }
            }
        }
    }
}
