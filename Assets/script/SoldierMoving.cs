using UnityEngine;
using System.Collections;
//cleanLevel = 0;
public class SoldierMoving : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
    }


    void Update()
    {
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
                agent.SetDestination(point);
            }
        }


        if (agent.remainingDistance == 0)
        {
            animation.Play("Idle");
        }
        else
        {
            animation.Play("Run");
        }

    }
}
