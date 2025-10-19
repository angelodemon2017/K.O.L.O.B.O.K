using System;

public class LaunchToCosmosWindowModel : WindowModelBase
{
    public float PowerToLaunch = 0;
    public float PowerFromLaunch = 100;
    public float PowerByTap = 5;

    public Action CallBackAfterPowerToLaunch;

    public float GetPercent => PowerToLaunch / PowerFromLaunch;
}