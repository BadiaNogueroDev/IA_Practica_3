

namespace BTs
{
    public abstract class Action : Leaf
    {
        public Action() : this("unnamed action") { }
        public Action(string name) : base(name) { }
    } 
}
