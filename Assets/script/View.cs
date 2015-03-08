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

        public void march(Model.Position destination)
        {
            model.lineUp();
            model.march(destination);
        }

        public void update()
        {
            this.model.setPosition(new Model.Position(this.gameObject.transform.position));
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
            navMeshAgent.stoppingDistance = 2.5f * model.getRadius();
        }

        public void update()
        {
            this.model.setPosition(new Model.Position(this.gameObject.transform.position));
            if (this.model.getDestination() != null)
                this.navMeshAgent.SetDestination(this.model.getDestination());
            else if (this.model.getFollowMember() != null)
                this.navMeshAgent.SetDestination(this.model.getFollowMember().getPosition());

            if (navMeshAgent.remainingDistance < 2.5f * model.getRadius())
            {
                this.gameObject.animation.Play("Idle");
            }
            else
            {
                this.gameObject.animation.Play("Run");
            }
        }
    }
}