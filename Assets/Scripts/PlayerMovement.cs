using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(StartMovement(2));
    }

    private void Update()
    {
        if (GameManager.instance.needUpdatePath)
            StartCoroutine(StartMovement(0));
    }

    private IEnumerator StartMovement(int second)
    {
        yield return new WaitForSeconds(second);
        
        _navMeshAgent.SetDestination(target.position);
    }
}
