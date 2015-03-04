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
                Vector3 point = hit.point;
                transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
                view.navMeshAgent.SetDestination(point);
            }
        }


        if (view.navMeshAgent.remainingDistance == 0)
        {
            animation.Play("Idle");
        }
        else
        {
            animation.Play("Run");
        }
	}
}
