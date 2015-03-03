using Model;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace View
{
    public class Troop<T>
        where T : Soldier, new()
    {
        public int ready_ = 0;
        public GameObject gameObject;
        protected Model.Troop<T> troop;

        public Troop(GameObject gameObject)
        {
            this.troop = new Model.Troop<T>();
        }
    }

    public class Marine
    {
        public int ready_ = 0;
        public GameObject gameObject;
        protected Model.Marine marine;
        
        public NavMeshAgent navMeshAgent;

        public Marine(GameObject gameObject)
        {
            this.gameObject = gameObject;
            marine = new Model.Marine();
        }

        void start()
        {
            System.Threading.Thread thread = new System.Threading.Thread(() => {
                this.ready_ += 1;
                this.navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
                this.ready_ -= 1;
            });
            thread.Start();
        }

        void update()
        {
            if (this.ready_ != 0) return;

        }
    }

    public class Test
    {
        public Test()
        {

        }
    }
}