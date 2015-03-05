using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace View
{
    public class Troop<T>
        where T : Model.Soldier, new()
    {
        public Model.Troop<T> model;
        public GameObject gameObject;

        public Troop(Model.Troop<T> model, GameObject gameObject)
        {
            this.model = model;
            this.gameObject = gameObject;
            foreach (T childModel in model)
            {
                GameObject childObject = (GameObject)Object.Instantiate(Resources.Load("SoldierBlue"));
                childObject.transform.parent = gameObject.transform;
                childObject.AddComponent<_blueSoldier>().model = childModel;
            }
        }
    }

    public class Soldier
    {
        public Model.Soldier model;
        public GameObject gameObject;
        
        public NavMeshAgent navMeshAgent;

        public Soldier(Model.Soldier model, GameObject gameObject)
        {
            this.model = model;
            this.gameObject = gameObject;

            navMeshAgent = this.gameObject.AddComponent<NavMeshAgent>();
            navMeshAgent.radius = model.getRadius();
            navMeshAgent.speed = model.getSpeed();
            navMeshAgent.acceleration = model.getAcceleration();
        }

        public void update()
        {
            ;
        }
    }
}