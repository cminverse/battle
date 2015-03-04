using Model;
using UnityEngine;

public class _god : MonoBehaviour
{
    void Start()
    {
        new View.Troop<Marine>(new Troop<Marine>(7), new GameObject("BlueTeam"));
    }

    void Update()
    {
    }
}