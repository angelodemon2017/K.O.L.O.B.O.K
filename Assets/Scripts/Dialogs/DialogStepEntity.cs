using System;
using System.Collections.Generic;

[Serializable]
public class DialogStepEntity
{
    public ECharacters character;
    public string KeyOfDialogStep;
    public List<string> variantsToContinue;
}