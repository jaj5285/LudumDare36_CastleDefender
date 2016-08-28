﻿using UnityEngine;
using System.Collections;

public class Construction : MonoBehaviour {

	public float maxHealth = 100f;	// [0, inf]
	public float curHealth = 100f;	// [0, maxHealth]

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Enemy") {
			// Cause Enemy to attack
			other.gameObject.GetComponent<EnemyBehavior>().setTarget(this.gameObject);
		}
	}

	public void receiveAttack (float damage) {
		this.curHealth -= damage;

		if (this.curHealth < 0f) {
			// Do Destroy actions
			Destroy(this.gameObject, 1f);
		}
	}
}