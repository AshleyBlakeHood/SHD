using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vNavAgent : MonoBehaviour
{
	public float speed = 2f;

	public AStar navFloor;

	public int spacesToCheckAhead = 4;
	public int percentageToCheckAhead = 4;

	public bool lookAtPath = true;
	public float rotationSpeed = 0.1f;

	private bool running = false;

	public bool hasPath = false;
	private List<Vector2> path = new List<Vector2>();

	private Vector2 endPoint = Vector2.zero;

	void Start()
	{
		if (navFloor == null)
			navFloor = GameObject.Find ("vNavigation").GetComponent<vNavRay> ();
	}

	void FixedUpdate()
	{
		//If a path isn't loaded or agent is paused don't do anything. Don't even blink.
		if (hasPath && running)
		{
			//Empty paths means end has been reached, stop the agent.
			if (path.Count < 1)
			{
				hasPath = false;
				running = false;
			}
			else
			{
				//Near the path point? Remove it from the queue and carry on otherwise move closer.
				if (Vector2.Distance (transform.position, path[path.Count - 1]) > 0.1f)
				{
					//transform.Translate ((Vector2.Lerp (transform.position, path[path.Count - 1], speed)) - new Vector2(transform.position.x, transform.position.y));
					transform.position = Vector2.Lerp (transform.position, path[path.Count - 1], speed);
				}
				else
				{
					path.RemoveAt (path.Count - 1);

					//Check that the path is still legal.
					SpaceAheadCheck ();
					PercentageAheadCheck();
				}

				//Rotation
				if (path.Count > 0 && lookAtPath)
				{
					Vector3 direction = new Vector3(path[path.Count - 1].x, path[path.Count - 1].y, 0) - transform.position;
					Quaternion rotation = Quaternion.LookRotation (direction, transform.TransformDirection(Vector3.forward));
					//transform.rotation = new Quaternion(0, 0, Mathf.Lerp (transform.rotation.z, rotation.z, rotationSpeed), rotation.w);
					transform.rotation = Quaternion.Slerp (transform.rotation, new Quaternion (transform.rotation.x, transform.rotation.y, rotation.z, rotation.w), Time.deltaTime * rotationSpeed);
				}
			}
		}
	}

	public void SpaceAheadCheck()
	{
		//If space to check ahead is 0 or less agent will walk through obstacles created after it created a path.
		for (int i = path.Count - 1; i > (path.Count - 1) - spacesToCheckAhead; i--)
		{
			if (i < 0)
				break;

			//If the path is now illegal create a new path.
			if (!navFloor.IsNodePassable (path[i]))
			{
				path = navFloor.GetPath (transform.position, endPoint);
			}
		}
	}

	public void PercentageAheadCheck()
	{
		//Used to look ahead at different intervals to check the path is still legal.
		int increment = path.Count / percentageToCheckAhead;

		if (increment < 1)
			increment = 1;

		for (int i = path.Count - 1; i > 0; i-= increment)
		{
			if (i < 0)
				break;
			
			if (!navFloor.IsNodePassable (path[i]))
			{
				path = navFloor.GetPath (transform.position, endPoint);
			}
		}
	}

	public void Continue()
	{
		//Allows the agent to continue after being paused, if agent has no path when called it will be ignored.
		hasPath = true;
		running = true;
	}

	public void Pause()
	{
		//Pauses the movement to carry on later.
		hasPath = true;
		running = false;
	}

	public void Stop()
	{
		//Stops the agent and makes it forget it's path.
		hasPath = false;
		running = false;

		path.Clear ();
	}

	public void SetDestination(Vector2 destination)
	{
		//Give the agent a place in Unity Units and it will create a path there.
		path = navFloor.GetPath (transform.position, destination);

		endPoint = destination;

		hasPath = true;
		running = true;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;

		for (int i = 0; i < path.Count; i++)
		{
			if (i > 0)
			{
				Gizmos.DrawLine(path[i - 1], path[i]);
				Gizmos.DrawSphere (path[i], 0.1f);
			}
		}

		if (path.Count > 0)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere (path[0], 0.1f);
		}
	}
}
