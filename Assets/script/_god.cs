﻿using Model;
using UnityEngine;

public class _god : MonoBehaviour
{
    void Start()
    {
        {
            GameObject blueTeam = new GameObject("BlueTeam");
            Model.Troop<Marine> model = new Troop<Marine>(6);
            blueTeam.AddComponent<_blueTeam>().model = model;
        }
        {
            GameObject blueTeam = new GameObject("BlueTeam");
            Model.Troop<Marine> model = new Troop<Marine>(6);
            blueTeam.AddComponent<_blueTeam>().model = model;
        }
    }

    void OnGUI()
    {
        GUI.Button(new Rect(10, 50, 80, 30), ("单纵"));
        GUI.Button(new Rect(10, 100, 80, 30), ("双纵"));
        GUI.Button(new Rect(10, 150, 80, 30), ("阵列"));
        GUI.Button(new Rect(10, 200, 80, 30), ("方阵"));
        GUI.Button(new Rect(10, 250, 80, 30), ("方阵"));

    }

    void Update()
    {
    }
}