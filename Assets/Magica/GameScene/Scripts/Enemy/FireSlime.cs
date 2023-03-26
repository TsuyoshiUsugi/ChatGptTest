using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireSlime : MonoBehaviour, IHit
{
    PlayerMoveController _playerMoveController;

    [SerializeField] int _hp = 20;

    int _damage = 10;
    NavMeshAgent _navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerMoveController = FindAnyObjectByType<PlayerMoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        _navMeshAgent.SetDestination(_playerMoveController.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerManager>()?.Hit(_damage);
    }

    public void Hit(int damage)
    {
        _hp -= damage;

        if(_hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
