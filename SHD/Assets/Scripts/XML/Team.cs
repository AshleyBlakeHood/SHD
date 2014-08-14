using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	public Character one, two, three, four, five;


	public void instantiateTeam( int teamID)
	{

		CharacterXML chars = new CharacterXML ();
		
		chars.getData ();
		
		TeamXML teams = new TeamXML ();
		
		teams.getData ();

        List<string> teamC = teams.getTeam(teamID);

		one = chars.getCharacter (Convert.ToInt32(teamC[1]));
		two = chars.getCharacter (Convert.ToInt32(teamC[2]));
		three = chars.getCharacter (Convert.ToInt32(teamC[3]));
		four = chars.getCharacter (Convert.ToInt32(teamC[4]));
		five = chars.getCharacter (Convert.ToInt32(teamC[5]));

	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
