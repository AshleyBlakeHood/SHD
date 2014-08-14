using UnityEngine;
using System.Collections;

public class MindControl : Power
{
	public GameObject bullet;

	GameObject targetAgent;
	Agent a;

	GameObject targetToKill;

	float timeAgentStartControl = 0;

	float nextShot;
	float cooldown = 2;

	private GameObject oldProjectile;

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
//				RaycastHit hit;
//				
//				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//				Physics.Raycast(ray, out hit);
				
				if (hit.collider.gameObject.tag == "Agent")
				{
					if (UsePower ())
					{
						targetAgent = hit.collider.gameObject;
						a = targetAgent.GetComponent<Agent>();
						
						a.seekNewDestination = true;
						targetAgent.tag = "Untagged";
						targetToKill = GameObject.FindGameObjectWithTag ("Agent");
						a.target = GameObject.FindGameObjectWithTag ("Agent");
						oldProjectile = a.projectile;
						a.projectile = bullet;

						timeAgentStartControl = Time.time;
					}
				}
			}
			
			if (targetAgent == null)
				return;
			
			if (Time.time > timeAgentStartControl + 10)
			{
				targetAgent.tag = "Agent";
				a.projectile = oldProjectile;

				a.seekNewDestination = true;
				
				targetAgent = null;
				targetToKill = null;

				a.SetHouseAsTarget ();
			}
			
			if (targetToKill == null)
			{
				targetToKill = GameObject.FindGameObjectWithTag ("Agent");
				a.target = GameObject.FindGameObjectWithTag ("Agent");
			}
			
//			if (targetAgent != null && targetToKill != null)
//			{
//				if (Vector3.Distance (targetAgent.transform.position, targetToKill.transform.position) < 5)
//				{
////					a.agent.SetDestination (targetAgent.transform.position);
////					targetAgent.transform.LookAt (targetToKill.transform.position);
//					
//					if (Time.time > nextShot)
//					{
//						a.Shoot(bullet);
//						
//						nextShot = Time.time + cooldown;
//					}
//				}
//				else
//				{
//					
//					a.GetComponent<vNavAgent>().SetDestination (targetToKill.transform.position);
//				}
//			}
		}
		
		Recharge ();
	}
}
