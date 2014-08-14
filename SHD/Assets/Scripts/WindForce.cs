using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindForce : MonoBehaviour {

	public Vector2 forceOrigin = Vector2.zero;

	public List<GameObject> hittingObjects;
	
	void Start () {
		hittingObjects = new List<GameObject>();
	}
	
	void Update () {
		
		if(Input.GetMouseButtonUp(0))
		{
			foreach(GameObject hitID in hittingObjects)
			{
				if (hitID != null)
					hitID.rigidbody2D.AddForce((new Vector2 (hitID.transform.position.x, hitID.transform.position.y) - new Vector2 (forceOrigin.x + transform.position.x, forceOrigin.y + transform.position.y)) * 20);
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{
		hittingObjects.Add(c.gameObject);
	}
	
	void OnTriggerExit2D(Collider2D c)
	{
		foreach(GameObject hitID in hittingObjects)
		{
			if(hitID.GetInstanceID() == c.gameObject.GetInstanceID())
			{
				hittingObjects.Remove(hitID);
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawSphere (new Vector2 (forceOrigin.x + transform.position.x, forceOrigin.y + transform.position.y), 0.2f);
	}
}
