using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    // Add NaveMeshAgent Var
    NavMeshAgent agentComponent;

    // Add Things To Chase
    [SerializeReference]
    Transform thingsToChase;

    private void Awake()
    {
        agentComponent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (thingsToChase != null)
        {
            agentComponent.SetDestination(GameObject.Find("Player(Clone)").transform.position);
        }
    }
}
