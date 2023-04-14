using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IAEnemiga : MonoBehaviour
{

    public NavMeshAgent Agent;

    public Transform player;

    public LayerMask WhatIsGround, WhatIsPlayer;

    public Vector3 WalkPoint;

    bool WalkPointSet;

    public float WalkpointRange;

    public float TimeBetweenShots;

    public GameObject ProjectilePrefab;

    bool Attacked;

    public float sightRange, AttackRange;

    public bool PlayersInSightRange, PlayersInAttackRange;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        PlayersInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        PlayersInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        if (!PlayersInSightRange && !PlayersInAttackRange)
            Movementrange();
        if (PlayersInSightRange && !PlayersInAttackRange)
            Chase();
        if (PlayersInAttackRange && PlayersInSightRange)
            Attack();
    }

    private void Movementrange()
    {
        if (!WalkPointSet)
            Searchwalkpoint();

        if (WalkPointSet)
            Agent.SetDestination(WalkPoint);

        Vector3 distanceToWalkPoint = transform.position - WalkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            WalkPointSet = false;
    }

    void Searchwalkpoint()
    {
        float RandomZ = Random.Range(-WalkpointRange, WalkpointRange);
        float RandomX = Random.Range(-WalkpointRange, WalkpointRange);

        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ) ;

        if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
            WalkPointSet = true;

    }

    void Chase()
    {
        Agent.SetDestination(player.position);
    }

    void Attack()
    {
        Agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!Attacked)
        {
            Rigidbody rigidbody = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rigidbody.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rigidbody.AddForce(transform.up * 8f, ForceMode.Impulse);


            Attacked = true;
            Invoke(nameof(ResetAttack), TimeBetweenShots);
        }
    }

    private void ResetAttack()
    {
        Attacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
