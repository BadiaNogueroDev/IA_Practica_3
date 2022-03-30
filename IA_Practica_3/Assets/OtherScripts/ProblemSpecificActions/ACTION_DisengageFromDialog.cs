using UnityEngine;
using BTs;

public class ACTION_DisengageFromDialog : Action
{
    public ACTION_DisengageFromDialog() { }  
    
   
    public override Status OnTick ()
    {
        GetComponent<IDialogSystem>().DisengageFromDialog();
        return Status.SUCCEEDED;
    }
}
