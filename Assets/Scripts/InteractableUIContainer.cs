using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class InteractableUIContainer : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		InteractableObject io = GetComponent<InteractableObject>();

		io.Hold += HandleHold;
		io.Release += HandleRelease;
	}
	
	void HandleHold(RaycastHit hit)
	{
		if (PlayerManager.instance.currentCreationMode == CreationMode.UIDRAG)
		{
			GameObject go = PlayerManager.instance.currentObject;

			if (go != null)
			{
				Vector3 pos = hit.point - PlayerManager.instance.currentObject.GetComponent<DraggableUI>().offset;
				
				Vector3 localPos = go.transform.GetChild(0).gameObject.transform.localPosition;
				go.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0.02f, localPos.y, localPos.z);

				go.transform.localPosition = new Vector3(hit.point.x, pos.y, hit.point.z);
				go.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
				
			}
		}
	}

	void HandleRelease()
	{
		if (PlayerManager.instance.currentCreationMode == CreationMode.UIDRAG)
		{
			PlayerManager.instance.currentCreationMode = CreationMode.NONE;
			PlayerManager.instance.currentObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("UI");
			PlayerManager.instance.currentObject = null;
		}
	}
}
