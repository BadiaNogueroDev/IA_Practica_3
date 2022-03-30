using UnityEngine;
using BTs;

[CreateAssetMenu(fileName = "BT_CUSTOMER", menuName = "Behaviour Trees/BT_CUSTOMER", order = 1)]
public class BT_CUSTOMER : BehaviourTree
{
   
    public override void OnConstruction()
    {

        Parallel child1 = new ParallelAnd(
            new ACTION_Arrive("storeEntrance"),
            new ACTION_Utter("9", "2")
        );
        Action child2 = new ACTION_Utter("0", "2");
        RepeatUntilSuccessDecorator child3 = new RepeatUntilSuccessDecorator(
           new CONDITION_EngagedInDialog()
        ); 
        RepeatUntilFailureDecorator child4 = new RepeatUntilFailureDecorator(
            new CONDITION_EngagedInDialog()
        );
        Action child5 = new ACTION_Utter("4", "2");
        Parallel child6 = new ParallelAnd(
            new ACTION_Arrive("exitPoint"),
            new Selector(
                new Sequence(
                    new LambdaCondition(() => { return gameObject.GetComponent<CUSTOMER_BLACKBOARD>().goodMood; }),
                    new ACTION_Activate("shoppingBag"),
                    new ACTION_Utter("5", "2"),
                    new ACTION_Utter("6", "2")
                ),
                new Sequence(
                    new ACTION_Utter("7", "2"),
                    new ACTION_Utter("8", "2")
                )
            )
        );  

        root = new Sequence(child1, child2, child3, child4, child5, child6);
    }
}
