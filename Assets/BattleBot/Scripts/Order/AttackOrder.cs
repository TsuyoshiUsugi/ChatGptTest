using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// “G‚ÌŒ³‚Ü‚ÅˆÚ“®‚µUŒ‚‚·‚é
/// “G‚ª‹ŠE“à‚É“ü‚Á‚½ê‡(Raycast‚ª“G‚É’¼Ú“–‚½‚Á‚½ê‡)ˆÚ“®‚ğ~‚ßUŒ‚
/// </summary>
[RequireComponent(typeof(SearchEnemyOrder))]
public class AttackOrder : MonoBehaviour, ICommand
{
    [Header("QÆ")]
    NavMeshAgent _navMeshAgent;
    SearchEnemyOrder _searchEnemyOrder;
    EnemyBrain _enemyBrain;
    

    [Header("İ’è’l")]
    WaitForSeconds wait = new WaitForSeconds(1);

    void start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _searchEnemyOrder = GetComponent<SearchEnemyOrder>();
    }

    /// <summary>
    /// ˆø”‚Ì“G‚ğ’ÇÕ‚·‚é
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
    /// “G‚ª¶‚«‚Ä‚¢‚éŠÔw’è‚³‚ê‚½•b”ŒŸõ‚µ‘±‚¯‚é
    /// </summary>
    /// <returns></returns>
    IEnumerator Search()
    {
        while(_enemyBrain.Hp > 0)
        {
            //Raycast‚µA“G‚É“–‚½‚Á‚½‚çNavmesh‚Æ‚ß‚ÄËŒ‚
            yield return wait;
        }
    }
}
