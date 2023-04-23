using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class BombController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    Rigidbody2D rb;
    public bool isGrounded;
    public int jumpCount = 1;
    bool isJumping;
    Player player;
    Transform playerT;
    SpriteRenderer GFX;
    Animator animator;
    public float timer;
    public TextMeshPro timerTXT;
    public Color blinkColour;
    public GameObject explodeEFX;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GFX = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = GFX.GetComponent<Animator>();

        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        playerT = player.transform;
        player.SwitchBombs += Switching;

        GFX.color = Color.grey;
    }

    void Update()
    {
        if (player.bomb == this)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
                Explode();

            StartCoroutine(Blinking());

            if (Input.GetKeyDown(KeyCode.F))
                Explode();
        }
        if (transform.position.y < -5)
            Explode();


        timerTXT.text = Mathf.RoundToInt(timer).ToString();
        timerTXT.color = GFX.color;
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }

        if (rb.velocity.y < 0)
            rb.gravityScale = fallMultiplier;
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
            rb.gravityScale = lowJumpMultiplier;
        else
            rb.gravityScale = 5f;
    }

    public void Move(Vector2 input)
    {
        Vector2 moveVelocity = new Vector2(input.x * moveSpeed,
                                           rb.velocity.y);
        rb.velocity = moveVelocity;

        if(rb.velocity.x != 0)
            GFX.transform.eulerAngles = Vector3.up * (rb.velocity.x > 0 ? 0 : 180);
        if(jumpCount == 1)
            animator.SetFloat("VelocityX", Mathf.Abs(input.x));

        if (input.y == 1 && jumpCount == 1 && rb.velocity.y <= 0)
            Jump();
        float fixedVelocityY;
        if (rb.velocity.y < 0 || input.y != 0)
            fixedVelocityY = Mathf.Sign(rb.velocity.y);
        else
            fixedVelocityY = 0;

        animator.SetFloat("VelocityY", fixedVelocityY);

    }

    void Jump()
    {
        isJumping = true;
    }

    void OnMouseDown()
    {
        if (player != null)
        {
            player.bomb = this;
            playerT.SetParent(transform);
            playerT.position = transform.position;

            player.SwitchBombs?.Invoke();
        }
    }

    void Switching()
    {
        if (player.bomb == this)
            GFX.color = Color.white;
        else if(this != null)
        {
            rb.velocity = Vector2.zero;
            GFX.color = Color.grey;
        }
    }

    IEnumerator Blinking()
    {
        while(true)
        {
            float blinkSpeed = 1 / timer;
            float percent = Mathf.PingPong(Time.time * blinkSpeed, 1);
            GFX.color = Color.Lerp(Color.white, blinkColour, percent);
           
            yield return null;
        }
    }

    public void Explode()
    {
        timer = 0;
        playerT.SetParent(null);
        Instantiate(explodeEFX, transform.position, Quaternion.identity);

        if (FindObjectOfType<GameUI>() != null)
            FindObjectOfType<GameUI>().remainingBar.value--;
        if (FindObjectOfType<MenuManager>() != null)
        {
            MenuManager menuManager = FindObjectOfType<MenuManager>();
            menuManager.boomCount++;
            menuManager.SpawnNewBomb();
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillZone"))
        {
            if(other.GetComponent<Enemy>())
            {
                Enemy enemy = other.GetComponent<Enemy>();
                Instantiate(explodeEFX, enemy.transform.position, Quaternion.identity);
                Destroy(enemy.gameObject);
            }
            Explode();
        }
    }
}