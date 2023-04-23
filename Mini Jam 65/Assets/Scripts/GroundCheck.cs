using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float type;
    Player player;
    Enemy enemy;

    void Awake()
    {
        if(type == 1)
            enemy = transform.parent.GetComponent<Enemy>();
        else
            player = GetComponent<Player>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (type == 0 && other.CompareTag("Ground"))
            player.bomb.jumpCount = 1;
        if (type == 1 && (other.CompareTag("Ground") || other.CompareTag("Enemy")))
            enemy.dir = -enemy.dir;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (type == 0 && other.CompareTag("Ground"))
            player.bomb.jumpCount = 0;
    }
}
