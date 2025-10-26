using DG.Tweening;
using System;
using System.Threading.Tasks;

public static class DOTweenExtensions
{
    public static Task Wait(float seconds)
    {
        return DOTween.To(() => 0, x => { }, 1, seconds)
                     .SetEase(Ease.Linear)
                     .AsyncWaitForCompletion();
    }

    public static Sequence CallAfterDelay(float delay, Action callback)
    {
        return DOTween.Sequence()
            .AppendInterval(delay)
            .AppendCallback(() => callback?.Invoke());
    }
}