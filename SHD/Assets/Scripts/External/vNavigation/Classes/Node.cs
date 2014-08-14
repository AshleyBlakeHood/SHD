using UnityEngine;
using System.Collections;

public class Node
{
	public int x;
	public int y;
	public bool passable;
	public int cost;
	public int FScore;
	public int GScore;
	public int HScore;
	
	public int parentX;
	public int parentY;

	public bool staticUnpassable = false;

	private int obstacleCount = 0;

	public void AddObstacle()
	{
		obstacleCount++;

		passable = false;
	}

	public void RemoveObstacle()
	{
		if (obstacleCount <= 1 && !staticUnpassable)
			passable = true;

		if (obstacleCount > 0)
			obstacleCount--;
	}

	public void SetCoords(int ix, int iy)
	{
		x = ix;
		y = iy;
	}
	
	public void SetParent(int x, int y)
	{
		parentX = x;
		parentY = y;
	}
}