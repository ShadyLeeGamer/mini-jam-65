using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static DontDestroy instance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
