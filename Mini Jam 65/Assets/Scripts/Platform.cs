using UnityEngine;

public class Platform : MonoBehaviour
{
    Collider2D collider;
    Player player;

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Player>();
    }

    float halfColliderHeight;
    void Start()
    {
        halfColliderHeight = transform.localScale.y / 2;
    }

    void Update()
    {
        bool passable = player.transform.position.y < transform.position.y + halfColliderHeight;
        collider.isTrigger = passable;
    }
}
