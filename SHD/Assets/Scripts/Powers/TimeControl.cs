using UnityEngine;
using System.Collections;

public class TimeControl : Power
{
	Agent a;

	// Use this for initialization
	void Start ()
	{
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
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
				
				if (hit.collider.gameObject.tag == "Agent")
				{
					if (UsePower ())
					{
						a = hit.collider.gameObject.GetComponent<Agent>();
						
						a.agent.speed = 0.05f;
						StartCoroutine (UnfreezeTime (a));
					}
				}
			}
		}
		
		Recharge ();
	}
	
	IEnumerator UnfreezeTime(Agent agent)
	{
		yield return new WaitForSeconds(10);
		
		if (agent.agent != null)
			agent.agent.speed = 0.1f;
	}
}
