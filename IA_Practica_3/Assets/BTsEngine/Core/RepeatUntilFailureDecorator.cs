using System;

namespace BTs
{
    public class RepeatUntilFailureDecorator : Decorator
    {
        public RepeatUntilFailureDecorator() : base() { }
        public RepeatUntilFailureDecorator(String name) : base(name) { }
        public RepeatUntilFailureDecorator(ITickable child) : base(child) { }
        public RepeatUntilFailureDecorator(String name, ITickable child) : base(name, child) { }

        public override Status OnTick()
        {
            if (status == Status.FAILED || status == Status.SUCCEEDED)
                throw new Exception("Repeat decorator ticked in " + status.ToString() + " status");

            Status st = children[currentChild].Tick();

            if (st == Status.SUCCEEDED) children[currentChild].Clear(); // own status is not changed and continues to be RUNNING
            else if (st == Status.FAILED) status = Status.SUCCEEDED;

            return status;
        }
    }
}