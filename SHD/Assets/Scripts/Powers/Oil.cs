using UnityEngine;
using System.Collections;

public class Oil : Power {

	GameManager manager;
	
	public GameObject oilPatch;
	
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
				
				if (oilPatch != null)
				{
					if (UsePower ())
					{
						GameObject g = (Instantiate(oilPatch, new Vector2(ray.origin.x, ray.origin.y), oilPatch.transform.rotation) as GameObject);
						g.transform.parent = manager.dynamicObjectHolder.transform;
					}
				}
			}
		}
		Recharge();
	}
}
