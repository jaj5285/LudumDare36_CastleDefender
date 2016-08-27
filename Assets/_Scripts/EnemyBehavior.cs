using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	// TODO: Temp for Testing
	public Material NormalMaterial;
	public Material AttackMaterial;

	public GameObject target;

	public GameObject prevNode;
	public GameObject nextNode;

	private TrackNode prevNodeCode;
	private TrackNode nextNodeCode;

	public GameObject curTarget;

	public float travelSpeed;			// units per second

	public float curProgress = 0f;		// [0, 1]
	public float distToNextNode = 1f;	// (0, inf]

	public bool isAttacking = false;
	public float attackDistance = 10f;

	void Start () {
		this.prevNodeCode = this.prevNode.GetComponent<TrackNode> ();
		this.selectNextNode ();
		this.selectTarget ();
	}

	void FixedUpdate () {
		


		if (isAttacking) {
			if (this.curTarget == null || Vector3.Distance (this.transform.position, this.curTarget.transform.position) >= this.attackDistance) {
				// Transition out of Attack State
				this.GetComponent<Renderer>().material = NormalMaterial;
				this.isAttacking = false;
			}
			// Do Attack?

		} else {
			if (curTarget != null && Vector3.Distance (this.transform.position, this.curTarget.transform.position) < this.attackDistance) {
				// Transition to Attack State
				this.GetComponent<Renderer>().material = AttackMaterial;
				this.isAttacking = true;
			}

			this.curProgress = Mathf.Clamp01 (this.curProgress + (travelSpeed / distToNextNode) * Time.deltaTime);

			if (this.curProgress == 1f) {
				this.selectNextNode ();
			}

			if (curTarget == null) {
				this.selectTarget ();
			}

			this.transform.LookAt (this.nextNode.transform.position);
			Vector3 idealPos = Vector3.Lerp (this.prevNode.transform.position, this.nextNode.transform.position, this.curProgress);

			this.transform.position = idealPos;
		}
	}

	private void selectTarget () {
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Target");
		if (targets.Length == 0) {
			this.curTarget = null;
		} else {
			this.curTarget = targets [Random.Range (0, targets.Length)];
		}
	}

	private void selectNextNode () {
		if (this.nextNode != null) {
			this.prevNode = this.nextNode;
			this.prevNodeCode = this.nextNodeCode;
		}

		if (this.curTarget != null) {
			float minDist = Vector3.Distance (this.curTarget.transform.position, this.prevNodeCode.connectedNodes [0].transform.position);
			int minDistIndex = 0;

			for (int i = 1; i < this.prevNodeCode.connectedNodes.Length; i++) {

				// Find the connected Node that is closest to the current target
				if (minDist > Vector3.Distance (this.curTarget.transform.position, this.prevNodeCode.connectedNodes [i].transform.position)) {
					minDist = Vector3.Distance (this.curTarget.transform.position, this.prevNodeCode.connectedNodes [i].transform.position);
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
