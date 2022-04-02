using UnityEngine;
using BTs;

[CreateAssetMenu(fileName = "BT_ANITA", menuName = "Behaviour Trees/BT_ANITA", order = 1)]
public class BT_ANITA : BehaviourTree
{
    Condition customerInStore = new CONDITION_CustomerInStore("customer");
    Condition alwaysTrue = new CONDITION_AlwaysTrue();
    Action hideBroom = new ACTION_Deactivate("theBroom");
    Action hideNotes = new ACTION_Deactivate("theNotes");
    Action say = new ACTION_Utter("10", "2");
    Action goToDesk = new ACTION_Arrive("theFrontOfDesk");
    
    public override void OnConstruction()
    {
        root = new DynamicSelector();
        root.AddChild(customerInStore, new Sequence(hideBroom, hideNotes, say, goToDesk, new BT_See_to_Customer()));
        root.AddChild(alwaysTrue, new BT_Sweep_Sing());
    }
}

class BT_Sweep_Sing : BehaviourTree
{
    Action clear = new ACTION_ClearUtterance();
    Action produceBroom = new ACTION_Activate("theBroom");
    Action produceMusicalNotes = new ACTION_Activate("theNotes");
    Action HighSeekWanderArround = new ACTION_WanderAround("theSweepingPoint", "0.8");
    Action LowSeekWanderArround = new ACTION_WanderAround("theSweepingPoint", "0.2");
    Condition tooFar = new CONDITION_FeelUnsafe("theSweepingPoint", "safeRadius", "extraSafeRadius");
    Condition alwaysTrue = new CONDITION_AlwaysTrue();
    DynamicSelector wanderArroundSelector = new DynamicSelector();

    public override void OnConstruction()
    {
        wanderArroundSelector.AddChild(tooFar, HighSeekWanderArround);
        wanderArroundSelector.AddChild(alwaysTrue, LowSeekWanderArround);
        root = new Sequence(clear, produceBroom, produceMusicalNotes, wanderArroundSelector);
    }
}

class BT_See_to_Customer : BehaviourTree
{
    Action engageInDialog = new ACTION_EngageInDialog("customer");
    Action askEngagedCustomer = new ACTION_AskEngaged("11", "2", "answer");

    Selector selector;
    Action parseAnswer = new ACTION_ParseAnswer("answer", "productRequested");
    Action engagedYesDialog = new ACTION_TellEngaged("13", "2");

    Action engagedNoDialog = new ACTION_TellEngaged("12", "2");

    Action disengageFromDialog = new ACTION_DisengageFromDialog();

    public override void OnConstruction()
    {
        selector = new Selector(new Sequence(parseAnswer,engagedYesDialog, new BT_Sell_Product()), engagedNoDialog);
        root = new Sequence(engageInDialog, askEngagedCustomer, selector, disengageFromDialog);
    }
}

class BT_Sell_Product : BehaviourTree
{
    Condition existences = new CONDITION_CheckExistences("productRequested");
    Action sellProduct = new ACTION_Sell("productRequested");
    Action sayHere = new ACTION_TellEngaged("14","2");
    Action sayNoneLeft = new ACTION_TellEngaged("15", "2");

    public override void OnConstruction()
    {
        root = new Selector(new Sequence(existences, sellProduct, sayHere), sayNoneLeft);
    }
}