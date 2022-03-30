using System;

namespace BTs
{
    public class RepeatUntilSuccessDecorator : Decorator
    {
        public RepeatUntilSuccessDecorator() : base() { }
        public RepeatUntilSuccessDecorator(String name) : base(name) { }
        public RepeatUntilSuccessDecorator(ITickable child) : base(child) { }
        public RepeatUntilSuccessDecorator(String name, ITickable child) : base(name, child) { }

        public override Status OnTick()
        {
            if (status == Status.FAILED || status == Status.SUCCEEDED)
                throw new Exception("Repeat decorator ticked in " + status.ToString() + " status");

            Status st = children[currentChild].Tick();

            if (st == Status.FAILED) children[currentChild].Clear();
            else if (st == Status.SUCCEEDED) status = Status.SUCCEEDED;

            return status;
        }
    }
}
