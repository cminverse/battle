using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _blueSoldier : MonoBehaviour {

    private View.Marine marine;

	void Start () {
        marine = new View.Marine(this.gameObject);
	}

	
	void Update () {
	}
}
