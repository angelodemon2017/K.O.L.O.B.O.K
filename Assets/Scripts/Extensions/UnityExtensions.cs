using UnityEngine;

public static class UnityExtensions
{
    public static void DestroyChildrens(this Transform transform)
    {
        while (transform.childCount > 0)
        {
            if (Application.isPlaying)
            {
                GameObject.Destroy(transform.GetChild(0).gameObject);
            }
            else
            {
                GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }
}