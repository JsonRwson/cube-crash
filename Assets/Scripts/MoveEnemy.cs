using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    Rigidbody2D EnemyBody;
    Vector2 TargetPosition;

    private float MoveSpeed = 22f;
    private float DesiredX;
    private float DesiredY;
    private GameObject[] Enemies;
    private bool CanMove;

    void Start()
    {
        DesiredX = transform.position.x;
        DesiredY = transform.position.y;
        EnemyBody = GetComponent<Rigidbody2D>();
        CanMove = true;
    }

    void Update()
    {
        if(EnemyBody.velocity == new Vector2(0,0)){
            CanMove = true;
        }
        else
        {
           CanMove = false; 
        }
    }

    public void PlayerHasMoved(){
        Move(Random.Range(0, 4));
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
        if(CanMove)
        {
            switch(direction)
            {
                case 0:
                    EnemyBody.AddForce(new Vector2(0, +MoveSpeed), ForceMode2D.Impulse);
                    break;

                case 1:
                    EnemyBody.AddForce(new Vector2(MoveSpeed, 0), ForceMode2D.Impulse);
                    break;

                case 2:
                    EnemyBody.AddForce(new Vector2(0, -MoveSpeed), ForceMode2D.Impulse);
                    break;

                case 3:
                    EnemyBody.AddForce(new Vector2(-MoveSpeed, 0), ForceMode2D.Impulse);
                    break;

                default:
                    break;
            }
        }
    }
}