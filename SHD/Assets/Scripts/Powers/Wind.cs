using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wind : Power {

	GameManager manager;

	//Wind variables
	public GameObject windSprite;
	private GameObject wS;
	Ray ray;
	RaycastHit hit;

	void Start () {
		manager = GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<GameManager>();
	}
	
	void Update () {
		//PUT CODE HERE THAT MUST ALWAYS EXECUTE EVEN WHEN CHARACTER IS NOT IN FOCUS
		if (activePower == true)
		{
			//PUT CODE HERE THAT MUST ONLY EXECUTE WHEN THE CHARACTER IS IN FOCUS
			if (wS == null)
			{
				wS = (Instantiate(windSprite) as GameObject);
				wS.transform.parent = manager.dynamicObjectHolder.transform;
			}

			if (Input.GetMouseButton (0))
			{
				if (UsePower ())
				{
					Vector2 mouseInWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					//Physics.Raycast(ray, out hit); 
					//float hx = hit.point.x;
					//float hz = hit.point.z;
					//wS.transform.LookAt(new Vector3(mouseInWorld.x, mouseInWorld.y, 0));
					Vector3 direction = transform.position - new Vector3(mouseInWorld.x, mouseInWorld.y, 0);
					Quaternion rotation = Quaternion.LookRotation (direction, transform.TransformDirection(Vector3.forward));
					wS.transform.rotation = new Quaternion (transform.rotation.x, transform.rotation.y, rotation.z, rotation.w);
					//wS.GetComponent<WindForce>().moveDir = new Vector3(hx,0,hz);
				}
			}
		}
		else
		{
			DestroyObject(wS);
		}
		Recharge();
	}
	
}
