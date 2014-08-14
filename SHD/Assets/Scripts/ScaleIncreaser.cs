using UnityEngine;
using System.Collections;

public class ScaleIncreaser : MonoBehaviour
{
	public Vector3 start = new Vector3(0, 0, 0);
	public Vector3 end = new Vector3(1, 1, 1);
	public Vector3 increment = new Vector3(1, 1, 1);

	public GameObject spawnOnDeath;

	float lastIncrease = 0;

	// Use this for initialization
	void Start ()
	{
		transform.localScale = start;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (lastIncrease > Time.time + 60)
			return;

		transform.localScale = transform.localScale + increment;

		if (transform.localScale.x > end.x || transform.localScale.y > end.y || transform.localScale.z > end.z)
		{
			if (spawnOnDeath != null)
			{
				GameObject g = (Instantiate(spawnOnDeath, transform.position, spawnOnDeath.transform.rotation) as GameObject);
				g.transform.parent = gameObject.transform.parent;
			}

			Destroy(gameObject);
		}

		lastIncrease = Time.time;
	}
}
