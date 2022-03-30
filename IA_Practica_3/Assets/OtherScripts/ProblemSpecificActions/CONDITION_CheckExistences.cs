using UnityEngine;
using BTs;

public class CONDITION_CheckExistences : Condition
{

    public string keyItem;

    public CONDITION_CheckExistences(string keyItem)  {
        this.keyItem = keyItem;
    }
    

    public override bool Check ()
    {
        ANITAs_BLACKBOARD blackboard = GetComponent<ANITAs_BLACKBOARD>();
        if (blackboard != null) return blackboard.CheckExistences(blackboard.Get<string>(keyItem));
        else return false;
    }

}
