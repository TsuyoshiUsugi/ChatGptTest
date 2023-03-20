using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の元まで移動し攻撃する
/// 敵が視界内に入った場合(Raycastが敵に直接当たった場合)移動を止め攻撃
/// </summary>
[RequireComponent(typeof(SearchEnemyOrder))]
public class AttackOrder : MonoBehaviour, ICommand
{
    [Header("参照")]
    NavMeshAgent _navMeshAgent;
    SearchEnemyOrder _searchEnemyOrder;
    EnemyBrain _enemyBrain;
    

    [Header("設定値")]
    WaitForSeconds wait = new WaitForSeconds(1);

    void start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _searchEnemyOrder = GetComponent<SearchEnemyOrder>();
    }

    /// <summary>
    /// 引数の敵を追跡する
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
    /// 敵が生きている間指定された秒数検索し続ける
    /// </summary>
    /// <returns></returns>
    IEnumerator Search()
    {
        while(_enemyBrain.Hp > 0)
        {
            //Raycastし、敵に当たったらNavmeshとめて射撃
            yield return wait;
        }
    }
}
