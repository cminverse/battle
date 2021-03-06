﻿using UnityEngine;
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

        public void square( )
        {
            
        }

        public void march(Model.Rect rect)
        {
            //model.lineUp();
            //model.gather();
            //model.march(destination);
            model.square(rect);
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
            navMeshAgent.stoppingDistance = 0;// 2.0f * (model.getRadius() + model.getInterspace());
        }

        public void update()
        {
            this.model.setPosition(new Model.Position(this.gameObject.transform.position));
            if (this.model.getDestination() != null)
                this.navMeshAgent.SetDestination(this.model.getDestination());
            else if (this.model.getFrontMember() != null)
                this.navMeshAgent.SetDestination(this.model.getFrontMember().getPosition());

            if (navMeshAgent.remainingDistance < 2.0f * (model.getRadius() + model.getInterspace()))
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