using UnityEngine;
using System.Collections;

public class Telekenesis : Power
{
	bool dragging = false;

	//Telekenesis Variables
	Vector3 startPosition;
	float pickupTime;
	GameObject targetAgent;

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
						targetAgent = hit.collider.gameObject;
						
						//Telekenesis Code
						startPosition = targetAgent.transform.position;
						pickupTime = Time.realtimeSinceStartup;
						dragging = true;
					}
				}
			}
			else if (Input.GetMouseButtonUp (0) && targetAgent != null)
			{
				targetAgent.GetComponent<vNavAgent>().Stop ();
				
				float timeSpentDragging = Time.realtimeSinceStartup - pickupTime;
				
				Vector3 directionToSend = (startPosition - targetAgent.transform.position) * -1;
				
				targetAgent.rigidbody2D.velocity = directionToSend;
				
				targetAgent.rigidbody2D.AddForce(directionToSend * (10 / timeSpentDragging));
				
				dragging = false;
				targetAgent = null;
			}
			
			if (dragging == true)
			{
				Debug.Log ("Dragging");
				Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(targetAgent.transform.position).z);
				
				Vector3 curPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
				targetAgent.transform.position = curPosition;
			}
		}
		
		Recharge ();
	}
}
