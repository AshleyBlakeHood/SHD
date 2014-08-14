using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireEffect : MonoBehaviour {

	public List<GameObject> hittingObjects;
	float cumTime, lastHit = 0, damageHit = 2, hitTimer = 0.5f;
	// Use this for initialization
	void Start () {
		hittingObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		cumTime += Time.deltaTime;
		if(cumTime - lastHit > hitTimer)
		{
			foreach(GameObject go in hittingObjects)
			{
				if (go != null)
				{
					if (go.tag == "Agent")
						go.GetComponent<Agent>().Damage(damageHit);
				}
			}

			lastHit = cumTime;
		}
		
		Debug.Log(damageHit);
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{
		Debug.Log (c.gameObject.name);
		if (c.gameObject.tag == "Agent")
		{
			hittingObjects.Add(c.gameObject);
		}
		if(c.gameObject.name == "Oil Slick(Clone)")
		{
			damageHit = 6;
			hittingObjects.Add(c.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D c)
	{
		foreach(GameObject hitID in hittingObjects)
		{
			if(hitID.GetInstanceID() == c.gameObject.GetInstanceID())
			{
				if (c.gameObject.name == "Agent(Clone)" || c.gameObject.name == "fireCircle(Clone)"){
					hittingObjects.Remove(hitID);}
			}
		}
	}

	//@@@Ash is this left over copy pasta from Oil?
//	void OnDestroy()
//	{
//		foreach(GameObject go in hittingObjects)
//		{
//			//go.gameObject.GetComponent<NavMeshAgent>().speed = 2.0f; 
//			hittingObjects.Remove(go);
//		}
//	}
}
