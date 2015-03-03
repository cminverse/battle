﻿using System.Collections;
using System.Collections.Generic;

namespace Model
{
    public class Troop<T> : Team<T>
        where T : Soldier, new()
    {
        public Troop()
        {
        }

        public Troop(int amount)
            : base(amount)
        {
        }

        public Troop(T[] soldiers)
            : base(soldiers)
        {
        }

        public Troop(List<T> list)
            : base(list)
        {
        }
    }

    public interface asTeam
    {
    }

    public class Team<T> : Crowd<T>//, asTeamMember Troops form Army
        where T : Human, new()
    {
        public Team()
        {
        }

        public Team(int amount)
            : base(amount)
        {
        }

        public Team(T[] array)
            : base(array)
        {
        }

        public Team(List<T> list)
            : base(list)
        {
        }


    }

    public interface asCrowd
    {

    }

    public class Crowd<T> : Group<T>, hasStatusUnit
        where T : Creature, new()
    {
        public Crowd()
        {
        }

        public Crowd(int amount)
            : base(amount)
        {
        }

        public Crowd(T[] array)
            : base(array)
        {
        }

        public Crowd(List<T> list)
            : base(list)
        {
        }

        public enum Status
        {
            idle, battle, attack, suffer, guard, sneak, dead
        }

        public enum Behavior
        {
            born, stand, walk, run, sprint, dash, sit, squat, crawl, jump, dead,
        }

        Model.Status status = new Model.Status();
        public virtual System.Enum getStatus()
        {
            return this.status.getStatus();
        }
        public virtual void setStatus(System.Enum status)
        {
            this.status.setStatus(status);
        }
        public virtual System.Enum getBehavior()
        {
            return this.status.getBehavior();
        }
        public virtual void setBehavior(System.Enum behavior)
        {
            this.status.setBehavior(behavior);
        }
    }

    public interface asGroup
    {

    }

    public class Group<T> : Entity, asGroup, IEnumerable
        where T : Entity, new()
    {
        protected List<T> list;

        public IEnumerator GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public Group()
        {
            this.list = new List<T>();
        }

        public Group(T[] array)
            : this()
        {
            foreach (T item in array)
            {
                this.list.Add(item);
            }
        }

        public Group(int amount)
            : this()
        {
            for (int i = 0; i < amount; i++)
            {
                this.list.Add(new T());
            }
        }

        public Group(List<T> list)
        {
            this.list = list;
        }
    }

    //------------------------------------

    public class Marine : Soldier
    {

    }

    public interface asSoldier
    {

    }

    public class Soldier : Human, asSoldier
    {
        public new virtual void update()
        {
            base.update();
        }
    }

    public interface asHuman
    {

    }

    public class Human : Creature, asHuman
    {
        public new virtual void update()
        {
            base.update();
        }
    }

    public class Life
    {
        private float life;

        public Life()
            : this(1)
        {
        }

        public Life(float life)
        {
            this.life = life;
        }

        public static implicit operator float(Life life)
        {
            return life.life;
        }
    }

    public class Health
    {
        private float health;

        public Health()
            : this(1)
        {
        }

        public Health(float health)
        {
            this.health = health;
        }

        public static implicit operator float(Health health)
        {
            return health.health;
        }
    }

    public class Age
    {
        private float age;

        public Age()
            : this(0)
        {
        }

        public Age(float age)
        {
            this.age = age;
        }

        public static implicit operator float(Age age)
        {
            return age.age;
        }
    }

    public interface asCreature
    {
        Life getLife();
        void setLife(Life life);
        Health getHealth();
        void setHealth(Health health);
        Age getAge();
        void setAge(Age age);
    }

    public interface asTeamMember
    {
        Entity getFollowMember();
        void setFollowMember(Entity entity);
        Entity getImitateMember();
        void setImitateMember(Entity entity);
    }

    public interface hasStatusUnit
    {
        System.Enum getStatus();
        void setStatus(System.Enum status);
        System.Enum getBehavior();
        void setBehavior(System.Enum behavior);
    }

    public class Status
    {
        protected enum duckType
        {
            defaultZero
        }

        public Status()
            : this(duckType.defaultZero, duckType.defaultZero)
        {
        }

        public Status(System.Enum status, System.Enum behavior)
        {
            this.status = status;
            this.behavior = behavior;
        }

        private System.Enum status;
        public System.Enum getStatus()
        {
            return this.status;
        }
        public void setStatus(System.Enum status)
        {
            this.status = status;
        }

        private System.Enum behavior;
        public System.Enum getBehavior()
        {
            return this.behavior;
        }
        public void setBehavior(System.Enum behavior)
        {
            this.behavior = behavior;
        }
    }

