using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    public SpriteRenderer bgRenderer;
    public float playDelay;
    public Color winCol;

    public void Play(int mode)
    {
        StartCoroutine(Loading(mode));
    }
    IEnumerator Loading(int mode)
    {
        Color defaultCol = bgRenderer.color;
        float percent = 0;

        while (true)
        {
            percent += Time.deltaTime / playDelay;
            bgRenderer.color = Color.Lerp(defaultCol, winCol, percent);

            if(percent >= 1)
                SceneManager.LoadScene(mode == 0 ? SceneManager.GetActiveScene().buildIndex + 1 :
                                                                                              0);
            yield return null;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
