using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _blueSoldier : MonoBehaviour {

    public View.Soldier view;
    public Model.Soldier model;

	void Start () {
        view = new View.Soldier(this.model, this.gameObject);
	}

	
	void Update () {
        this.view.update();
	}
}
