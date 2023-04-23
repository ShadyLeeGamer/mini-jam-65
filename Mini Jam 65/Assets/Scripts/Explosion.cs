using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    public GameObject oreEFX;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            Instantiate(gameObject, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Ore"))
        {
            if(FindObjectOfType<MenuManager>() != null)
                Destroy(FindObjectOfType<MenuManager>().gameObject);

            Instantiate(oreEFX, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            FindObjectOfType<SceneManaging>().Play(SceneManager.GetActiveScene().name == "End" ? 1 :
                                                                                                 0);
        }
    }
}