using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̌��܂ňړ����U������
/// �G�����E���ɓ������ꍇ(Raycast���G�ɒ��ړ��������ꍇ)�ړ����~�ߍU��
/// </summary>
[RequireComponent(typeof(SearchEnemyOrder))]
public class AttackOrder : MonoBehaviour, ICommand
{
    [Header("�Q��")]
    NavMeshAgent _navMeshAgent;
    SearchEnemyOrder _searchEnemyOrder;
    EnemyBrain _enemyBrain;
    

    [Header("�ݒ�l")]
    WaitForSeconds wait = new WaitForSeconds(1);

    void start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _searchEnemyOrder = GetComponent<SearchEnemyOrder>();
    }

    /// <summary>
    /// �����̓G��ǐՂ���
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="bot"></param>
    public void Command(string[] arguments, GameObject bot)
    {
        var index = int.Parse(arguments[0]);
        _enemyBrain = _searchEnemyOrder.Enemies[index];
        _navMeshAgent.SetDestination(_enemyBrain.gameObject.transform.position);
        StartCoroutine(nameof(Search));
    }

    /// <summary>
    /// �G�������Ă���Ԏw�肳�ꂽ�b��������������
    /// </summary>
    /// <returns></returns>
    IEnumerator Search()
    {
        while(_enemyBrain.Hp > 0)
        {
            //Raycast���A�G�ɓ���������Navmesh�Ƃ߂Ďˌ�
            yield return wait;
        }
    }
}
