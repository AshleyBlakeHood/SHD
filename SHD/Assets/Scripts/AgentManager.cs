using UnityEngine;
using System.Collections;

public class AgentManager : MonoBehaviour
{
	GameManager manager;
	SpriteRenderer backgroundSR;

	public GameObject agent;

	int wave = 1;
	int aliveAgents = 0;

	float lastWaveTime = 0;

	// Use this for initialization
	void Start ()
	{
		manager = GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<GameManager>();
		backgroundSR = manager.background.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > lastWaveTime + 30 || aliveAgents <= 0)
			SpawnWave ();
	}

	public void SpawnWave()
	{
		for (int i = 0; i < 4 + wave; i++)
		{
			SpawnAgent ();
		}

		wave++;
		lastWaveTime = Time.time;
	}

	public void SpawnAgent()
	{
		aliveAgents++;

		float x = 0;
		float y = 0;
		
		int result = Random.Range (0, 4);

		//PickSide
		if (result == 0)
		{
			x = backgroundSR.bounds.extents.x * -1;
			y = Random.Range (backgroundSR.bounds.extents.y * -1, backgroundSR.bounds.extents.y);
		}
		else if (result == 1)
		{
			x = backgroundSR.bounds.extents.x;
			y = Random.Range (backgroundSR.bounds.extents.y * -1, backgroundSR.bounds.extents.y);
		}
		else if (result == 2)
		{
			y = backgroundSR.bounds.extents.y * -1;
			x = Random.Range (backgroundSR.bounds.extents.x * -1, backgroundSR.bounds.extents.x);
		}
		else if (result == 3)
		{
			y = backgroundSR.bounds.extents.y;
			x = Random.Range (backgroundSR.bounds.extents.x * -1, backgroundSR.bounds.extents.x);
		}

		GameObject g = (Instantiate (agent, new Vector3(x, y, 0), agent.transform.rotation) as GameObject);
		g.transform.parent = manager.dynamicObjectHolder.transform;
	}

	public void AgentDied()
	{
		aliveAgents--;
		manager.points++;
	}

	void OnGUI()
	{
		//DEBUG
		if (GUI.Button (new Rect(5, 5, 10, 10), "SA"))
		{
			for (int i = 0; i < 5; i++)
			{
				SpawnAgent();
			}
		}
	}
}
