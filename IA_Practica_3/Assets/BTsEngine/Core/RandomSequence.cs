using System;

namespace BTs
{
    class RandomSequence : Sequence
    {

        public RandomSequence() : base() { }
        public RandomSequence(string name) : base(name) { }

        public RandomSequence(params ITickable[] tasks) : base()
        {
            foreach (ITickable t in tasks)
            {
                this.AddChild(t);
            }
        }
        public RandomSequence(string name, params ITickable[] tasks) : base(name)
        {
            foreach (ITickable t in tasks)
            {
                this.AddChild(t);
            }
        }

        private Random alea = new Random();

        public override void Initialize()
        {
            base.Initialize();
            // after the "normal" initialization, just shuffle the children

            for (int i=1; i<=children.Count; i++)
            {
                int a = alea.Next(0, children.Count);
                int b = alea.Next(0, children.Count);
                ITickable inter = children[a];
                children[a] = children[b];
                children[b] = inter;
            }
        }
    }
}
