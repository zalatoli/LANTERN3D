﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int currentGold;
    public Text goldText;

    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        goldText.text = "Gold: " + currentGold;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
