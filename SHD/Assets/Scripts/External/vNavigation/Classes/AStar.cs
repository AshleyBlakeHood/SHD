using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar : MonoBehaviour
{
	public Rect scanArea = new Rect (-1, -1, 1, 1);
	
	public float sizeWidth = 0.1f;
	public float sizeHeight = 0.1f;
	
	protected int width = 10;
	protected int height = 10;
	
	protected Node[,] map;

	public List<Vector2> GetPath(Vector2 startPos, Vector2 endPos)
	{
		NodeBinaryHeap openList = new NodeBinaryHeap();
		List<Node> closedList = new List<Node> ();
		
		int startX = Mathf.RoundToInt (Mathf.Abs (scanArea.x - startPos.x) / sizeWidth);
		int startY = Mathf.RoundToInt (Mathf.Abs (scanArea.y - startPos.y) / sizeHeight);
		
		Debug.Log ("StartX: " + startX + " StartY: " + startY);
		
		int endX = Mathf.RoundToInt (Mathf.Abs (scanArea.x - endPos.x) / sizeWidth);
		int endY = Mathf.RoundToInt (Mathf.Abs (scanArea.y - endPos.y) / sizeHeight);
		
		Debug.Log ("EndX: " + endX + " EndY: " + endY);

		//Check Path is Solveable
		if (map[startX, startY].passable == false || map[endX, endY].passable == false)
			return null;

		//Set score to 0, could set its parent to itself for path detection if needed.
		map [startX, startY].FScore = 0;
		map [startX, startY].SetParent (startX, startY);
		
		openList.Insert (map [startX, startY]);
		
		while (openList.count > 0)
		{
			Node lowestNode = openList.GetAndRemoveRoot ();
			
			if (lowestNode.x == endX && lowestNode.y == endY)
			{
				List<Vector2> output = new List<Vector2>();
				
				return CalculatePath (lowestNode, output);
			}
			
			//openList.RemoveAt (indexForRemove);
			closedList.Add (lowestNode);
			
			//Get Neighbours
			Node[] primaryNei = new Node[8];
			
			//Default 4
			//Left
			if (lowestNode.x > 0)
				primaryNei[0] = map[lowestNode.x - 1, lowestNode.y];
			
			//Right
			if (lowestNode.x < width - 1)
				primaryNei[1] = map[lowestNode.x + 1, lowestNode.y];
			//Up
			
			if (lowestNode.y < height - 1)
				primaryNei[2] = map[lowestNode.x, lowestNode.y + 1];
			
			//Down
			if (lowestNode.y > 0)
				primaryNei[3] = map[lowestNode.x, lowestNode.y - 1];
			
			//Advanced 4
			//Top Left
			if (lowestNode.y < height - 1 && lowestNode.x > 0)
				primaryNei[4] = map[lowestNode.x - 1, lowestNode.y + 1];
			
			//Bottom Left
			if (lowestNode.y > 0 && lowestNode.x > 0)
				primaryNei[5] = map[lowestNode.x - 1, lowestNode.y - 1];
			
			//Top Right
			if (lowestNode.y < height - 1 && lowestNode.x < width - 1)
				primaryNei[6] = map[lowestNode.x + 1, lowestNode.y + 1];
			
			//Bottom Right
			if (lowestNode.y > 0 && lowestNode.x < width - 1)
				primaryNei[7] = map[lowestNode.x + 1, lowestNode.y - 1];
			
			for (int i = 0; i < 8; i++)
			{
				//Out of bounds check.
				if (primaryNei[i] == null)
					continue;
				
				if (primaryNei[i].passable == false)
					continue;
				
				if (i >= 4)
				{
					if (DiagonalWallCheck (primaryNei[i]) == true)
						continue;
				}
				
				//Checks Neighbour isn't in Closed List
				if (closedList.Contains (primaryNei[i]))
					continue;
				
				if (openList.Contains (primaryNei[i]) == false)
				{
					//Set the parent of the neighbour to the current node.
					primaryNei[i].SetParent (lowestNode.x, lowestNode.y);
					
					//primaryNei[i].GScore = CalculateDistance(primaryNei[i].x, primaryNei[i].y, startX, startY);
					primaryNei[i].HScore = CalculateDistance(primaryNei[i].x, primaryNei[i].y, endX, endY);
					primaryNei[i].FScore = primaryNei[i].GScore + primaryNei[i].HScore;
					
					openList.Insert (primaryNei[i]);
				}
				else
				{
					//Diagonal optomisation will go here. - Actually add a working G cost.
				}
				
				//Add node back to map.
				map[primaryNei[i].x, primaryNei[i].y] = primaryNei[i];
			}
		}
		
		//Output
		List<Vector2> finalPath = new List<Vector2> ();
		return finalPath;
	}
	
	protected void InitializeMap()
	{
		map = new Node[width,height];
		
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				map[x, y] = new Node();
				
				map[x, y].x = x;
				map[x, y].y = y;
				map[x, y].passable = true;
			}
		}
	}

	private List<Vector2> CalculatePath(Node node, List<Vector2> path)
	{
		if (!(node.x == node.parentX && node.y == node.parentY))
		{
			path.Add (new Vector2(node.x * sizeWidth + scanArea.x, node.y * sizeHeight + scanArea.y));
			
			CalculatePath (map[node.parentX, node.parentY], path);
		}
		
		return path;
	}
	
	public int CalculateDistance(int aX, int aY, int bX, int bY)
	{
		//Calculates and returns the Manhatten distance between two points.
		return Mathf.Abs(aX - bX) + Mathf.Abs(aY - bY);
	}
	
	private bool DiagonalWallCheck(Node node)
	{
		if (node.y > 0)
		{
			if (map[node.x, node.y -1].passable == false)
				return true;
		}
		
		if (node.y < height - 1)
		{
			if (map[node.x, node.y + 1].passable == false)
				return true;
		}
		
		if (node.x > 0)
		{
			if (map[node.x - 1, node.y].passable == false)
				return true;
		}
		
		if (node.x < width - 1)
		{
			if (map[node.x + 1, node.y].passable == false)
				return true;
		}
		
		return false;
	}
	
	public void FillMapArea(Vector2 centre, Vector2 size)
	{
		if (map == null)
			return;
		
		for (int x = Mathf.RoundToInt (((centre.x - (size.x / 2)) + scanArea.width) / sizeWidth); x < ((centre.x + (size.x / 2)) + scanArea.width) / sizeWidth; x++)
		{
			for (int y = Mathf.RoundToInt (((centre.y - (size.y / 2)) + scanArea.height) / sizeHeight); y < ((centre.y + (size.y /2)) + scanArea.height) / sizeHeight; y++)
			{
				map[x, y].AddObstacle();
			}
		}
	}
	
	public void ClearMapArea(Vector2 centre, Vector2 size)
	{
		if (map == null)
			return;
		
		for (int x = Mathf.RoundToInt (((centre.x - (size.x / 2)) + scanArea.width) / sizeWidth); x < ((centre.x + (size.x / 2)) + scanArea.width) / sizeWidth; x++)
		{
			for (int y = Mathf.RoundToInt (((centre.y - (size.y / 2)) + scanArea.height) / sizeHeight); y < ((centre.y + (size.y /2)) + scanArea.height) / sizeHeight; y++)
			{
				map[x, y].RemoveObstacle();
			}
		}
	}
	
	public bool IsNodePassable(Vector2 pos)
	{
		//Debug.Log ("X: " + Mathf.RoundToInt (Mathf.Abs (scanArea.x - pos.x) / sizeWidth) + " Y: " + Mathf.RoundToInt (Mathf.Abs (scanArea.y - pos.y) / sizeHeight));
		return map[Mathf.RoundToInt (Mathf.Abs (scanArea.x - pos.x) / sizeWidth), Mathf.RoundToInt (Mathf.Abs (scanArea.y - pos.y) / sizeHeight)].passable;
	}

	void OnDrawGizmosSelected()
	{
		//If the map hasn't been initalized, don't loop through.
		if (map != null)
		{
			//Loop through map.
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					//Set colour based on if passable currently or not.
					if (map[x, y].passable == true)
						Gizmos.color = new Color(0, 1, 0, 0.2f);
					else
						Gizmos.color = new Color(1, 0, 0, 0.2f);
					
					//Draw cube.
					Gizmos.DrawCube(new Vector3((x * sizeWidth) + scanArea.x, (y * sizeHeight) + scanArea.y, 0), new Vector3(sizeWidth, sizeHeight, 0));
				}
			}
		}

		//Draw area that is being scanned.
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube (new Vector3 (transform.position.x + scanArea.x + scanArea.width, transform.position.y + scanArea.y + scanArea.height, transform.position.z), new Vector3 (scanArea.width * 2, scanArea.height * 2, transform.position.z));
	}
}
