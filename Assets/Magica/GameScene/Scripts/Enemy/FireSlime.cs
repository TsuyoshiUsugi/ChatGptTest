using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireSlime : MonoBehaviour
{
    PlayerMoveController _playerMoveController;

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
}