    public class Creature : Entity, asCreature, asTeamMember, hasStatusUnit
    {
        public enum Status
        {
            idle, battle, attack, suffer, guard, sneak, dead
        }

        public enum Behavior
        {
            born, stand, walk, run, sprint, dash, sit, squat, crawl, jump, dead,
        }

        Model.Status status = new Model.Status();
        public virtual System.Enum getStatus()
        {
            return this.status.getStatus();
        }
        public virtual void setStatus(System.Enum status)
        {
            this.status.setStatus(status);
        }
        public virtual System.Enum getBehavior()
        {
            return this.status.getBehavior();
        }
        public virtual void setBehavior(System.Enum behavior)
        {
            this.status.setBehavior(behavior);
        }

        protected Life life = new Life();
        public virtual Life getLife()
        {
            return this.life;
        }
        public virtual void setLife(Life life)
        {
            this.life = life;
        }

        protected Health health = new Health();
        public virtual Health getHealth()
        {
            return this.health;
        }
        public virtual void setHealth(Health health)
        {
            this.health = health;
        }

        protected Age age = new Age();
        public virtual Age getAge()
        {
            return this.age;
        }
        public virtual void setAge(Age age)
        {
            this.age = age;
        }

        protected Entity followMember = null;
        public virtual Entity getFollowMember()
        {
            return this.followMember;
        }
        public virtual void setFollowMember(Entity followMember)
        {
            this.followMember = followMember;
        }

        protected Entity imitateMember = null;
        public virtual Entity getImitateMember()
        {
            return this.imitateMember;
        }
        public virtual void setImitateMember(Entity imitateMember)
        {
            this.imitateMember = imitateMember;
        }

        public new virtual void update()
        {
            base.update();
            if (followMember != null)
            {
                this.setFaceTo(followMember.getPosition());
                this.setTowards(followMember.getPosition());
            }
            if (imitateMember != null)
            {
                this.setFaceTo(imitateMember.getFaceTo());
                this.setTowards(imitateMember.getTowards());
            }
        }
    }

    public class Position
    {
        UnityEngine.Vector3 vector3;

        public Position()
            : this(0, 0, 0)
        {
        }

        public Position(float x, float y, float z)
            : this(new UnityEngine.Vector3(x, y, z))
        {
        }

        public Position(UnityEngine.Vector3 vector3)
        {
            this.vector3 = vector3;
        }

        public static implicit operator UnityEngine.Vector3(Position position)
        {
            return position.vector3;
        }
    }

    public interface asEntity
    {
        Position getPosition();
        void setPosition(Position position);
        Position getTowards();
        void setTowards(Position towards);
        float getRadius();
        void setRadius(float radius);
        float getSpeed();
        void setSpeed(float speed);
        float getAcceleration();
        void setAcceleration(float acceleration);

        void update();
    }

    public class Entity : asEntity
    {
        protected Position position = new Position();
        public virtual Position getPosition()
        {
            return this.position;
        }
        public virtual void setPosition(Position position)
        {
            this.position = position;
        }

        protected Position faceTo = new Position();
        public virtual Position getFaceTo()
        {
            return this.faceTo;
        }
        public virtual void setFaceTo(Position faceTo)
        {
            this.faceTo = faceTo;
        }

        protected float radius = 0f;
        public virtual float getRadius()
        {
            return this.radius;
        }
        public virtual void setRadius(float radius)
        {
            this.radius = radius;
        }

        protected float speed = 0f;
        public virtual float getSpeed()
        {
            return this.speed;
        }
        public virtual void setSpeed(float speed)
        {
            this.speed = speed;
        }

        protected float acceleration = 0f;
        public virtual float getAcceleration()
        {
            return this.acceleration;
        }
        public virtual void setAcceleration(float acceleration)
        {
            this.acceleration = acceleration;
        }

        protected Position towards = new Position();
        public virtual Position getTowards()
        {
            return this.towards;
        }
        public virtual void setTowards(Position towards)
        {
            this.towards = towards;
        }

        public virtual void update()
        {
        }
    }

    public class World
    {
        private readonly Entity[] entity;
        public World(params Entity[] entity)
        {
            this.entity = entity;
        }
        public void update()
        {
            while (true)
            {
                foreach (Entity entity in this.entity)
                {
                    entity.update();
                }
            }
        }
    }

    public class God
    {
        public void epiphany()
        {
            System.Threading.Thread time = new System.Threading.Thread(genesis);
            time.Start();
        }

        public void genesis()
        {
            World world = new World(all());
            world.update();
        }

        public Entity[] all()
        {
            return new Entity[] {
                new Troop<Marine>(1),
                new Team<Soldier>(),
            };
        }
    }
}