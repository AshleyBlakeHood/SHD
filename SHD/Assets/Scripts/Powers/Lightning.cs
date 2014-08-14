using UnityEngine;
using System.Collections;

public class Lightning : Power {

	GameManager manager;
	
	public GameObject zap;
	
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
				
				if (zap != null)
				{
					if (UsePower ())
					{
						GameObject g = (Instantiate(zap, new Vector2(ray.origin.x, ray.origin.y), zap.transform.rotation) as GameObject);
						g.transform.parent = manager.dynamicObjectHolder.transform;
					}
				}
			}
		}
		Recharge();
	}
}
