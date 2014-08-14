using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tsunamiPush : MonoBehaviour {

	public Vector3 moveDir;
	public List<GameObject> hittingObjects;
	public Vector3 targetPos;
	
	// Use this for initialization
	void Start () {
		hittingObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(transform.position != targetPos)
		{
			transform.position = Vector3.Lerp(transform.position, targetPos, 0.07f);
		}
		else
		{
			Destroy(gameObject);
		}
		
		foreach(GameObject hitID in hittingObjects)
		{
			float distance = Vector3.Distance(transform.position, targetPos);
			hitID.rigidbody2D.AddForce(targetPos * (distance * 5));
		}
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{
		hittingObjects.Add(c.gameObject);
	}
	
	void OnTriggerExit2D(Collider2D c)
	{
		if(hittingObjects.Count != 0)
		{
			foreach(GameObject hitID in hittingObjects)
			{
				if(hitID.GetInstanceID() == c.gameObject.GetInstanceID())
				{
					hittingObjects.Remove(hitID);
				}
			}
		}
	}
	
	void OnDestroy()
	{
		hittingObjects = null;
	}
}
