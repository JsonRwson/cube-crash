using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject Player;
    Rigidbody2D PlayerBody;
    Vector2 TargetPosition;

    private float MoveSpeed = 22f;
    private float DesiredX;
    private float DesiredY;
    private GameObject[] Enemies;
    public bool CanMove;

    void Start()
    {
        DesiredX = Player.transform.position.x;
        DesiredY = Player.transform.position.y;
        PlayerBody = Player.GetComponent<Rigidbody2D>();
        CanMove = true;
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        if(PlayerBody != null)
        {
            if(PlayerBody.velocity == new Vector2(0,0)){
                CanMove = true;
            }
            else
            {
                CanMove = false; 
            }
        }
    }

    public void Move(int direction)
    {
        /* 
            directions:
            0: up
            1: right
            2: down
            3: left
        */

        if(PlayerBody != null)
        {
            if(CanMove)
            {
                switch(direction)
                {
                    case 0:
                        PlayerBody.AddForce(new Vector2(0, +MoveSpeed), ForceMode2D.Impulse);
                        break;

                    case 1:
                        PlayerBody.AddForce(new Vector2(MoveSpeed, 0), ForceMode2D.Impulse);
                        break;

                    case 2:
                        PlayerBody.AddForce(new Vector2(0, -MoveSpeed), ForceMode2D.Impulse);
                        break;

                    case 3:
                        PlayerBody.AddForce(new Vector2(-MoveSpeed, 0), ForceMode2D.Impulse);
                        break;

                    default:
                        break;
                }
                TriggerEnemyMove();
            }
        }
    }

    public void TriggerEnemyMove()
    {
        for(int i = 0; i < Enemies.Length; i++)
        {
            if(Enemies[i] != null)
            {
                Enemies[i].GetComponent<MoveEnemy>().PlayerHasMoved();
            }
        }
    }

    public void RefreshEnemiesArray()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
}