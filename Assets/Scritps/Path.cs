using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < transform.childCount; i++)
        {
            var t = transform.GetChild(i);
            Gizmos.DrawSphere(t.position, 0.15f);

            if (i > 0)
            {
                var prev = transform.GetChild(i - 1);
                Gizmos.DrawLine(prev.position, t.position);
            }
        }
    }

    private void OnValidate()
    {
        points.Clear();
        for (int i = 0; i < transform.childCount; i++)
            points.Add(transform.GetChild(i));
    }
}
