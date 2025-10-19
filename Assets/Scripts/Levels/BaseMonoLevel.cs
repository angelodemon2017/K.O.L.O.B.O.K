using System;
using UnityEngine;

public class BaseMonoLevel : MonoBehaviour
{
    public SimpleBallController simpleBallController;

    public virtual Inputer GetInputer => simpleBallController;

    public Action<int> ChangeCheckpoint;

    public virtual void StartLevel(int checkPoint = 0)
    {

    }

    public virtual void EndLevel()
    {

    }
}