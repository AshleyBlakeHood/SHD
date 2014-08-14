using UnityEngine;
using System.Collections;

public class Fire : Power {

	GameManager manager;
	
	public GameObject fireSpot;

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
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				if (fireSpot != null)
				{	
					if (UsePower ())
					{
						GameObject g = (Instantiate(fireSpot, new Vector2(ray.origin.x, ray.origin.y), fireSpot.transform.rotation) as GameObject);
						g.transform.parent = manager.dynamicObjectHolder.transform;
					}
				}
			}
		}
		Recharge();
	}
}
