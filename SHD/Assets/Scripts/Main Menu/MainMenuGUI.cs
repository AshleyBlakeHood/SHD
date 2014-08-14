using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	// Use this for initialization
	
	int buttonWidth = 200, buttonHeight = 40, spacer = 10;
	
	void Start () {
		if(!PlayerPrefs.HasKey("selectedTeam"))
		{
			PlayerPrefs.SetInt("selectedTeam", 1);
		}
		
		if(!PlayerPrefs.HasKey("unlockedTeams"))
		{
			PlayerPrefs.SetInt("unlockedTeams", 2);
		}
		PlayerPrefs.Save();
	}
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		drawButtons();
	}
	
	void drawButtons()
	{
		if(GUI.Button(new Rect(Screen.width/2 - (buttonWidth/2), Screen.height/2, buttonWidth, buttonHeight), "Play"))
		{
			runGame();
		} 
		
		if(GUI.Button(new Rect(Screen.width/2 - (buttonWidth/2), Screen.height/2 + (buttonHeight + spacer), buttonWidth, buttonHeight), "Team Builder"))
		{
			runTeamBuilder();
		} 
		
		if(GUI.Button(new Rect(Screen.width/2 - (buttonWidth/2), Screen.height/2  + (buttonHeight + spacer) * 2, buttonWidth, buttonHeight), "Instructions"))
		{
			runInstructions();
		} 
		
		if(GUI.Button(new Rect(Screen.width/2 - (buttonWidth/2), Screen.height/2 +  (buttonHeight + spacer) * 3, buttonWidth, buttonHeight), "Quit"))
		{
			Application.Quit();
		} 
	}
	
	void runGame()
	{
		Application.LoadLevel("Team Select");
	}
	
	void runTeamBuilder()
	{
		Application.LoadLevel("Team Builder");
	}
	
	void runInstructions()
	{
	}
}
