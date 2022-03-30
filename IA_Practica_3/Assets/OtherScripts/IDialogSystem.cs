
// Classes that want to behave as a DialogSystems must implment this interface

public interface IDialogSystem // by E.Sesa
{
    // makes receptor set current utterance to indexth
    void SetUtterance(int index);
    // clears the utterance "line" (usually a textMesh)
    void ClearUtterance();

    // engage partner as your partner in dialog. False if partner refuses
    bool EngageInDialog(IDialogSystem partner);
    // receive engament request. Return false to refuse
    bool BeEngagedInDialog(IDialogSystem partner);

    // ask to engaged partner. index is the index of the question. 
    // if second parameter is true make an utterance 
    // result is the received answer
    string Ask(int index, bool utter);
    // Receice a question. Result is the answer
    string BeAsked(string question);

    // tell something to your dialog partner. 
    // if second parameter is true make an utterance 
    bool Tell(int index, bool utter);
    // receice sentence 
    void BeTold(string sentence);
   
    // tell your partner dialog is over
    void DisengageFromDialog();
    // When invoked, your partner is telling you dialog is over
    void BeDisengagedFromDialog();

    // do I have a dialog partner?
    bool IsEngagedInDialog();
}
