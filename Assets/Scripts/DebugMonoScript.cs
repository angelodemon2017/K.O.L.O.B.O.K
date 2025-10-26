using UnityEngine;
using UnityEngine.Playables;

public class DebugMonoScript : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private PlayableAsset _clip1;
    [SerializeField] private PlayableAsset _clip2;

    [ContextMenu("RunClip1")]
    public void RunClip1()
    {
        _playableDirector.Play(_clip1);
    }

    [ContextMenu("RunClip2")]
    public void RunClip2()
    {
        _playableDirector.Play(_clip2);
    }
}