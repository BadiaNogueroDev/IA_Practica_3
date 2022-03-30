using UnityEngine;
using BTs;

public class ACTION_Utter : Action
{

    public string parameterIndex;
    public string parameterDuration;

    public ACTION_Utter()  { }
    
    public ACTION_Utter(string index, string duration = "3") {
        this.parameterIndex = index;
        this.parameterDuration = duration;
    }

    private IDialogSystem dialogSystem;
    private float duration;
    private float elapsedTime;

    public override void OnInitialize()
    {
        dialogSystem = gameObject.GetComponent<IDialogSystem>();
        duration = blackboard.Get<float>(parameterDuration);

        if (dialogSystem == null)
        {
            Debug.Log("No dialog system in Utter. Succeeding, anyway");
            elapsedTime = duration;
        }
        else
        {
            dialogSystem.SetUtterance(blackboard.Get<int>(parameterIndex));
            elapsedTime = 0;
        }
    }

    public override Status OnTick ()
    {
        if (elapsedTime >= duration)
        {
            dialogSystem.ClearUtterance();
            return Status.SUCCEEDED;
        }
        else
        {
            elapsedTime += Time.deltaTime;
            return Status.RUNNING;
        }
    }

    public override void OnAbort()
    {
        dialogSystem.ClearUtterance();
    }

}
