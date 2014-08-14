using UnityEngine;
using System.Collections;

public class Blockade : MonoBehaviour
{
	SpriteRenderer sr;
	vNavObstacle vNavOb;

	float targetRadius = 0;

	// Use this for initialization
	void Start ()
	{
		sr = gameObject.GetComponent<SpriteRenderer>();
		vNavOb = gameObject.GetComponent<vNavObstacle>();

		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
		targetRadius = vNavOb.size.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		sr.color = Color.Lerp (sr.color, new Color(sr.color.r, sr.color.g, sr.color.b, 1), Time.time / 100);
		//nmo.radius = Mathf.Lerp(0, targetRadius, Time.time / 2);
	}
}
