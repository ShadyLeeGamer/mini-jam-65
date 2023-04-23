using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image timerCountIMG;
    public Sprite[] numberSPR;
    public Slider remainingBar;
    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        BombController[] totalBombs = FindObjectsOfType<BombController>();
        remainingBar.maxValue = totalBombs.Length;
        remainingBar.value = remainingBar.maxValue;
    }

    void Update()
    {
        if(player.bomb != null)
            timerCountIMG.sprite = numberSPR[Mathf.RoundToInt(player.bomb.timer)];
    }
}