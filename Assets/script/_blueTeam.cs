using Model;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//cleanLevel = 0;
public class _blueTeam : MonoBehaviour {

    private View.Troop<Marine> troop;

	void Start () {
        troop = new View.Troop<Marine>(this.gameObject);
	}

	
	void Update () {
	}
}