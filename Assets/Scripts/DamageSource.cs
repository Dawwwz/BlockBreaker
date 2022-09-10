using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<HealthManager>(out HealthManager component))
        {
            if (component.GetComponent<Brick>().unbreakable) { return; }
            component.Set_Damage(damage);
            component.GetComponent<Brick>().Set_State();
        }
    }
}
