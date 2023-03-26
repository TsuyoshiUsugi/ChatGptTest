using UnityEngine;

public class Fire : MonoBehaviour
{
    int _damage = 30;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IHit>()?.Hit(_damage);

        Destroy(this.gameObject);
    }
}
