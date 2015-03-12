using Model;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _teamMovingDestination : MonoBehaviour
{
    public View.Troop<Marine> view;

    GameObject destination;
    Vector3 startPoint;
    Vector3 endPoint;
    float unitSize;
    int formationWidth;
    int formationDepth;

    List<GameObject> list = new List<GameObject>();
    RaycastHit hit;

    void Start()
    {
        destination = new GameObject("destination");
        destination.transform.parent = this.transform;

        Model.Soldier soldier = new Model.Soldier();
        unitSize = 2 * (soldier.getRadius() + soldier.getInterspace());
    }

    void mouseDown()
    {
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.collider.name.Equals("Terrain"))
        {
            startPoint = hit.point;
        }
    }

    void mousePress()
    {
        if (Input.GetMouseButton(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.collider.name.Equals("Terrain"))
        {
            endPoint = hit.point;
            Vector3 dragVector = endPoint - startPoint;
            
            if (list.Count != this.view.model.getAmount())
            {
                for (int i = list.Count; i < this.view.model.getAmount(); i++)
                {
                    GameObject childObject = (GameObject)Object.Instantiate(Resources.Load("SoldierRed"));
                    childObject.transform.parent = this.destination.transform;
                    list.Add(childObject);
                }
            }

            int temp = 1 + (int)(dragVector.magnitude / unitSize) < 3 ? 3 : 1 + (int)(dragVector.magnitude / unitSize);
            formationWidth = temp > list.Count ? list.Count : temp;
            formationDepth = (list.Count - 1) / formationWidth + 1;

            Vector3 cross = Vector3.Cross(endPoint, startPoint);
            cross.y = -System.Math.Abs(cross.y);
            Vector3 offset = Vector3.Cross(cross, dragVector).normalized * unitSize;

            for (int i = 0; i < list.Count; i++)
            {
                list[i].transform.position = startPoint + offset * (formationDepth / 2 - (i / formationWidth)) + (i % formationWidth) * dragVector.normalized * unitSize;
            }
        }
    }

    void mouseUp()
    {
        if (Input.GetMouseButtonUp(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.collider.name.Equals("Terrain"))
        {
            endPoint = hit.point;
            Debug.Log(formationWidth);
            Debug.Log(formationDepth);
            view.march(new Model.Rect(new Model.Position[]{
                new Model.Position(list[0].transform.position),
                new Model.Position(list[formationWidth - 1].transform.position),
                new Model.Position(list[(formationDepth - 1) * formationWidth].transform.position),
                new Model.Position(list[list.Count - 1].transform.position)
            }));
        }
    }
    
    void Update()
    {
        mouseDown();
        mousePress();
        mouseUp();
    }


    void OnGUI()
    {
        GUILayout.Label("当前鼠标X轴位置：" + Input.mousePosition.x);
        GUILayout.Label("当前鼠标Y轴位置：" + Input.mousePosition.y);
        GUILayout.Label("startPoint: " + this.startPoint);
        GUILayout.Label("endPoint: " + this.endPoint);
    }
}