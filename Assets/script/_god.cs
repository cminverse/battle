using Model;
using UnityEngine;

public class _god : MonoBehaviour
{
    void Start()
    {
        GameObject blueTeam = new GameObject("BlueTeam");
        Model.Troop<Marine> model = new Troop<Marine>(7);
        blueTeam.AddComponent<_blueTeam>().model = model;
    }

    void Update()
    {
    }
}