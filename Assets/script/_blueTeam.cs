using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//cleanLevel = 1;
//zhengxing, should design the front MVC/MVP
public class _blueTeam : MonoBehaviour
{
    public Troop<Soldier> data = new Troop<Soldier>();
    GameObject presentation;
    private NavMeshAgent agent;
    void Start()
    {
        presentation = (GameObject)Instantiate(Resources.Load("SoldierBlue"));
        presentation.transform.parent = transform;
        presentation.transform.position = new Vector3(10, 0, 10);
        agent = presentation.gameObject.AddComponent<NavMeshAgent>();
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
                data.goSomewhere(point);
                foreach (Soldier item in data.members)
                {
                    presentation.transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
                    agent.SetDestination(item.target);
                }
            }
        }


        if (agent.remainingDistance == 0)
        {
            presentation.animation.Play("Idle");
        }
        else
        {
            presentation.animation.Play("Run");
        }
    }
}
//zhengxing, should extends IEnumerator
public class Troop<T>
    where T : Soldier, new()
{
    public List<T> members = new List<T>();

    public Troop() : this(1) { }

    public Troop(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            members.Add(new T());
        }
    }
    //zhengxing, should detach with the Vector3
    public void goSomewhere(Vector3 position)
    {
        foreach (T item in members)
        {
            item.target = position;
        }
    }
}

public class Entity
{
    protected float radius = 0;
    protected float speed = 0;
    protected float acceleration = 0;
    protected float angularSpeed = 0;
}

public class Soldier : Entity, asTeamMember, canBattle
{
    protected float force = 0.1f;
    protected float health = 1;
    //zhengxing, should detach with Vector3
    public Vector3 target;

    public virtual Attack createAttack()
    {
        return new Attack(force);
    }

    public virtual void suffer(Attack attack)
    {
        this.health -= attack.power;
    }
}

public class Marine : Soldier
{
    public override void suffer(Attack attack)
    {
        ;
    }
}

public class Attack
{
    public float power;

    public Attack(float force)
    {
        this.power = force;
    }
}

public interface asTeamMember
{

}

public interface canBattle
{
    Attack createAttack();
    void suffer(Attack attack);
}