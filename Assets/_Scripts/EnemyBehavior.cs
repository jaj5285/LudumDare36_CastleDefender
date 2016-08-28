using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	// TODO: Temp for Testing
	public Material NormalMaterial;
	public Material AttackMaterial;
	public Material DoAttackMaterial;

	public float attackDamage = 10f;
	public float attackInterval = 2f;
	public float attackDuration = 0.5f;

	public GameObject prevNode;
	public GameObject nextNode;

	private TrackNode prevNodeCode;
	private TrackNode nextNodeCode;

	public GameObject curDestination;
	public GameObject curTarget;

	public float travelSpeed;			// units per second

	public float curProgress = 0f;		// [0, 1]
	public float distToNextNode = 1f;	// (0, inf]

	public bool isAttacking = false;

	void Start () {
		this.prevNodeCode = this.prevNode.GetComponent<TrackNode> ();
		this.selectNextNode ();
		this.selectDestination ();
	}

	void FixedUpdate () {
		if (this.isAttacking) {
			if (this.curTarget == null) { this.removeTarget (); }
		} else {
			this.moveUnitTowardDest ();
		}
	}

	public void setTarget (GameObject target) {
		this.isAttacking = true;
		this.curTarget = target;
		StartCoroutine (this.handleAttack ());
	}

	public void removeTarget () {
		this.isAttacking = false;
	}

	private void moveUnitTowardDest () {
		this.curProgress = Mathf.Clamp01 (this.curProgress + (travelSpeed / distToNextNode) * Time.deltaTime);

		if (this.curProgress == 1f) {
			this.selectNextNode ();
		}

		if (curDestination == null) {
			this.selectDestination ();
		}

		this.transform.LookAt (this.nextNode.transform.position);
		Vector3 idealPos = Vector3.Lerp (this.prevNode.transform.position, this.nextNode.transform.position, this.curProgress);

		this.transform.position = idealPos;
	}

	IEnumerator handleAttack () {
		// Transition to Attak State
		this.GetComponent<Renderer>().material = AttackMaterial;

		// Repeat Attack Actions
		while (this.isAttacking) {
			this.GetComponent<Renderer>().material = DoAttackMaterial;
			yield return new WaitForSeconds (this.attackDuration);
			this.curTarget.GetComponent<Construction> ().receiveAttack (this.attackDamage);
			this.GetComponent<Renderer>().material = AttackMaterial;
			yield return new WaitForSeconds (this.attackInterval);
		}

		// Exit Attack State
		this.GetComponent<Renderer>().material = NormalMaterial;
	}

	private void selectDestination () {
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Target");
		if (targets.Length == 0) {
			this.curDestination = null;
		} else {
			this.curDestination = targets [Random.Range (0, targets.Length)];
		}
	}

	private void selectNextNode () {
		if (this.nextNode != null) {
			this.prevNode = this.nextNode;
			this.prevNodeCode = this.nextNodeCode;
		}

		if (this.curDestination != null) {
			float minDist = Vector3.Distance (this.curDestination.transform.position, this.prevNodeCode.connectedNodes [0].transform.position);
			int minDistIndex = 0;

			for (int i = 1; i < this.prevNodeCode.connectedNodes.Length; i++) {

				// Find the connected Node that is closest to the current target
				if (minDist > Vector3.Distance (this.curDestination.transform.position, this.prevNodeCode.connectedNodes [i].transform.position)) {
					minDist = Vector3.Distance (this.curDestination.transform.position, this.prevNodeCode.connectedNodes [i].transform.position);
					minDistIndex = i;
				}

				this.nextNode = this.prevNodeCode.connectedNodes [minDistIndex];
			}
		} else {
			this.nextNode = this.prevNodeCode.connectedNodes [Random.Range (0, this.prevNodeCode.connectedNodes.Length)];
		}

		this.nextNodeCode = this.nextNode.GetComponent<TrackNode>();

		this.distToNextNode = Vector3.Distance (this.prevNode.transform.position, this.nextNode.transform.position);
		this.curProgress = 0f;
	}
}
