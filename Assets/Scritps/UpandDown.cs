using UnityEngine;

public class UpandDown : MonoBehaviour
{
    public float moveDistance = 0.3f; // 좌우 이동 거리
    public float moveSpeed = 2f;      // 이동 속도

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
    }
}
