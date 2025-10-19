using UnityEngine;

public class poses : MonoBehaviour {

    
    public int pose;
    string[] posename = new string[20] { "pose01", "pose02", "pose03", "pose04", "pose05", "pose06", "pose07", "pose08"
                                       , "pose09", "pose10", "pose11", "pose12", "pose13", "pose14", "pose15", "pose16"
                                       , "pose17", "Female Walk 0", "Slow run", "Drunk Run Forward"};

    void Start ()
    {
        ApplyPose();
    }

    public void SetAndPlayPose(int pos)
    {
        pose = pos;
        ApplyPose();
    }

    [ContextMenu("ApplyPose")]
    private void ApplyPose()
    {
        if (pose > posename.Length) pose = Random.Range(1, posename.Length);
        GetComponent<Animator>().Play(posename[pose - 1], 0);
    }
}
