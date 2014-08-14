using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewerGUI : CharacterXML{

	List<Character> charList = new List<Character>();
	// Use this for initialization
	void Start () {
		//rawCharData = new XMLHandler();
		characterSheet = Resources.Load("Characters") as TextAsset;
		getData();
		
		getCharacterByName("Slaye");
		Character doobies = getCharacter(3);
		
		for(int i = 0; i<heroList.Count; i++)
		{
			charList.Add(getCharacter(i));
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,-1,Screen.width, Screen.height), Resources.Load("tbBG") as Texture2D);
		displayCharSelect();
	}
	
	void displayCharSelect()
	{
		
		
		foreach (Character tempChar in charList)
		{
			GUI.Button(new Rect(0,0,80,80),tempChar.cname);
		}
	}
}
