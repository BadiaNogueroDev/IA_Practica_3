using System;
using UnityEngine;

namespace BTs
{
    // superclass of all nodes in a Behaviour tree (not intended to be Monobehaviours), including non-leaves and leaves
    // Nodes are tickable and much more...
    public abstract class Node : ITickable 
    {
        public abstract Status OnTick();
        public abstract void Initialize();
        public abstract void OnAbort();
        public virtual void AddChild(ITickable child) { throw new NotImplementedException(); }
        public virtual void AddChildren(params ITickable[] children) { throw new NotImplementedException(); }
        
        // Adds a condition/ITickable pair (only for dynamic selectors)
        public virtual void AddChild(Condition condition, ITickable child) { throw new NotImplementedException(); }

        private bool initialized = false;
        protected Status status = Status.LIMBO;

        private string _name;
        public string Name
        {
            get { return _name; }
            // no setter for name
        }

        private static int unnamedInstanceCounter = 0;

        public Node() : this("unnamed_" + unnamedInstanceCounter) { unnamedInstanceCounter++; }
        public Node(string name) { _name = name; }

        public Status GetStatus() { return this.status; }

        public Status Tick()
        {
            if (!initialized)
            {
                Initialize();
                initialized = true;
            }
            status = OnTick();
            return status;
        }

        public virtual void Clear()
        {
            status = Status.LIMBO;
            initialized = false;
        }

        public void Abort()
        {
            if (initialized)
            {
                // if is necessary because non initialized tasks may have non-initialized fields 
                // that OnAbort may try to access...
                OnAbort();
                Clear();
            }
        }

        public bool IsTerminated()
        {
            return status == Status.SUCCEEDED || status == Status.FAILED;
        }



        public bool IsInitialized()
        {
            return initialized;
        }


        // Unity related section (all other code should be Unity-independent)
        protected GameObject _gameObject;
        public virtual GameObject theGameObject
        {
            get { return _gameObject; }
            set
            {
                _gameObject = value;
            }
        }

        public T GetComponent<T>()
        {
            return theGameObject.GetComponent<T>();
        }

        public T AddComponent<T>() where T:Component
        {
            return theGameObject.AddComponent<T>();
        }

    }
}
