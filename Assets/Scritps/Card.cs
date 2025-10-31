using UnityEngine;

public class DragObject2D : MonoBehaviour
{
    private Vector3 offset;

    void OnMouseDown()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // 2D에서는 z 좌표 0으로
        offset = transform.position - mouseWorldPos;
    }

    void OnMouseDrag()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos + offset;
    }
}
