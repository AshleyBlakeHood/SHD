using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;

[System.Serializable]
public class TeamXML:MonoBehaviour{
	
	public TextAsset teamSheet;
	public XmlNodeList teamList;
	public int teamCount, teamSize = 5;
	public bool hasTeam;
	XmlDocument rawTeamList = new XmlDocument();
	public CharacterContainer[] characterCache;
	public TeamContainer[] teamsCache;
	public CharacterXML charDetails = new CharacterXML();
	
	void Start()
	{
	}
	
	public void getData() // Called at the start
	{
		//Checks if PlayerTeams.xml exists, if not creates one from template
		if(!System.IO.File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "PlayerTeams.xml"))
		{
			Debug.Log("NO XML");
			TextAsset XMLData = Resources.Load("PlayerTeams") as TextAsset;
			System.IO.File.WriteAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "PlayerTeams.xml", XMLData.ToString());
			
		}
		
		//Trys to load the team data, should never fail as at least 2 teams should always exist
		try
		{
			rawTeamList.Load(Application.persistentDataPath + Path.DirectorySeparatorChar + "PlayerTeams.xml"); // Loads the XML file from the data path
			Debug.Log(1);
			teamList = rawTeamList.SelectNodes("/TEAMS/TEAM");
			teamCount = teamList.Count;
			Debug.Log("Has teams");
			hasTeam = true;
		}
		catch
		{
			Debug.Log("No teams");
			hasTeam = false;
		} 
		
		charDetails.getData();
		characterCache = cacheIcons(charDetails.characterCount);
		teamsCache = cacheTeams(teamCount);
	}
	
	public void newTeam() // Saves a new node to the file
	{
		//Selects the parent node, in this case TEAMS, to place the new child in
		XmlNode container = rawTeamList.SelectSingleNode("/TEAMS");
		//Inititialising a new child node
		XmlNode newMainNode;
		//Populates new child node 
		newMainNode = rawTeamList.CreateNode(XmlNodeType.Element, "TEAM", null);
		
		//Creates new attribute to be appeded to the child node
		//this is the team ID, will automatically increment by team count
		XmlAttribute id = rawTeamList.CreateAttribute("id");
		id.Value = teamCount.ToString();
		newMainNode.Attributes.Append(id);
		
		//Creates empty node to contain the appending node
		XmlNode childNode1 = null;
		
		//Appends a series of nodes to the child node
		appendChildToSheet(ref childNode1, ref newMainNode, "c1", "-1"); // Adds the nodes
		appendChildToSheet(ref childNode1, ref newMainNode, "c2", "-1");
		appendChildToSheet(ref childNode1, ref newMainNode, "c3", "-1");
		appendChildToSheet(ref childNode1, ref newMainNode, "c4", "-1");
		appendChildToSheet(ref childNode1, ref newMainNode, "c5", "-1");
		
		//Appends new child to parent nodes
		container.AppendChild(newMainNode);
		
		//Saves and reloads the xml document
		rawTeamList.Save(Application.persistentDataPath + Path.DirectorySeparatorChar + "PlayerTeams.xml");
		rawTeamList.Load(Application.persistentDataPath + Path.DirectorySeparatorChar + "PlayerTeams.xml"); // Loads the XML file from the data path
		teamList = rawTeamList.SelectNodes("/TEAMS/TEAM");
		teamCount = teamList.Count; // Resets the team count
	}

	private void appendChildToSheet(ref XmlNode newNode, ref XmlNode targetNode, string nodeName, string text)
	{
		//creates a new, empty node with the name nodeName
		newNode = rawTeamList.CreateNode(XmlNodeType.Element, nodeName, null);
		newNode.InnerText = text;
		targetNode.AppendChild(newNode);
	}

	CharacterContainer[] cacheIcons(int heroCount)
	{
		//Creates the character cache... not much else to say
		CharacterContainer[] tempcharacterCache = new CharacterContainer[heroCount+1];
		int i = 0;
		foreach(XmlNode node in charDetails.heroList)
		{		
			CharacterContainer temp = new CharacterContainer();
			temp.iconTexture = Resources.Load(node.ChildNodes[1].InnerText) as Texture;
			int.TryParse(node.Attributes["id"].InnerText, out temp.id);
			temp.powerClass = node.ChildNodes[6].InnerText;
			tempcharacterCache[i] = temp;
			i++;
		}
		CharacterContainer blankTemp = new CharacterContainer();
		blankTemp.id = -1;
		blankTemp.iconTexture = Resources.Load("noneicon") as Texture;
		tempcharacterCache[tempcharacterCache.Length-1] = blankTemp;
		return tempcharacterCache;
	}
	
	TeamContainer[] cacheTeams(int teamCount)
	{
		TeamContainer[] tempTeamCache = new TeamContainer[teamCount];
		int i = 0;
		//node is the complete team node with all the character ID's in it as children
		//i increments through the temp cache object
		foreach(XmlNode node in teamList)
		{
			//Temporary Team Container, removes null reference issues
			TeamContainer tempTeamContainer = new TeamContainer();
			tempTeamContainer.teamArray = new CharacterContainer[teamSize];
			//Increments through the XML datas child nodes
			for(int j = 0; j<teamSize; j++)
			{
				foreach(CharacterContainer cont in characterCache)
				{
					CharacterContainer temp = new CharacterContainer();
					int nodeid;
					int.TryParse(node.ChildNodes[j].InnerText, out nodeid);
					//If the IDs match the container object is populated and put in to the temp team container
					if(cont.id == nodeid)
					{
						temp.iconTexture = cont.iconTexture;
						temp.id = cont.id;
						temp.powerClass = cont.powerClass;
						tempTeamContainer.teamArray[j] = temp;
						break;
					}
				}
			}
			//the teap team container is pushed to the temp team cache, this will happen even if the ID couldn't be found
			//it will add a blank object as if the characters id is -1
			tempTeamCache[i] = tempTeamContainer;
			i++;
		}	
		return tempTeamCache;
	}
	
	public void saveTeam(int selectedTeam)
	{	
		int i = 0;
		//Populates the node with the new data, that node will be saved back in to the sheet later
		foreach(CharacterContainer cont in teamsCache[selectedTeam].teamArray)
		{
			teamList[selectedTeam].ChildNodes[i].InnerText = cont.id.ToString();
			i++;
		}
		saveCurrent(teamList[selectedTeam], selectedTeam);
		//Re-caches the data with the new teams
		teamsCache = cacheTeams(teamList.Count);
	}
	
	public void saveCurrent(XmlNode currentNode, int selectedTeam)
	{
		//Selects a node from the XML sheet by the team ID
		XmlNode temp = rawTeamList.SelectSingleNode("/TEAMS/TEAM[@id='" + selectedTeam + "']");
		//Selects all the team nodes, this allows access to all the nodes later
		XmlNode docTemp = rawTeamList.SelectSingleNode("/TEAMS");
		//Replaces the node found by remp with the data in the nodes passed into the function
		docTemp.ReplaceChild(currentNode,temp);
		//Saves and reloads the sheet
		rawTeamList.Save(Application.persistentDataPath + Path.DirectorySeparatorChar + "PlayerTeams.xml");
		rawTeamList.Load(Application.persistentDataPath + Path.DirectorySeparatorChar + "PlayerTeams.xml");
		teamList = rawTeamList.SelectNodes("/TEAMS/TEAM");
		Debug.Log("Saved");
	}
	
	//getTeam and getTeamData are obsolete, only used in Team but teams are held in TeamContainer now
	public List<string> getTeam(int teamID) // Loads the individual team from the file
	{
		foreach (XmlNode node in teamList)
		{
			if(teamID.ToString() == node.Attributes["id"].InnerText)
			{
				Debug.Log(node.ChildNodes[0].InnerText + " TEAM IS FOUND");
				return getTeamData(node);
			}
		}
		return null;	
	}
	
	public List<string> getTeamData(XmlNode currentTeam)
	{
		List<string> temp = new List<string>();
		
		temp[0] = currentTeam.Attributes["id"].Value;
		temp[1] = currentTeam.ChildNodes[0].InnerText;
		temp[2] = currentTeam.ChildNodes[1].InnerText;
		temp[3] = currentTeam.ChildNodes[2].InnerText;
		temp[4] = currentTeam.ChildNodes[3].InnerText;
		temp[5] = currentTeam.ChildNodes[4].InnerText;
		
		return temp;
	}
	
	public TeamContainer getTeamByID(int teamID)
	{	
		/*//Decrements teamID by 1, this is an adjustment for indexing as the teamID is always 1 or above
		teamID--;
		
		//Gets each team in the XML sheet
		foreach (XmlNode node in teamList)
		{
			//Checks for the one that matches the wanted ID
			if(teamID.ToString() == node.Attributes["id"].InnerText)
			{
				TeamContainer tempTeamContainer = new TeamContainer();
				tempTeamContainer.teamArray = new CharacterContainer[teamSize];
				Debug.Log ("Size: " + teamSize);

				int j = 0;
				//Inner node is each individual character ID in the node
				foreach(XmlNode innerNode in node)
				{
					Debug.Log ("Inner: " + innerNode.InnerText);

					//Searches through each individual character in the character cache
					foreach(CharacterContainer cont in characterCache)
					{
						CharacterContainer temp = new CharacterContainer();
						int nodeid;
						int.TryParse(innerNode.InnerText, out nodeid);
						//If the IDs match they will be placed into the teamcontainer
						if(cont.id == nodeid)
						{
							temp.iconTexture = cont.iconTexture;
							temp.id = cont.id;
							Debug.Log ("ID: " + cont.id);
							temp.powerClass = cont.powerClass;
							Debug.Log ("Power: " + cont.powerClass);
							tempTeamContainer.teamArray[j] = temp;
							break;
						}
					}
					j++;
				}
				return tempTeamContainer;
			}
		}
		return null;
		*/
		
		
		//LOOK AT ALL THE F***ING CODE I CUT OUT BY HAVING THE TEAM CACHE LOCAL TO THIS SCRIPT! HOLY BALLS!
		
		return teamsCache[teamID-1];
	}
}