using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    Brick brick;

    private void Start()
    {
        brick = GetComponent<Brick>();
    }
    public void Set_Damage(int damage_Value)
    {
        brick.health -= damage_Value;
        if(brick.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
