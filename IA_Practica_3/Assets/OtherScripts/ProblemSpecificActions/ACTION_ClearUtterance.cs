using UnityEngine;
using BTs;

public class ACTION_ClearUtterance : Action
{

    public ACTION_ClearUtterance() { }

    public override Status OnTick ()
    {
        GetComponent<IDialogSystem>().ClearUtterance();
        return Status.SUCCEEDED;
    }

}
