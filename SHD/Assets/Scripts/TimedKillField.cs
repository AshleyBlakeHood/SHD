using UnityEngine;
using System.Collections;

public class TimedKillField : MonoBehaviour
{
	public float TTL = 1f;
	public string replacementTag = "Untagged";

	private float startTime;

	// Use this for initialization
	void Start ()
	{
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > startTime + TTL)
			gameObject.tag = replacementTag;
	}
}
