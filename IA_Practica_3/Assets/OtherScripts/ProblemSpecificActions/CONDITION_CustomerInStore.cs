using UnityEngine;
using BTs;

public class CONDITION_CustomerInStore : Condition
{

    public string parameterCustomerOut;

    public CONDITION_CustomerInStore() {}
    public CONDITION_CustomerInStore(string customerOut) {
        parameterCustomerOut = customerOut;
    }

    public override bool Check ()
    {
        GameObject customer = SensingUtils.FindInstanceWithinRadius(
            blackboard.Get<GameObject>("theStoreEntrance"), "CUSTOMER", 30
        );
        if (customer != null)
        {
            blackboard.Put(parameterCustomerOut, customer);
            return true;
        }
        else
        {
            return false;
        }
    }

}
