using Model;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _blueTeam : MonoBehaviour {

    public Model.Troop<Marine> model;
    public View.Troop<Marine> view;

    void Start()
    {
        view = new View.Troop<Marine>(this.model, this.gameObject);
	}

	
	void Update () {
        this.view.update();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.name.Equals("Terrain"))
                {
                    return;
                }
                view.march(new Position(hit.point));
            }
        }
	}
}