using UnityEngine;

public class _god : MonoBehaviour
{
    void Start()
    {
        {
            GameObject blueTeam = new GameObject("BlueTeam");
            Model.Troop<Model.Marine> model = new Model.Troop<Model.Marine>(64);
            blueTeam.AddComponent<_blueTeam>().model = model;
        }
    }

    void OnGUI()
    {
        GUI.Button(new Rect(10, 50, 80, 30), ("列队"));
        GUI.Button(new Rect(10, 100, 80, 30), ("列阵"));
        GUI.Button(new Rect(10, 150, 80, 30), ("行军"));
        GUI.Button(new Rect(10, 200, 80, 30), (""));
        GUI.Button(new Rect(10, 250, 80, 30), (""));

    }

    void Update()
    {
    }
}