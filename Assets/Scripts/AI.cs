using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour, ICombat
{
    [SerializeField] float speed = 10;
    [SerializeField] float maxHealth = 4;
    [SerializeField] float damage = 1;
    private float currentHealth;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public Transform[] points;
    public bool isAlive { get; private set; }

    

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GoToNextPoint();
        currentHealth = maxHealth;
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

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
        
    }
    public void DealDamage(float damage)
    {
        
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy has {currentHealth} hp");
        if (currentHealth < 0)
        {
            Destroy(this.gameObject);
            isAlive = false;
        }
    }
}
