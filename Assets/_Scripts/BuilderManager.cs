using UnityEngine;
using System.Collections;

public class BuilderManager : MonoBehaviour {

	public GameObject[] holdPrefabs;
	public GameObject[] dropPrefabs;

	public int curHeldObjIndex;

	public GameObject curHeldObj;

	public Vector3 holdOffset;

	
	// Update is called once per frame
	void Update () {
		if (curHeldObj != null) {
			// Update curHeldObj's position and stuff

		}
	}

	public void pickupItem (int itemIndex) {
		if (curHeldObj == null && itemIndex >= 0 && itemIndex < this.holdPrefabs.Length) {
			
			curHeldObjIndex = itemIndex;
			curHeldObj = GameObject.Instantiate (this.holdPrefabs [itemIndex]);
			curHeldObj.transform.SetParent (this.transform);
			curHeldObj.transform.localPosition = this.holdOffset;
			curHeldObj.transform.localRotation = Quaternion.identity;
		}
	}

	public void dropCurItem () {
		if (curHeldObj != null && curHeldObj.GetComponent<Placer>().goodPlace) {
			// Create correct Drop Prefab
			GameObject dropped = GameObject.Instantiate (this.dropPrefabs [this.curHeldObjIndex]);
			dropped.transform.parent = null;
			dropped.transform.position = this.curHeldObj.transform.position;
			dropped.transform.rotation = this.curHeldObj.transform.rotation;

			RaycastHit groundHit;
			if (Physics.Raycast (dropped.transform.position, -Vector3.up, out groundHit)) {
				dropped.transform.position -= new Vector3 (0, groundHit.distance, 0);
			}

			// Destroy Held Prefab
			Destroy (curHeldObj);
			curHeldObj = null;
		}
	}
}
