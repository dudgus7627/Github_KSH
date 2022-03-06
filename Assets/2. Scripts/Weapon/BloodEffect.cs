using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
	public GameObject[] Prefabs;
	public GameObject occurrence_transform;

	private int currentNomber;
	private GameObject currentInstance;

	void Start()
	{
		currentInstance = Instantiate(Prefabs[currentNomber], transform.position, new Quaternion()) as GameObject;
		var reactivator = currentInstance.AddComponent<DemoReactivator>();
		reactivator.TimeDelayToReactivate = 2f;
	}

	// Update is called once per frame
	void ChangeCurrent(int delta)
	{
		currentNomber += delta;
		if (currentNomber > Prefabs.Length - 1)
			currentNomber = 0;
		else if (currentNomber < 0)
			currentNomber = Prefabs.Length - 1;
		if (currentInstance != null) Destroy(currentInstance);
		currentInstance = Instantiate(Prefabs[currentNomber], transform.position, new Quaternion()) as GameObject;
		var reactivator = currentInstance.AddComponent<DemoReactivator>();
		reactivator.TimeDelayToReactivate = 1.5f;
	}
}
