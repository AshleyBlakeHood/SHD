using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour
{
	public bool activePower = false;

	public float costPerUse = 10;
	public float maxUse = 30;
	public float rechargeRate = 5;

	public float currentUse = 30;
	
	public void Recharge()
	{
		if (currentUse < maxUse)
		{
			currentUse += rechargeRate * Time.deltaTime;

			if (currentUse > maxUse)
				currentUse = maxUse;
		}
	}

	public bool UsePower()
	{
		if (currentUse > costPerUse)
		{
			currentUse -= costPerUse;

			return true;
		}
		else
		{
			return false;
		}
	}
}
