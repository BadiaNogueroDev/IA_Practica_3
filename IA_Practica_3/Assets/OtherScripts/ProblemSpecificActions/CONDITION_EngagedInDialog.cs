using UnityEngine;
using System;
using BTs;

public class CONDITION_EngagedInDialog : Condition
{

    public CONDITION_EngagedInDialog()  { }
   
    public override bool Check ()
    {
        try
        {
            if (gameObject.GetComponent<IDialogSystem>().IsEngagedInDialog())
                return true;
            else return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
