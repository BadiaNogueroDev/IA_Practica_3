using UnityEngine;
using BTs;

public class CONDITION_FeelUnsafe : Condition
{
    public string keyAttractor;
    public string keySafeRadius;
    public string keyExtraSafeRadius;

    // Constructor
    public CONDITION_FeelUnsafe(string keyAttractor,
                                string keySafeRadius,
                                string keyExtraSafeRadius)
    {
        this.keyAttractor = keyAttractor;
        this.keySafeRadius = keySafeRadius;
        this.keyExtraSafeRadius = keyExtraSafeRadius;
    }

    private bool lastTick = false;

    public override bool Check()
    {
        // outside safe, return false
        // inside extraSafe, return false
        // any other case return same as at lastTick
        if (SensingUtils.DistanceToTarget(gameObject, attractor) > safeRadius)
            lastTick = true;
        else if (SensingUtils.DistanceToTarget(gameObject, attractor) < extraSafeRadius)
            lastTick = false;
        return lastTick;

    }

    private GameObject attractor;
    private float safeRadius;
    private float extraSafeRadius;


    public override void OnInitialize()
    {
        attractor = blackboard.Get<GameObject>(keyAttractor);
        safeRadius = blackboard.Get<float>(keySafeRadius);
        extraSafeRadius = blackboard.Get<float>(keyExtraSafeRadius);
        lastTick = false;
    }
}
