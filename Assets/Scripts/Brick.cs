using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Brick : MonoBehaviour
{
    public MeshRenderer[] mesh;
    public Material[] states;
    public int health;
    public int points;
    public bool unbreakable;

    private void Awake()
    {
     //    material = GetComponent<MeshRenderer>();
         Set_State();
    }
    public void Set_State()
    {
        for(int i = 0; i < mesh.Length; i++)
        {   
            if(health > 0 && health < states.Length) { mesh[i].material = states[health]; }
            if (unbreakable) { mesh[i].material = states[0]; }
        }
    }
    
    private void OnDestroy()
    {
        if(FindObjectOfType<GameManagerr>() != null)
        {
            FindObjectOfType<GameManagerr>().Score(this);
        }
    }
}
