using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player;

    public float smoothSpeed;
    public Vector3 offset;

    void FixedUpdate()
    {
        float targetX = player.bomb != null ? player.bomb.transform.position.x + offset.x : 0;
        Vector3 desiredPos = new Vector3(targetX, offset.y, offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,
                                                        desiredPos,
                                                      smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
