using UnityEngine;
using System.Collections;

[System.Serializable]
public class characterLoaderTest : MonoBehaviour {

	string currentID;
	int searchID = 0;
	public CharacterXML testingXMLLoading;
	public Character currentCharacter;
	// Use this for initialization
	void Start () {
		testingXMLLoading.getData();
		currentCharacter = testingXMLLoading.getCharacter(searchID);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.inputString != "")
		{
			currentID = Input.inputString;
			Debug.Log(currentID);
			switch(currentID)
			{
				case "1":
				searchID = 1;
				break;
			case "2":
				searchID = 2;
				break;
			case "3":
				searchID = 3;
				break;
			case "4":
				searchID = 4;
				break;
			case "5":
				searchID = 5;
				break;
			case "6":
				searchID = 6;
				break;
			case "7":
				searchID = 7;
				break;
			case "8":
				searchID = 8;
				break;
			case "9":
				searchID = 9;
				break;
			case "0":
				searchID = 0;
				break;
				
			}
			currentCharacter = testingXMLLoading.getCharacter(searchID);
			
			gameObject.AddComponent(currentCharacter.cabilityclass);
		}
		
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10,10,500,100), "Name: " + currentCharacter.cname);
		GUI.Label(new Rect(10,60,500,100), "Image: " + currentCharacter.cimage);
		GUI.Label(new Rect(10,110,500,100), "Race: " + currentCharacter.crace);
		GUI.Label(new Rect(10,160,500,100), "Gender: " + currentCharacter.cgender);
		GUI.Label(new Rect(10,210,500,100), "Ability: " + currentCharacter.cability);
		GUI.Label(new Rect(10,260,500,300), "Bio: " + currentCharacter.cbio);
		GUI.Label(new Rect(10,460,500,100), "Ability Class: " + currentCharacter.cabilityclass);
		GUI.Label(new Rect(10,510,500,100), "ID: " + currentCharacter.cid);
	}
}
