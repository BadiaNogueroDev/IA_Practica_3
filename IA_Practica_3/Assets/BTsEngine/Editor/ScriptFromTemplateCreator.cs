using UnityEngine;
using UnityEditor;

public class ScriptFromTemplateCreator
{
    private const string pathToTemplate_BT = "Assets/BTsEngine/Editor/Templates/BT_Template.cs.txt";
    private const string pathToTemplate_Action = "Assets/BTsEngine/Editor/Templates/Action_Template.cs.txt";
    private const string pathToTemplate_Condition = "Assets/BTsEngine/Editor/Templates/Condition_Template.cs.txt";

    [MenuItem(itemName: "Assets/Create/BT Script", isValidateFunction: false, priority: 22)]
    public static void CreateScriptFromTemplate_00()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToTemplate_BT, "new BT.cs");
    }

    [MenuItem(itemName: "Assets/Create/ACTION Script", isValidateFunction: false, priority: 22)]
    public static void CreateScriptFromTemplate_01()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToTemplate_Action, "new Action.cs");
    }

    [MenuItem(itemName: "Assets/Create/CONDITION Script", isValidateFunction: false, priority: 22)]
    public static void CreateScriptFromTemplate_02()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToTemplate_Condition, "new Condition.cs");
    }
}
