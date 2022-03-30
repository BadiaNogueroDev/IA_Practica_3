using UnityEngine;
using BTs;

public class ACTION_TellEngaged : Action
{
    public string keyIndex;
    public string KeyDuration;

    public ACTION_TellEngaged(string keyIndex, string keyDuration) {
        this.keyIndex = keyIndex;
        this.KeyDuration = keyDuration;
    }

    private IDialogSystem dialogSystem;
    private float elapsedTime;
    private int index;
    private float duration;

    public override void OnInitialize()
    {
        dialogSystem = GetComponent<IDialogSystem>();
        index = blackboard.Get<int>(keyIndex);
        duration = blackboard.Get<float>(KeyDuration);
       
        dialogSystem.SetUtterance(index);
        elapsedTime = 0;
    }

    public override Status OnTick ()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= duration)
        {
            if (dialogSystem.Tell(index, false))
                return Status.SUCCEEDED;
            else return Status.FAILED;
        }
        else return Status.RUNNING;
    }

    public override void OnAbort()
    {
        dialogSystem.ClearUtterance();
    }

}
