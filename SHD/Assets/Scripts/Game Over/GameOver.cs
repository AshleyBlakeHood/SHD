using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
	GUIStyle g = new GUIStyle();
	public float points = 0;

	// Use this for initialization
	void Start ()
	{
		g.alignment = TextAnchor.UpperCenter;
		points = PlayerPrefs.GetFloat ("LastScore");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI()
	{
		GUI.Label (new Rect(10, 225, Screen.width - 20, 300), "GAME OVER\n\nPoints: " + points, g);

		if (GUI.Button (new Rect((Screen.width / 2) / 2 - 50, 300, 100, 50), "Replay"))
			Application.LoadLevel (0);

		if (GUI.Button (new Rect(((Screen.width / 2) / 2) * 3 - 50, 300, 100, 50), "Quit"))
			Application.Quit();
	}
}
