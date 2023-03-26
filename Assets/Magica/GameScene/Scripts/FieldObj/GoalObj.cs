using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalObj : MonoBehaviour
{
    [SerializeField] string _nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMoveController>())
        {
            SceneManager.LoadScene(_nextSceneName);
        }
    }
}
