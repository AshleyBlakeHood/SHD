using UnityEngine;
using System.Collections;

public class TeamSelectGUI : MonoBehaviour {

	float screenSpaceDivision = 0, buttonWidth = 80, buttonHeight = 80;
	
	// Use this for initialization
	void Start () {
		screenSpaceDivision = (Screen.width/PlayerPrefs.GetInt("unlockedTeams"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		for (int i = 1; i <= PlayerPrefs.GetInt("unlockedTeams"); i++)
		{
			if(GUI.Button(new Rect(((screenSpaceDivision * i) - buttonWidth/2)-screenSpaceDivision/2, Screen.height/2 - buttonHeight/2, buttonWidth, buttonHeight), i.ToString()))
			{
				PlayerPrefs.SetInt("selectedTeam", i);
				PlayerPrefs.Save();
			}
		}
		
		if(GUI.Button(new Rect(Screen.width/2 - buttonWidth/2, Screen.height-100, buttonWidth, buttonHeight), "Play"))
		{
			Application.LoadLevel("Main Scene");
		}
		
		if(GUI.Button(new Rect(20, Screen.height-100, buttonWidth, buttonHeight), "Back"))
		{
			Application.LoadLevel("Main Menu");
		}
		
		GUI.Label(new Rect(Screen.width/2-50, 30, 100,50), "Team " + PlayerPrefs.GetInt("selectedTeam").ToString() + " selected");
	}
}
