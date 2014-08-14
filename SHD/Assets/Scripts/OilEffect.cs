using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OilEffect : MonoBehaviour {

	public List<GameObject> hittingObjects;
	// Use this for initialization
	void Start () {
		hittingObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Agent(Clone)")
		{
		hittingObjects.Add(c.gameObject);
		c.gameObject.GetComponent<vNavAgent>().speed = 0.1f;
		}
	}
	
	void OnTriggerExit2D(Collider2D c)
	{
		foreach(GameObject hitID in hittingObjects)
		{
			if(hitID.GetInstanceID() == c.gameObject.GetInstanceID())
			{
				if (c.gameObject.name == "Agent(Clone)"){
					hitID.gameObject.GetComponent<vNavAgent>().speed = 0.05f;
				hittingObjects.Remove(hitID);}
			}
		}
	}
	
	void OnDestroy()
	{
		foreach(GameObject go in hittingObjects)
		{
			go.gameObject.GetComponent<vNavAgent>().speed = 0.1f;
			hittingObjects.Remove(go);
		}
	}
	
	

}
