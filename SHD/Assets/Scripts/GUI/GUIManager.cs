using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{
	GameManager manager;

	public GUISkin selectButton;

	// Use this for initialization
	void Start () 
	{
		manager = GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect(10, 10, 150, 30), "Super Strength", selectButton.button))
		{
			manager.gameObject.GetComponent<SuperStrength>().activePower = true;
			manager.gameObject.GetComponent<Telekenesis>().activePower = false;
			manager.gameObject.GetComponent<MindControl>().activePower = false;
			manager.gameObject.GetComponent<TimeControl>().activePower = false;
			manager.gameObject.GetComponent<Blockades>().activePower = false;
			manager.gameObject.GetComponent<Wind>().activePower = false;
			manager.gameObject.GetComponent<Lightning>().activePower = false;
			manager.gameObject.GetComponent<Oil>().activePower = false;
			manager.gameObject.GetComponent<Fire>().activePower = false;
			manager.gameObject.GetComponent<Tsunami>().activePower = false;
		}
		
		if (GUI.Button (new Rect(180, 10, 150, 30), "Telekenesis", selectButton.button))
		{
			manager.gameObject.GetComponent<Telekenesis>().activePower = true;
			manager.gameObject.GetComponent<SuperStrength>().activePower = false;
			manager.gameObject.GetComponent<MindControl>().activePower = false;
			manager.gameObject.GetComponent<TimeControl>().activePower = false;
			manager.gameObject.GetComponent<Blockades>().activePower = false;
			manager.gameObject.GetComponent<Wind>().activePower = false;
			manager.gameObject.GetComponent<Lightning>().activePower = false;
			manager.gameObject.GetComponent<Oil>().activePower = false;
			manager.gameObject.GetComponent<Fire>().activePower = false;
			manager.gameObject.GetComponent<Tsunami>().activePower = false;
		}
		
		if (GUI.Button (new Rect(350, 10, 150, 30), "Mind Control", selectButton.button))
		{
			manager.gameObject.GetComponent<MindControl>().activePower = true;
			manager.gameObject.GetComponent<Telekenesis>().activePower = false;
			manager.gameObject.GetComponent<SuperStrength>().activePower = false;
			manager.gameObject.GetComponent<TimeControl>().activePower = false;
			manager.gameObject.GetComponent<Blockades>().activePower = false;
			manager.gameObject.GetComponent<Wind>().activePower = false;
			manager.gameObject.GetComponent<Lightning>().activePower = false;
			manager.gameObject.GetComponent<Oil>().activePower = false;
			manager.gameObject.GetComponent<Fire>().activePower = false;
			manager.gameObject.GetComponent<Tsunami>().activePower = false;
		}
		
		if (GUI.Button (new Rect(520, 10, 150, 30), "Time Control", selectButton.button))
		{
			manager.gameObject.GetComponent<TimeControl>().activePower = true;
			manager.gameObject.GetComponent<MindControl>().activePower = false;
			manager.gameObject.GetComponent<Telekenesis>().activePower = false;
			manager.gameObject.GetComponent<SuperStrength>().activePower = false;
			manager.gameObject.GetComponent<Blockades>().activePower = false;
			manager.gameObject.GetComponent<Wind>().activePower = false;
			manager.gameObject.GetComponent<Lightning>().activePower = false;
			manager.gameObject.GetComponent<Oil>().activePower = false;
			manager.gameObject.GetComponent<Fire>().activePower = false;
			manager.gameObject.GetComponent<Tsunami>().activePower = false;
		}
		
		if (GUI.Button (new Rect(690, 10, 150, 30), "Blockades", selectButton.button))
		{
			manager.gameObject.GetComponent<Blockades>().activePower = true;
			manager.gameObject.GetComponent<TimeControl>().activePower = false;
			manager.gameObject.GetComponent<MindControl>().activePower = false;
			manager.gameObject.GetComponent<Telekenesis>().activePower = false;
			manager.gameObject.GetComponent<SuperStrength>().activePower = false;
			manager.gameObject.GetComponent<Wind>().activePower = false;
			manager.gameObject.GetComponent<Lightning>().activePower = false;
			manager.gameObject.GetComponent<Oil>().activePower = false;
			manager.gameObject.GetComponent<Fire>().activePower = false;
			manager.gameObject.GetComponent<Tsunami>().activePower = false;
		}
		//END OF JORDANS POWERS
		
		if (GUI.Button (new Rect(10, Screen.height - 40, 150, 30), "Wind", selectButton.button))
		{
			manager.gameObject.GetComponent<Wind>().activePower = true;
			manager.gameObject.GetComponent<SuperStrength>().activePower = false;
			manager.gameObject.GetComponent<Telekenesis>().activePower = false;
			manager.gameObject.GetComponent<MindControl>().activePower = false;
			manager.gameObject.GetComponent<TimeControl>().activePower = false;
			manager.gameObject.GetComponent<Blockades>().activePower = false;
			manager.gameObject.GetComponent<Lightning>().activePower = false;
			manager.gameObject.GetComponent<Oil>().activePower = false;
			manager.gameObject.GetComponent<Fire>().activePower = false;
			manager.gameObject.GetComponent<Tsunami>().activePower = false;
		}
		
		if (GUI.Button (new Rect(180, Screen.height - 40, 150, 30), "Lightning", selectButton.button))
		{
			manager.gameObject.GetComponent<Lightning>().activePower = true;
			manager.gameObject.GetComponent<Telekenesis>().activePower = false;
			manager.gameObject.GetComponent<SuperStrength>().activePower = false;
			manager.gameObject.GetComponent<MindControl>().activePower = false;
			manager.gameObject.GetComponent<TimeControl>().activePower = false;
			manager.gameObject.GetComponent<Blockades>().activePower = false;
			manager.gameObject.GetComponent<Wind>().activePower = false;
			manager.gameObject.GetComponent<Oil>().activePower = false;
			manager.gameObject.GetComponent<Fire>().activePower = false;
			manager.gameObject.GetComponent<Tsunami>().activePower = false;
		}
		
		if (GUI.Button (new Rect(350, Screen.height - 40, 150, 30), "Oil", selectButton.button))
		{
			manager.gameObject.GetComponent<Oil>().activePower = true;
			manager.gameObject.GetComponent<MindControl>().activePower = false;
			manager.gameObject.GetComponent<Telekenesis>().activePower = false;
			manager.gameObject.GetComponent<SuperStrength>().activePower = false;
			manager.gameObject.GetComponent<TimeControl>().activePower = false;
			manager.gameObject.GetComponent<Blockades>().activePower = false;
			manager.gameObject.GetComponent<Wind>().activePower = false;
			manager.gameObject.GetComponent<Lightning>().activePower = false;
			manager.gameObject.GetComponent<Fire>().activePower = false;
			manager.gameObject.GetComponent<Tsunami>().activePower = false;
		}
		
		if (GUI.Button (new Rect(520, Screen.height - 40, 150, 30), "Fire", selectButton.button))
		{
			manager.gameObject.GetComponent<Fire>().activePower = true;
			manager.gameObject.GetComponent<TimeControl>().activePower = false;
			manager.gameObject.GetComponent<MindControl>().activePower = false;
			manager.gameObject.GetComponent<Telekenesis>().activePower = false;
			manager.gameObject.GetComponent<SuperStrength>().activePower = false;
			manager.gameObject.GetComponent<Blockades>().activePower = false;
			manager.gameObject.GetComponent<Wind>().activePower = false;
			manager.gameObject.GetComponent<Lightning>().activePower = false;
			manager.gameObject.GetComponent<Oil>().activePower = false;			
			manager.gameObject.GetComponent<Tsunami>().activePower = false;
		}
		
		if (GUI.Button (new Rect(690, Screen.height - 40, 150, 30), "Tsunami", selectButton.button))
		{
			manager.gameObject.GetComponent<Tsunami>().activePower = true;
			manager.gameObject.GetComponent<Blockades>().activePower = false;
			manager.gameObject.GetComponent<TimeControl>().activePower = false;
			manager.gameObject.GetComponent<MindControl>().activePower = false;
			manager.gameObject.GetComponent<Telekenesis>().activePower = false;
			manager.gameObject.GetComponent<SuperStrength>().activePower = false;
			manager.gameObject.GetComponent<Wind>().activePower = false;
			manager.gameObject.GetComponent<Lightning>().activePower = false;	
			manager.gameObject.GetComponent<Oil>().activePower = false;
			manager.gameObject.GetComponent<Fire>().activePower = false;
			
		}
	}
}
