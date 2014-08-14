using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vNavRay : AStar
{
	// Use this for initialization
	void Start ()
	{
		width = Mathf.RoundToInt ((Mathf.Abs (scanArea.x) + Mathf.Abs (scanArea.width)) / sizeWidth);
		height = Mathf.RoundToInt ((Mathf.Abs (scanArea.y) + Mathf.Abs (scanArea.height)) / sizeHeight);

		InitializeMap ();
		RayCastCheck ();
	}

	//void Update() { RayCastCheck (); }

	public void RayCastCheck()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				RaycastHit2D hit = Physics2D.Raycast (new Vector3((x * sizeWidth) + scanArea.x, (y * sizeHeight) + scanArea.y, -5), new Vector3(0, 0, 1), 10);
				Debug.DrawLine (new Vector3((x * sizeWidth) + scanArea.x, (y * sizeHeight) + scanArea.y, -1), new Vector3((x * sizeWidth) + scanArea.x, (y * sizeHeight) + scanArea.y, 1));

				if (hit.collider != null)
				{
					if (hit.transform.gameObject.isStatic)
					{
						map[x, y].passable = false;
						map[x, y].staticUnpassable = true;
					}
				}
			}
		}
	}
}