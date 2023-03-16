using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 周囲の敵を検索して壁越しに見えるシェーダーを適用する
/// </summary>
public class SearchEnemyOrder : MonoBehaviour, ICommand
{
    [Header("参照")]
    [SerializeField] Material _seeThrough0;
    [SerializeField] Material _seeThrough1;

    //見つけた敵のリスト
    List<EnemyBrain> _enemies = new();
    public List<EnemyBrain> Enemies { get => _enemies; set => _enemies = value; }

    public void Command(string[] arguments, GameObject bot)
    {
        //現状はとりあえずFindを使う。
        //Bytypeで行っているとはいえどこかでキャッシュしたものを取得するつもり
        var enemies = FindObjectsByType<EnemyBrain>(FindObjectsSortMode.None);
        ShowEnemy(enemies);
    }

    void ShowEnemy(EnemyBrain[] enemyBrains)
    {
        foreach (var enemy in enemyBrains)
        {
            if(!_enemies.Contains(enemy))
            {
                _enemies.Add(enemy);
            }
            var enemyMesh = enemy.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();

            enemyMesh.materials[0].shader = _seeThrough0.shader;
            enemyMesh.materials[1].shader = _seeThrough1.shader;
        }
    }
}
