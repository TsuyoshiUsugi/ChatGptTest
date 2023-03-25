using UnityEngine;

public class Fire : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IHit>()?.Hit();
    }
}
