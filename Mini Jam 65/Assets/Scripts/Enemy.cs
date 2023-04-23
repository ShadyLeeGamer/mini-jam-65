using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float dir;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 move = new Vector2(dir * moveSpeed, rb.velocity.y);
        rb.velocity = move;
        transform.eulerAngles = Vector3.up * (rb.velocity.x > 0 ? 0 : 180);
    }
}
