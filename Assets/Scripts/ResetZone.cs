using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ResetZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<GameManagerr>().Miss();
    }

}
