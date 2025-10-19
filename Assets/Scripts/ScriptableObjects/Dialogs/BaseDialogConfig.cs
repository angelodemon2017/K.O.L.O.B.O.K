using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog model", menuName = "Dialog/Dialog config", order = 1)]
public class BaseDialogConfig : ScriptableObject
{
    public List<DialogStepEntity> DialogStepEntities;
}