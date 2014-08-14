using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
	GameManager manager;
	AgentManager agentManager;

	public GameObject projectile;

	public vNavAgent agent;
	SpriteRenderer houseSR;

	public GameObject target;

	public Texture2D bg1, bg2;
	public GUIStyle bg1s, bg2s;
	float healthWidth = 26, healthWidthMax = 26, maxHealth = 15, healthPerc;

	float nextShot;
	float cooldown = 2;

	float health;

	public bool seekNewDestination = true;

	// Use this for initialization
	void Start ()
	{
		manager = GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<GameManager>();
		agentManager = GameObject.FindGameObjectWithTag ("Agent Manager").GetComponent<AgentManager>();

		agent = gameObject.GetComponent<vNavAgent>();
		houseSR = manager.GetHouse ().GetComponent<SpriteRenderer>();

		target = houseSR.gameObject;

		SetTargetAsDestination ();
		
		health = maxHealth;
		bg1s.normal.background = bg1;
		bg2s.normal.background = bg2;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (rigidbody2D.velocity != Vector2.zero)
		{
			seekNewDestination = true;
			return;
		}

		if (agent.enabled == false)
			agent.enabled = true;

		if (agent.hasPath == false && seekNewDestination == true)
		{
			if (Physics.Raycast (transform.position, manager.GetHouse ().transform.position))
				Debug.Log ("House Hit");

			SetTargetAsDestination ();
		}

		if (target == houseSR.gameObject)
		{
			if (DistanceToHouse (transform.position) < 2)
			{
				Vector3 direction = new Vector3(target.transform.position.x,target.transform.position.y, 0) - transform.position;
				Quaternion rotation = Quaternion.LookRotation (direction, transform.TransformDirection(Vector3.forward));
				transform.rotation = Quaternion.Slerp (transform.rotation, new Quaternion (transform.rotation.x, transform.rotation.y, rotation.z, rotation.w), Time.deltaTime * agent.rotationSpeed);
				
				if (Time.time > nextShot)
				{
					Shoot(projectile);
					
					nextShot = Time.time + cooldown;
				}
				
				seekNewDestination = false;
				agent.Stop ();
			}
		}
		else
		{
			if (Vector2.Distance (transform.position, target.transform.position) < 5)
			{
				//agent.SetDestination (transform.position);
				//transform.LookAt (manager.GetHouse ().transform.position);
				
				Vector3 direction = new Vector3(target.transform.position.x,target.transform.position.y, 0) - transform.position;
				Quaternion rotation = Quaternion.LookRotation (direction, transform.TransformDirection(Vector3.forward));
				transform.rotation = Quaternion.Slerp (transform.rotation, new Quaternion (transform.rotation.x, transform.rotation.y, rotation.z, rotation.w), Time.deltaTime * agent.rotationSpeed);
				
				if (Time.time > nextShot)
				{
					Shoot(projectile);
					
					nextShot = Time.time + cooldown;
				}
				
				seekNewDestination = false;
				agent.Stop ();
			}
		}

		//Code for the health bars, its all percentages and stuff, GCSE maths dont fail me now
		healthPerc = (health*100)/maxHealth;
		healthWidth = (healthWidthMax*healthPerc)/100;
		Debug.Log(healthPerc);
		
	}

	public void Shoot(GameObject bullet)
	{
		if (target == null)
			return;

		GameObject g = (Instantiate(bullet, transform.position, transform.rotation) as GameObject);
		g.transform.parent = manager.dynamicObjectHolder.transform;
		g.SendMessage ("SetTarget", new Vector2(target.transform.position.x, target.transform.position.y));
	}

	public void Damage(float d)
	{
		health = health - d;

		if (health <= 0)
			Die();
	}

	public void Die()
	{
		agentManager.AgentDied();

		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		Debug.Log (c.gameObject.name);

		if (c.gameObject.tag == "Kill Field")
			Die ();
	}

	public void SetTargetAsDestination()
	{
		if (target == houseSR.gameObject)
		{
			SetHouseAsDestination();
		}
		else
		{
			agent.SetDestination (target.transform.position);
		}
	}

	public float DistanceToHouse(Vector2 position)
	{
		float xTimeser = 1;
		float yTimeser = 1;

		if (position.x < houseSR.transform.position.x)
			xTimeser = -1;

		if (position.y < houseSR.transform.position.y)
			yTimeser = -1;

		return Vector2.Distance (position, new Vector2(houseSR.transform.position.x, houseSR.transform.position.y) + new Vector2 (houseSR.bounds.extents.x * xTimeser, houseSR.bounds.extents.y * yTimeser));
	}

	public void SetHouseAsDestination()
	{
		//House Location Picker
		float x = 0;
		float y = 0;
		
		int result = Random.Range (0, 4);
		
		//PickSide
		if (result == 0) //Left Side
		{
			x = houseSR.bounds.extents.x * -1;
			y = Random.Range (houseSR.bounds.extents.y * -1, houseSR.bounds.extents.y);

			x -= 1;
		}
		else if (result == 1) //Right Side
		{
			x = houseSR.bounds.extents.x;
			y = Random.Range (houseSR.bounds.extents.y * -1, houseSR.bounds.extents.y);

			x+= 1;
		}
		else if (result == 2) // Bottom Side
		{
			y = houseSR.bounds.extents.y * -1;
			x = Random.Range (houseSR.bounds.extents.x * -1, houseSR.bounds.extents.x);

			y-=1;
		}
		else if (result == 3) // Top Side
		{
			y = houseSR.bounds.extents.y;
			x = Random.Range (houseSR.bounds.extents.x * -1, houseSR.bounds.extents.x);

			y+= 1;
		}
		
		agent.SetDestination (new Vector3(x, y, 0));
		//agent.SetDestination (new Vector3(Random.Range (houseSR.bounds.extents.x * -1, houseSR.bounds.extents.x), 0, Random.Range (houseSR.bounds.extents.z * -1, houseSR.bounds.extents.z)));
	}

	public void SetHouseAsTarget()
	{
		target = houseSR.gameObject;
	}

	void OnGUI()
	{
		GUI.Box(new Rect(Camera.main.WorldToScreenPoint(this.transform.position).x,(Camera.main.WorldToScreenPoint(this.transform.position).y * -1) + Screen.height ,30,10), GUIContent.none, bg1s);
		GUI.Box(new Rect(Camera.main.WorldToScreenPoint(this.transform.position).x + 2,(Camera.main.WorldToScreenPoint(this.transform.position).y * -1) + Screen.height + 2 ,healthWidth,6), GUIContent.none, bg2s);
	}
	
}
