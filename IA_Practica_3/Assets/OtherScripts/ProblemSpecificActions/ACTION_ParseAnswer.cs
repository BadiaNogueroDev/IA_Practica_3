using UnityEngine;
using BTs;

public class ACTION_ParseAnswer : Action
{
    public string keyAnswer;
    public string keyoutProductRequested; // this is an out parameter

    public ACTION_ParseAnswer(string keyAnswer, string keyoutProductRequested) {
        this.keyAnswer = keyAnswer;
        this.keyoutProductRequested = keyoutProductRequested;
    }


    public override Status OnTick ()
    {
        string itemRequested = null;

        // retrieve the answer from the blackboard
        string answer = blackboard.Get<string>(keyAnswer);

        // analyze it...
        if (answer.ToUpper().Contains("APPLE"))
            itemRequested = "APPLE";
        else if (answer.ToUpper().Contains("PEACH"))
            itemRequested = "PEACH";

        if (itemRequested != null)
        {
            blackboard.Put(keyoutProductRequested, itemRequested);
            return Status.SUCCEEDED;
        }
        else return Status.FAILED;
    }

    

}
