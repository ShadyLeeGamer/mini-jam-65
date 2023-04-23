using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public BombController bomb;
    [HideInInspector] public Vector2 input;
    public Action SwitchBombs;

    void Update()
    {
        if (bomb != null)
        {
            if (Input.GetKey(KeyCode.D))
                input.x = 1;
            else if (Input.GetKey(KeyCode.A))
                input.x = -1;
            else
                input.x = 0;

            input.y = Input.GetKey(KeyCode.W) ? 1 : 0;

            bomb.Move(input);
        }
    }
}