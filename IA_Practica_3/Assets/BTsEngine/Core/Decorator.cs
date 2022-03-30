using System;

namespace BTs
{

    public abstract class Decorator : NonLeafNode
    {
        public Decorator() : base() { }
        public Decorator(String name) : base(name) { }
        public Decorator(ITickable child) : base()
        {
            AddChild(child);
        }
        public Decorator(String name, ITickable child) : base(name)
        {
            AddChild(child);
        }

        public override void AddChild(ITickable child)
        {
            if (this.children.Count == 1) throw new Exception("Adding a second child to a decorator is not allowed");
            base.AddChild(child);
        }


        public override void AddChildren(params ITickable[] children)
        {
            // or maybe allow it provided only one child is given...?
            throw new Exception("AddChildren not invokable on decorators since decorators only have one child");
        }

        public override void Initialize()
        {
            base.Initialize();
            // EXPERIMENTAL CODE. REASON: make stuff reusable
            children[currentChild].Clear();
        }
    }
}
