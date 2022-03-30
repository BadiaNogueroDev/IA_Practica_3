using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BTs
{
    public abstract class NonLeafNode : Node
    {
        // non-leaf nodes (internal nodes) are the nodes capable of having children
        public List<ITickable> children = new List<ITickable>();
        public int currentChild = 0;


        public NonLeafNode() : base() { }   
        public NonLeafNode(string name) : base(name) {}

        // Experimental code
        public delegate void OnAbortDelegate(); // OnAbortDelegate is a function type corresponding to void parameterless method
        public OnAbortDelegate onAbortHandler = null;

        public override void AddChild(ITickable child)
        {
            // overridable since decorators can only have one child
            children.Add(child);
        }

        public override void AddChildren(params ITickable[] children)
        {
            foreach (ITickable child in children)
                AddChild(child);
        }

        public override void OnAbort()
        {
            // what if an internal node is aborted? 
            // abort its currentChild
            children[currentChild].Abort();

            // Experimental code
            // and then its abortion handler...
            onAbortHandler?.Invoke(); // Invoke but only if it's not null
        }

        public override void Initialize()
        {
            // not sealed since some decorators and subclasses need to do some specific set up...
            if (children.Count == 0)
                throw new Exception("Non-leaf node "+Name+" initialized with no children. Non-leaves are meant to have children");

            status = Status.RUNNING; // or should there be something like Status.READY?
            currentChild = 0;
            // also clear the first child.  REASON: make stuff reusable
            children[currentChild].Clear(); // next children will be cleared by subclasses
        }

        public override void Clear() {
            base.Clear();
            foreach (var child in children) child.Clear();
        }


        public override GameObject theGameObject
        {
            get { return _gameObject; }
            set
            {
                _gameObject = value;
                // propagate the gameobject to all children that require one. Propagation depends on type
                // for Nodes, just set the theGameObject property
                // for BTs invoke MakeReady with the gameobject to propagate as parameter
                foreach (ITickable child in children)
                {
                    if (child is Node) // only to Nodes since BTs already have a gameObject
                        ((Node)child).theGameObject = _gameObject;
                    else
                    {
                        // if it's not a Node then it's a fully-fledged Behaviour tree 
                        ((BehaviourTree)child).Construct(_gameObject);
                    }
                }
            }
        }
    }

    

}
