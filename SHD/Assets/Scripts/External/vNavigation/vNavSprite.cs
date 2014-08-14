using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vNavSprite : AStar
{
	public SpriteRenderer unwalkableSprite;
	public Color unwalkableColour = Color.red;

	// Use this for initialization
	void Start ()
	{
		//Calculate width and height from the size of the nav sprite and desired size difference in Unity Units.
		width = Mathf.RoundToInt ((unwalkableSprite.sprite.bounds.extents.x * 2) / sizeWidth);
		height = Mathf.RoundToInt ((unwalkableSprite.sprite.bounds.extents.y * 2) / sizeHeight);

		//Populate the scan area rect with information regarding the sprite to Unity space. (Usually 1 Unit = 100 Pixels).
		scanArea.x = (unwalkableSprite.sprite.bounds.extents.x * -1) + unwalkableSprite.transform.position.x;
		scanArea.y = (unwalkableSprite.sprite.bounds.extents.y * -1) + unwalkableSprite.transform.position.y;
		scanArea.width = unwalkableSprite.sprite.bounds.extents.x;
		scanArea.height = unwalkableSprite.sprite.bounds.extents.y;

		//Create the array to store the node information.
		InitializeMap ();

		//Populate the map with passable and impassable terrain.
		SpriteCheck ();
	}

	public void SpriteCheck()
	{
		//Loops through the map.
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				//Take the current x and times by 100 (Unity unit to pixel conversion) then check the pixel colour to see if it is impassable.
				if (unwalkableSprite.sprite.texture.GetPixel (Mathf.RoundToInt (x * (sizeWidth * 100)), Mathf.RoundToInt (y * (sizeHeight * 100))) == unwalkableColour)
				{
					//Set node as impassable and that it's statically impassable (done so that when an obstacle overlaps it it stays impassable after removal).
					map[x, y].passable = false;
					map[x, y].staticUnpassable = true;
				}
			}
		}
	}
}