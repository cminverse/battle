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
        targetStart();
	}

	
	void Update () {
        this.view.update();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.name.Equals("Terrain"))
            {
                view.march(new Position(hit.point));
            }
        }
        targetUpdate();
	}


    //LineRenderer
    GameObject target;
    private LineRenderer lineRenderer;
    Vector3 startPoint;
    Vector3 endPoint;
    float magnitude;
    int amount = 0;
    List<GameObject> list = new List<GameObject>();


    // Use this for initialization
    void targetStart()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(Color.white, Color.white);
        lineRenderer.SetWidth(2f, 2f);

        target = new GameObject("destination");
        target.transform.parent = this.transform;
    }

    // Update is called once per frame
    void targetUpdate()
    {
        lineRenderer = GetComponent<LineRenderer>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.name.Equals("Terrain"))
            {
                startPoint = hit.point;
                lineRenderer.SetPosition(0, startPoint);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.name.Equals("Terrain"))
            {
                endPoint = hit.point;
                lineRenderer.SetPosition(1, endPoint);
                this.magnitude = (endPoint - startPoint).magnitude;
                Model.Soldier soldier = new Model.Soldier();
                this.amount = (int)(this.magnitude / (soldier.getRadius() + soldier.getInterspace()) / 2 + 1);
                if (list.Count != this.amount)
                {
                    for (int i = list.Count; i < this.amount; i++)
                    {
                        GameObject childObject = (GameObject)Object.Instantiate(Resources.Load("SoldierRed"));
                        childObject.transform.parent = this.target.transform;
                        list.Add(childObject);
                    }
                }
                for (int i = 0; i < this.amount; i++)
                {
                    list[i].transform.position = startPoint + i * (endPoint - startPoint).normalized * (soldier.getInterspace() + soldier.getRadius()) * 2;
                    list[i].SetActive(true);
                }
                for (int i = this.amount; i < list.Count; i++)
                {
                    list[i].SetActive(false);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.name.Equals("Terrain"))
            {
                endPoint = hit.point;
                lineRenderer.SetPosition(1, endPoint);
            }
        }
    }

    void OnGUI()
    {
        GUILayout.Label("当前鼠标X轴位置：" + Input.mousePosition.x);
        GUILayout.Label("当前鼠标Y轴位置：" + Input.mousePosition.y);
        GUILayout.Label("startPoint: " + this.startPoint);
        GUILayout.Label("endPoint: " + this.endPoint);
        GUILayout.Label("magnitude: " + this.magnitude);
        GUILayout.Label("amount: " + this.amount);
    }
}