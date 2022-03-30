using UnityEngine;
using BTs;

[CreateAssetMenu(fileName = "BT_ANITA", menuName = "Behaviour Trees/BT_ANITA", order = 1)]
public class BT_ANITA : BehaviourTree
{
        
    public override void OnConstruction()
    {
        
    }
}

class BT_Sweep_Sing : BehaviourTree
{
    Action clear = new ACTION_ClearUtterance();
    Action produceBroom = new ACTION_Activate("theBroom");
    Action produceMusicalNotes = new ACTION_Activate("theNotes");
    //Action wanderArround = new ACTION_WanderAround("theSweepingPoint", ); //Ejemplo Ant Life

    public override void OnConstruction()
    {
        
    }
}