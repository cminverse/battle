using System.Collections;
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
            : this()
        {
            for (int i = 0; i < amount; i++)
            {
                this.list.Add(new T());
            }
        }

        public Troop(T[] array)
            : this()
        {
            foreach (T item in array)
            {
                this.list.Add(item);
            }
        }

        public Troop(List<T> list)
        {
            this.list = list;
        }
    }

    public interface asTeam
    {
        void square(Rect rect);
        void lineUp();
        void march(Position destination);
    }

    public class Team<T> : Crowd<T>, asTeam//, asTeamMember Troops form Army
        where T : Human, new()
    {
        public Team()
        {
        }

        public Team(int amount)
            : this()
        {
            for (int i = 0; i < amount; i++)
            {
                this.list.Add(new T());
            }
        }

        public Team(T[] array)
            : this()
        {
            foreach (T item in array)
            {
                this.list.Add(item);
            }
        }

        public Team(List<T> list)
        {
            this.list = list;
        }

        //zhengxing
        //Position should be as powerful as Vector3
        public virtual void square(Rect rect)
        {
            Position frontFirst = ((Position[])rect)[0];
            Position frontLast = ((Position[])rect)[1];
            Position behindFirst = ((Position[])rect)[2];
            Position lastOne = ((Position[])rect)[3];

            Position horizonUnit = new Position(((((UnityEngine.Vector3)frontLast - (UnityEngine.Vector3)frontFirst)).normalized));
            Position verticalUnit = new Position(((((UnityEngine.Vector3)behindFirst - (UnityEngine.Vector3)frontFirst)).normalized));

            int i = 0, j = 0;
            foreach (T member in list)
            {
                float scale = (member.getRadius() + member.getInterspace()) * 2;
                Position horizonStep = new Position((UnityEngine.Vector3)horizonUnit * scale);
                Position verticalStep = new Position((UnityEngine.Vector3)verticalUnit * scale);

                Position horizon = new Position((UnityEngine.Vector3)horizonStep * i++);
                Position vertical = new Position((UnityEngine.Vector3)verticalStep * j);

                bool horizonOutBoundFlag = UnityEngine.Vector3.Dot((UnityEngine.Vector3)frontLast - (UnityEngine.Vector3)frontFirst - (UnityEngine.Vector3)horizon, horizon) < 0;
                
                if (!horizonOutBoundFlag)
                {
                    member.setDestination(new Position((UnityEngine.Vector3)frontFirst + (UnityEngine.Vector3)horizon + (UnityEngine.Vector3)vertical));
                }
                else
                {
                    i = 0;
                    j++;
                    member.setDestination(new Position((UnityEngine.Vector3)frontFirst + (UnityEngine.Vector3)horizonStep * i + (UnityEngine.Vector3)verticalStep * j));
                }
            }
        }

        public virtual void lineUp()
        {
            T previousMember = null;
            foreach (T member in list)
            {
                if (previousMember == null)
                {
                    previousMember = member;
                    continue;
                }
                member.setTowards(previousMember.getPosition());
                member.setFrontMember(previousMember);
                previousMember = member;
            }
        }

        public virtual void march(Position destination)
        {
            foreach (T member in list)
            {
                if (member.getFrontMember() == null)
                {
                    member.setDestination(destination);
                }
            }
        }

        public override void gather()
        {
            if (list.Count != 0)
        	{
                setSurroundRelation();
	        }
        }

        void setSurroundRelation()
        {
            T[] square = new T[(int)System.Math.Sqrt(list.Count) + 1];
            int i = 0;
            foreach (T member in list)
            {
                if (i != 0)
                {
                    member.setLeftMember(square[i-1]);
                    square[i-1].setRightMember(member);
                }
                if (square[i] != null)
                {
                    member.setFrontMember(square[i]);
                    square[i].setBehindMember(member);
                }
                square[i] = member;
                if (++i == square.GetLength(0))
                    i = 0;
            }
        }

        public override void gather(Rect rect)
        {
            ;
        }
    }

    public interface asCrowd
    {
        void gather();
        void gather(Rect rect);
    }

    public class Crowd<T> : Group<T>, asCrowd, asStatusUnit, hasAgent
        where T : Creature, new()
    {
        public Crowd()
        {
        }

        public Crowd(int amount)
            : this()
        {
            for (int i = 0; i < amount; i++)
            {
                this.list.Add(new T());
            }
        }

        public Crowd(T[] array)
            : this()
        {
            foreach (T item in array)
            {
                this.list.Add(item);
            }
        }

        public Crowd(List<T> list)
        {
            this.list = list;
        }

        Position destination = new Position();
        public virtual Position getDestination()
        {
            return this.destination;
        }
        public virtual void setDestination(Position destination)
        {
            this.destination = destination;
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

        public virtual void gather()
        {
            this.gather(new Rect());
        }

        public virtual void gather(Rect rect)
        {
            ;
        }
    }

    public interface asGroup
    {
    }

    public interface asRect
    {
        void setVertex(Rect vertex);
        Rect getVertex();
        Position getCenter();
    }

    public class Group<T> : Entity, asGroup, IEnumerable, asRect
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

        public Group(int amount)
            : this()
        {
            for (int i = 0; i < amount; i++)
            {
                this.list.Add(new T());
            }
        }

        public Group(T[] array)
            : this()
        {
            foreach (T item in array)
            {
                this.list.Add(item);
            }
        }

        public Group(List<T> list)
        {
            this.list = list;
        }

        protected Rect vertex = new Rect();
        public virtual void setVertex(Rect vertex)
        {
            this.vertex = vertex;
        }
        public virtual Rect getVertex()
        {
            return this.vertex;
        }
        public virtual Position getCenter()
        {
            float x=0, y=0, z=0;
            foreach (Position point in vertex)
            {
                x += ((UnityEngine.Vector3)point).x;
                y += ((UnityEngine.Vector3)point).y;
                z += ((UnityEngine.Vector3)point).z;
            }
            return new Position(x, y, z);
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
        public Soldier()
        {
            this.setRadius(0.5f);
            this.setSpeed(10f);
            this.setAcceleration(100f);
            this.setInterspace(1.0f * this.getRadius());
        }

        public new virtual void update()
        {
            base.update();
        }
    }

    public interface asHuman
    {

    }

    public class Human : Creature, asHuman, asTeamMember
    {
        protected Human frontMember = null;
        public virtual Human getFrontMember()
        {
            return this.frontMember;
        }
        public virtual void setFrontMember(Human frontMember)
        {
            this.frontMember = frontMember;
        }

        protected Human leftMember = null;
        public virtual Human getLeftMember()
        {
            return this.leftMember;
        }
        public virtual void setLeftMember(Human leftMember)
        {
            this.leftMember = leftMember;
        }

        protected Human rightMember = null;
        public virtual Human getRightMember()
        {
            return this.rightMember;
        }
        public virtual void setRightMember(Human rightMember)
        {
            this.rightMember = rightMember;
        }

        protected Human behindMember = null;
        public virtual Human getBehindMember()
        {
            return this.behindMember;
        }
        public virtual void setBehindMember(Human behindMember)
        {
            this.behindMember = behindMember;
        }

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
        Human getFrontMember();
        void setFrontMember(Human front);
        Human getLeftMember();
        void setLeftMember(Human left);
        Human getRightMember();
        void setRightMember(Human right);
        Human getBehindMember();
        void setBehindMember(Human behind);
    }

    public interface asStatusUnit
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

    public interface hasAgent
    {
        Position getDestination();
        void setDestination(Position destination);
    }

    public class Creature : Entity, asCreature, asStatusUnit, hasAgent
    {
        public enum Status
        {
            idle, march, battle, attack, suffer, guard, sneak, dead
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

        protected Position destination = null;
        public virtual void setDestination(Position destination)
        {
            this.destination = destination;
        }
        public virtual Position getDestination()
        {
            return this.destination;
        }

        public new virtual void update()
        {
            base.update();
        }
    }

    public class Rect : IEnumerable
    {
        Position[] vertex;

        public IEnumerator GetEnumerator()
        {
            return this.vertex.GetEnumerator();
        }

        public Rect()
            : this(new Position[4] { new Position(), new Position(), new Position(), new Position() })
        {
        }

        public Rect(Position[] vertex)
        {
            this.vertex = vertex;
        }

        public Rect(Position vertex1, Position vertex2, Position center)
        {
            UnityEngine.Vector3 vector1 = ((UnityEngine.Vector3)vertex1);
            UnityEngine.Vector3 vector2 = ((UnityEngine.Vector3)vertex2);
            Position vertex3 = new Position(((UnityEngine.Vector3)center*2 - vector1));
            Position vertex4 = new Position(((UnityEngine.Vector3)center*2 - vector2));
            this.vertex = new Position[4] { vertex1, vertex2, vertex3, vertex4};
        }

        public static implicit operator Position[](Rect rect)
        {
            return rect.vertex;
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
        float getInterspace();
        void setInterspace(float interspace);

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

        protected Position towards = new Position();
        public virtual Position getTowards()
        {
            return this.towards;
        }
        public virtual void setTowards(Position towards)
        {
            this.towards = towards;
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

        protected float interspace = 0f;
        public virtual float getInterspace()
        {
            return this.interspace;
        }
        public virtual void setInterspace(float interspace)
        {
            this.interspace = interspace;
        }

        public virtual void update()
        {
        }
    }

    public class World
    {
        private readonly Entity[] entities;
        public World(params Entity[] entity)
        {
            this.entities = entity;
        }
        public void update()
        {
            while (true)
            {
                foreach (Entity entity in this.entities)
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