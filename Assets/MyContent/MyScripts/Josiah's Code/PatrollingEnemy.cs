using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public Transform LeftPatrolPoint;
    public Transform RightPatrolPoint;

    [Header("The body of the enemy that is moving")]
    public Transform Body;

    public int MovementSpeed;

    private bool isPatrollingLeft = true;
    private float proximity = 0.02f;

    private bool isAlive;

    private void Awake()
    {
        isAlive = true;
    }

    private void Update()
    {
        if(isAlive)
        {
            MoveEnemy();
        }
        else
        {
            //Move to ground level
        }
    }
    private void MoveEnemy()
    {
        if (isPatrollingLeft)
        {
            Body.transform.position = Vector2.MoveTowards(Body.transform.position, LeftPatrolPoint.position, MovementSpeed * Time.deltaTime);
            if (Vector2.Distance(Body.transform.position, LeftPatrolPoint.position) <= proximity)
            {
                isPatrollingLeft = false;
            }
        }
        else
        {
            Body.transform.position = Vector2.MoveTowards(Body.transform.position, RightPatrolPoint.position, MovementSpeed * Time.deltaTime);
            if (Vector2.Distance(Body.transform.position, RightPatrolPoint.position) <= proximity)
            {
                isPatrollingLeft = true;
            }
        }
    }
    public void SetIsAlive(bool alive)
    {
        isAlive = alive;
    }
}
