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