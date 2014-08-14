using UnityEngine;
using System.Collections;
using System.Xml;

/*

A quick note on character ids
The character ID -1 will always be at the end of the character cache
This makes it and its data accesable at teamData.characterCache[teamData.characterCache.Length-1]
Was easier than trying to fit it at the begining and adjusting the indexes to work

*/

public class TeamBuilderGUI : MonoBehaviour{

	int selectedSlot = 0, selectedCharID = 0, selectedTeam = 0, teamSize = 5;
	float buttonWidth = 80, buttonHeight = 80, buttonX = 30, buttonY = 10;
	Texture selectedTexture;
	TeamXML teamData = new TeamXML();
	
	// Use this for initialization
	void Start () {
		//Populates team data object with XML data and creates caches for teams and characters
		teamData.getData();	
	}

	void OnGUI() {
		drawTeamSelectButtons();
		drawSaveButton();
		drawNavButtons();
		drawLabels();
		drawCharButtons();
	}
	
	void drawCharButtons()
	{
		buttonX = 10;
		buttonY = 50;
		int i = 0;
		int j = 1;
		//This is very basic and will be changed to a more robust method for final release
		foreach(CharacterContainer character in teamData.characterCache)
		{
			if(GUI.Button(new Rect(buttonX,buttonY,buttonWidth, buttonHeight), character.iconTexture))
			{
				buttonPress(character.id);
			}
			buttonX+=buttonWidth;
			if(j==3)
			{
				buttonX = 10;
				buttonY+=buttonHeight;
				j=1;
			}
			else
			{
				j++;
			}
			i++;
		}
	}
	
	void drawTeamSelectButtons()
	{
		float bx = Screen.width- buttonWidth - 50, by = 50;
		int i = 0;
		//Same as character buttons, will probably change
		foreach(CharacterContainer character in teamData.teamsCache[selectedTeam].teamArray)
		{
			if(character.id == -1)
			{
				if(GUI.Button(new Rect(bx,by,buttonWidth, buttonHeight), teamData.characterCache[teamData.characterCache.Length-1].iconTexture))
				{
					teamButtonPress(i);
				}
			}
			else
			{
				if(GUI.Button(new Rect(bx,by,buttonWidth, buttonHeight), teamData.teamsCache[selectedTeam].teamArray[i].iconTexture))
				{
					teamButtonPress(i);
				}
			}
			by+=buttonHeight;
			i++;
		}
	}
	
	void drawSaveButton()
	{
		if(GUI.Button(new Rect(Screen.width/2-50, Screen.height - 70, 100,50), "Save"))
		{
			teamData.saveTeam(selectedTeam);
		}
	}
	
	void drawNavButtons()
	{
		if(GUI.Button(new Rect(Screen.width/2-150, Screen.height - 70, 30,50), "<"))
		{
			prevTeam();
		}
		
		if(GUI.Button(new Rect(Screen.width/2+130, Screen.height - 70, 30,50), ">"))
		{
			nextTeam();
		}
		
		if(GUI.Button(new Rect(20, Screen.height - 70, 50,50), "Menu"))
		{
			Application.LoadLevel("Main Menu");
		}
	}
	
	void drawLabels()
	{
		GUI.Label(new Rect(Screen.width - 150, 10, 150,30), "Team " + (selectedTeam + 1) + " of " + teamData.teamList.Count + " selected");
	}
	
	void buttonPress(int id)
	{	
		Debug.Log(id);
		//Checks if the character is in the team, only -1 can be placed multiple times
		bool isInTeam = false;
		foreach(CharacterContainer cont in teamData.teamsCache[selectedTeam].teamArray)
		{
			if(cont.id == id)
				isInTeam = true;
		}

		if(!isInTeam && id != -1)
			teamData.teamsCache[selectedTeam].teamArray[selectedSlot] = teamData.characterCache[id];
		else if(id == -1)
			teamData.teamsCache[selectedTeam].teamArray[selectedSlot] = teamData.characterCache[teamData.characterCache.Length-1];
	}
	
	void teamButtonPress(int bID)
	{
		Debug.Log(bID);
		selectedSlot = bID;
		Debug.Log("Selected Slot: " + bID);
	}
	
	void prevTeam()
	{
		if(selectedTeam != 0)
		{
			selectedTeam--;
		}
	}
	
	void nextTeam()
	{
		if(selectedTeam != teamData.teamList.Count-1)
		{
			selectedTeam++;
		}
	}
}
