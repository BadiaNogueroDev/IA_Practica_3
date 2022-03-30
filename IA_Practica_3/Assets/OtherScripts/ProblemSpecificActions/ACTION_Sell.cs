using UnityEngine;
using BTs;

public class ACTION_Sell : Action
{
    public string keyItem;
    
    public ACTION_Sell(string keyItem) {
        this.keyItem = keyItem;
    }

    private ANITAs_BLACKBOARD bl;
    string item;

    public override void OnInitialize()
    {
        bl = (ANITAs_BLACKBOARD)blackboard;
        item = bl.Get<string>(keyItem);
    }

    public override Status OnTick()
    {
        if (bl.Sell(item)) return Status.SUCCEEDED;
        else return Status.FAILED;

    }
}
