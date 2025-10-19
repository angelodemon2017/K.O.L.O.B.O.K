using System.Collections.Generic;

public class DialogEntity
{
    public List<DialogStepEntity> dialogSteps = new();

    public void SetDialog(BaseDialogConfig baseDialogConfig)
    {
        dialogSteps.Clear();
        dialogSteps.AddRange(baseDialogConfig.DialogStepEntities);
    }

    public bool IsEndStep(int step)
    {
        return step >= dialogSteps.Count;
    }

    public DialogStepEntity GetDialogStep(int step)
    {
        if (step < 0)
            step = 0;
        if (step >= dialogSteps.Count)
        {
            step = dialogSteps.Count - 1;
        }
        return dialogSteps[step];
    }
}