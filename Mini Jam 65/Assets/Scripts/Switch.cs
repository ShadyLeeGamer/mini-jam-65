using UnityEngine;

public class Switch : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public GameObject barrier;
    AudioSource SFX;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SFX = GetComponent<AudioSource>();
    }

    public void Interact(bool isPressed)
    {
        spriteRenderer.sprite = sprites[isPressed ? 0 : 1];
        barrier.SetActive(!isPressed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            SFX.Play();
            Interact(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            Interact(false);
    }
}