using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;

[System.Serializable]
public class CharacterXML{

	public TextAsset characterSheet;
	public XmlNodeList heroList;
	public bool hasXML;
	public int characterCount;
	XmlDocument rawHeroList = new XmlDocument();

	// Use this for initialization
	void Start () {
	}
	
	public void getData()
	{//Loads XML sheet in to readable XML document
		characterSheet = Resources.Load("Characters") as TextAsset;
		try
		{
			rawHeroList.LoadXml(characterSheet.text);
			heroList = rawHeroList.SelectNodes("/CHARACTERS/CHARACTER");
			characterCount = heroList.Count;
			Debug.Log("Has XML");
			hasXML = true;
		}
		catch
		{
			hasXML = false;
		}  
	}
	
	public Character getCharacter (int charID)
	{
		foreach (XmlNode node in heroList)
		{
			if(charID.ToString() == node.Attributes["id"].InnerText)
			{
				Debug.Log(node.ChildNodes[0].InnerText + " IS FOUND");
				return getCharacterData(node);
			}
		}
		return null;	
	}
	
	public Character getCharacterByName(string name)
	{
		foreach (XmlNode node in heroList)
		{
			if(name == node.ChildNodes[0].InnerText)
			{
				Debug.Log(node.ChildNodes[0].InnerText + " IS FOUND");
				return getCharacterData(node);
			}
		}
		return null;
	}
	
	public Character getCharacterData(XmlNode currentChar)
	{
	//Creates and returns new character object
		Character temp = new Character();
		temp.cid = currentChar.Attributes["id"].Value;
		temp.cname = currentChar.ChildNodes[0].InnerText;
		temp.cimage = currentChar.ChildNodes[1].InnerText;
		temp.crace = currentChar.ChildNodes[2].InnerText;
		temp.cgender = currentChar.ChildNodes[3].InnerText;
		temp.cability = currentChar.ChildNodes[4].InnerText;
		temp.cbio = currentChar.ChildNodes[5].InnerText;
		temp.cabilityclass = currentChar.ChildNodes[6].InnerText;
		return temp;
	}

	}
