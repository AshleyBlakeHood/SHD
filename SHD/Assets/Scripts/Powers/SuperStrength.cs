using UnityEngine;
using System.Collections;

public class SuperStrength : Power
{
	GameManager manager;

	public GameObject kitchenSink;

	// Use this for initialization
	void Start ()
	{
		manager = GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//PUT CODE HERE THAT MUST ALWAYS EXECUTE EVEN WHEN CHARACTER IS NOT IN FOCUS
		
		if (activePower == true)
		{
			//PUT CODE HERE THAT MUST ONLY EXECUTE WHEN THE CHARACTER IS IN FOCUS
			if (Input.GetMouseButtonDown (0))
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				if (kitchenSink != null)
				{
					if (UsePower ())
					{
						GameObject g = (Instantiate(kitchenSink, new Vector2(ray.origin.x, ray.origin.y), kitchenSink.transform.rotation) as GameObject);
						g.transform.parent = manager.dynamicObjectHolder.transform;
					}
				}
			}
		}
		
		Recharge ();
	}
}