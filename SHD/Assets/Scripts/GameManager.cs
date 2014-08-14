using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	GameObject house;
	public GameObject background;

	public GameObject bullet;
	public GameObject dynamicObjectHolder;


	public float points = 0;
	
	public TeamContainer chosenTeam;
	TeamXML teams = new TeamXML();
	
	public GameObject GetHouse()
	{
		return house;
	}

	// Use this for initialization
	void Start ()
	{
		teams.getData();
		chosenTeam = teams.getTeamByID(PlayerPrefs.GetInt("selectedTeam"));
		house = GameObject.FindGameObjectWithTag ("Defence Point");

		GameObject dynFind = GameObject.Find ("Dynamic Objects");

		if (dynFind != null)
			dynamicObjectHolder = dynFind;
		else
			dynamicObjectHolder = new GameObject("Dynamic Objects");

		//Jordan Add Power Code
		for (int i = 0; i < chosenTeam.teamArray.Length; i++)
		{
			Debug.Log ("PC: " + chosenTeam.teamArray[i].powerClass);
			gameObject.AddComponent (chosenTeam.teamArray[i].powerClass);
			Debug.Log ("PC: " + chosenTeam.teamArray[i].powerClass + "Out");
		}

		Component[] c = gameObject.GetComponents (typeof(Component));

		for (int i = 0; i < 5; i++)
		{
			gameObject.AddComponent (c[0].GetType ());
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}
