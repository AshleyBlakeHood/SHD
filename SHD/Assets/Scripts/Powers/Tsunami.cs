using UnityEngine;
using System.Collections;

public class Tsunami : Power {

	GameManager manager;
	
	Ray ray;
	RaycastHit hit;
	public GameObject tsSprite;
	private GameObject tsS;
	Vector3 endPos;
	
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (activePower == true)
		{
			//PUT CODE HERE THAT MUST ONLY EXECUTE WHEN THE CHARACTER IS IN FOCUS
			if (Input.GetMouseButtonDown (0))
			{
				if (UsePower ())
				{
					Vector2 mouseInWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					GameObject g = (Instantiate(tsSprite, new Vector3(0,0,0), tsSprite.transform.rotation) as GameObject);
					g.transform.parent = manager.dynamicObjectHolder.transform;


					//Look At
					Vector3 direction = g.transform.position - new Vector3(mouseInWorld.x, mouseInWorld.y, 0);
					Quaternion rotation = Quaternion.LookRotation (direction, transform.TransformDirection(Vector3.forward));
					g.transform.rotation = new Quaternion (transform.rotation.x, transform.rotation.y, rotation.z, rotation.w);

					//g.transform.LookAt(endPos);
					g.GetComponent<tsunamiPush>().targetPos = mouseInWorld;
				}
			}
		}
		Recharge();
	}
}
