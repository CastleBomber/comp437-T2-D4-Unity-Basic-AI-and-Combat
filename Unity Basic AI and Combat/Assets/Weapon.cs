﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    public int poolSize;
	public float weaponVelocity;

	private void Awake()
	{
		if (ammoPool == null)
		{
			ammoPool = new List<GameObject>();
		}

		for (int i = 0; i < poolSize; i++)
		{
			GameObject ammoObject = Instantiate(ammoPrefab);
			ammoObject.SetActive(false);
			ammoPool.Add(ammoObject);
		}
	}


	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			FireAmmo();
		}
	}

	public GameObject SpawnAmmo(Vector3 location)
	{
		foreach (GameObject ammo in ammoPool)
		{
			if (ammo.activeSelf == false)
			{
				ammo.SetActive(true);
				ammo.transform.position = location;
				return ammo;
			}
		}

		return null;
	}

	void FireAmmo()
	{
		Vector3 moustPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		GameObject ammo = SpawnAmmo(transform.position);

		if (ammo != null)
		{
			Arc arcScript = ammo.GetComponent<Arc>();
			float travelDuration = 1.0f / weaponVelocity;
			StartCoroutine(arcScript.TravelArc(moustPosition, travelDuration));
		}
	}

	private void OnDestroy()
	{
		ammoPool = null;
	}
}