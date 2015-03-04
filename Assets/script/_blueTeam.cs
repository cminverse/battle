using Model;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _blueTeam : MonoBehaviour {

    public Model.Troop<Marine> model;
    public View.Troop<Marine> view;

	void Start () {
        view = new View.Troop<Marine>(this.model, this.gameObject);
	}

	
	void Update () {
	}
}