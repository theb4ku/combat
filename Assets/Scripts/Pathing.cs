using UnityEngine;
using UnityEngine.AI;

public class Pathing : MonoBehaviour
{
    private int destPoint = 0;
    private NavMeshAgent agent;
    public Transform[] points;
    private AI enemy;
    [SerializeField] float viewRadius = 5f;
    [SerializeField] float enemyViewAngle = 30f;
    [SerializeField] float chaseSpeed = 5f;
    [SerializeField] float maxDistance = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GoToNextPoint();
        enemy = GetComponent<AI>();
    }

    void Update()
    {

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();

        }
        Collider[] hits = Physics.OverlapSphere(transform.position, viewRadius);

        if (hits != null)
        {

            for (int i = 0; i < hits.Length; i++)
            {

                Player player = hits[i].GetComponent<Player>();
                if (player != null)
                {
                    // Debug.Log(hits[i].gameObject.name);
                    float angle = Vector3.Angle(transform.forward, hits[i].transform.position - transform.position);

                    if (angle < enemyViewAngle)
                    {
                        float distance = Vector3.Distance(player.transform.position, transform.position);
                        agent.speed = chaseSpeed;
                        agent.destination = player.transform.position;

                        Vector3 direction = player.transform.position - transform.position;
                        Quaternion lookRotation = Quaternion.LookRotation(direction);
                        transform.rotation = lookRotation;

                        if (distance <= maxDistance)
                        {
                            agent.isStopped = true;

                            enemy.DealDamageTo(player);
                        }
                        else
                        {
                            agent.isStopped = false;
                        }
                    }
                    else
                    {
                        agent.isStopped = false;
                        agent.speed = 3f;
                        agent.destination = points[destPoint].position;
                    }
                }

            }

        }

    }
    void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }
}
