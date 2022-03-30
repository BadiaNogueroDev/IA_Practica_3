using UnityEngine;
using BTs;

public class ACTION_AskEngaged : Action
{
    public string keyUtteranceIndex;
    public string keyDuration;
    public string keyoutAnswer; // this is an out parameter
    
    public ACTION_AskEngaged(string keyUtteranceIndex, string keyDuration, string keyoutAnswer)  {
        this.keyUtteranceIndex = keyUtteranceIndex;
        this.keyDuration = keyDuration;
        this.keyoutAnswer = keyoutAnswer;
    }

    private IDialogSystem dialogSystem;
    private float elapsedTime;
    private float duration;
    private int utteranceIndex;
    private string answer; 

    public override void OnInitialize()
    {

        duration = blackboard.Get<float>(keyDuration);
        utteranceIndex = blackboard.Get<int>(keyUtteranceIndex);

        dialogSystem = gameObject.GetComponent<IDialogSystem>();
        // first utter (show the utterance)
        dialogSystem.SetUtterance(utteranceIndex);
        // then give partner time to answer
        elapsedTime = 0;
    }

    public override Status OnTick ()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= duration)
        {
            answer = dialogSystem.Ask(utteranceIndex, false); // no need to utter now. 
            if (answer != null)
            {
                // save the answer in the blackboard under the given name
                blackboard.Put(keyoutAnswer, answer);
                return Status.SUCCEEDED;
            }
            else return Status.FAILED;
        }
        else
        {
            return Status.RUNNING;
        }
    }

    public override void OnAbort()
    {
        if (dialogSystem != null) dialogSystem.ClearUtterance();
    }

}
