using UnityEngine;
using System.Collections;

public class vNavObstacle : MonoBehaviour
{
	public Vector2 centre = Vector2.zero;
	public Vector2 size = Vector2.one;

	public AStar navFloor;

	// Use this for initialization
	void Start ()
	{
//		if (navFloor == null)
//			navFloor = GameObject.Find ("vNavigation").GetComponent<vNavRay> ();
//
//		//Fills the nav map on start - Make sure this script executes after the AStar Nav scripts otherwise the obstacle won't be created.
//		navFloor.FillMapArea (new Vector2(transform.position.x, transform.position.y) + centre, size);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnDisable()
	{
		//On disable the area is cleared of this obstacle.
		navFloor.ClearMapArea (new Vector2(transform.position.x, transform.position.y) + centre, size);
	}

	void OnEnable()
	{
		if (navFloor == null)
			navFloor = GameObject.Find ("vNavigation").GetComponent<vNavRay> ();

		//On enable refill the area.
		navFloor.FillMapArea (new Vector2(transform.position.x, transform.position.y) + centre, size);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube (new Vector2(transform.position.x, transform.position.y) + centre, size);
	}
}