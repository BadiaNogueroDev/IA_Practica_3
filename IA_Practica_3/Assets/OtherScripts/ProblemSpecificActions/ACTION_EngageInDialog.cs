using UnityEngine;
using BTs;

public class ACTION_EngageInDialog : Action
{
    public string keyPartner;

    public ACTION_EngageInDialog(string keyPartner) {
        this.keyPartner = keyPartner;
    }

    private GameObject thePartner;
    private IDialogSystem partnerDialogSystem;

    public override void OnInitialize()
    {
        thePartner = blackboard.Get<GameObject>(keyPartner);
        if (thePartner!=null) partnerDialogSystem = thePartner.GetComponent<IDialogSystem>();
    }

    public override Status OnTick ()
    {
        if (partnerDialogSystem == null)
        {
            Debug.Log("Engage in dialog fails because target or its dialogSystem not found");
            return Status.FAILED;
        }
        else
        {
            // let's call our own engage...
            if (gameObject.GetComponent<IDialogSystem>().EngageInDialog(partnerDialogSystem))
                return Status.SUCCEEDED;
            else return Status.FAILED;
        }

    }

    

}
