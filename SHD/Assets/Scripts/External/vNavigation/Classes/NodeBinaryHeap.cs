using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeBinaryHeap
{
	private List<Node> items = new List<Node>();
	
	public int count
	{
		get
		{
			return items.Count;
		}
	}
	
	public bool Contains(Node item)
	{
		return items.Contains (item);
	}
	
	public void Insert(Node item)
	{
		int i = count;
		items.Add (item);
		
		while (i > 0 && items[(i - 1) / 2].FScore > item.FScore)
		{
			items[i] = items[(i - 1) / 2];
			i = (i - 1) / 2;
		}
		
		items [i] = item;
	}
	
	public Node GetRoot()
	{
		return items [0];
	}
	
	public Node GetAndRemoveRoot()
	{
		Node root = items [0];
		
		Node lastItem = items [items.Count - 1];
		items.RemoveAt (items.Count - 1);
		
		if (items.Count > 0)
		{
			int i = 0;
			
			while (i < items.Count / 2)
			{
				int j = (i * 2) + 1;
				
				if ((j < items.Count - 1) && (items[j].FScore > items[j + 1].FScore))
				{
					j++;
				}
				
				if (items[j].FScore > lastItem.FScore)
					break;
				
				items[i] = items[j];
				i = j;
			}
			
			items[i] = lastItem;
		}
		
		return root;
	}
}