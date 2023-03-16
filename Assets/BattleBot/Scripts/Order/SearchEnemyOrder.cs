using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���͂̓G���������ĕǉz���Ɍ�����V�F�[�_�[��K�p����
/// </summary>
public class SearchEnemyOrder : MonoBehaviour, ICommand
{
    [Header("�Q��")]
    [SerializeField] Material _seeThrough0;
    [SerializeField] Material _seeThrough1;

    //�������G�̃��X�g
    List<EnemyBrain> _enemies = new();
    public List<EnemyBrain> Enemies { get => _enemies; set => _enemies = value; }

    public void Command(string[] arguments, GameObject bot)
    {
        //����͂Ƃ肠����Find���g���B
        //Bytype�ōs���Ă���Ƃ͂����ǂ����ŃL���b�V���������̂��擾�������
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
