using System;
using UnityEngine;

namespace BTs
{
    public interface ITickable
    {
        Status GetStatus();
        Status Tick();
        void Abort();
        void Clear();
        string Name { get; }
    }

}
