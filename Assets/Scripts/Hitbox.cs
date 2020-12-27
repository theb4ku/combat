using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public List<ICombat> SubscribedEnemies { get; private set; }
    public Action OnEnemyListUpdate;
    // Start is called before the first frame update
    void Start()
    {
        SubscribedEnemies = new List<ICombat>();
    }
    private void Update()
    {
        for (int i = SubscribedEnemies.Count - 1; i >= 0; i--)
        {
            if (SubscribedEnemies[i].isAlive == false)
            {
                SubscribedEnemies.Remove(SubscribedEnemies[i]);
                OnEnemyListUpdate?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ICombat tempEnemy = other.GetComponent<ICombat>(); 
        if(tempEnemy != null)
        {
           // Debug.Log($"Added: {other.gameObject.name}");
            SubscribedEnemies.Add(tempEnemy);
            OnEnemyListUpdate?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ICombat tempEnemy = other.GetComponent<ICombat>();
        if(tempEnemy != null && SubscribedEnemies.Contains(tempEnemy))
        {
            //Debug.Log($"unsubbed: {other.gameObject.name}");
            
            SubscribedEnemies.Remove(tempEnemy);
            OnEnemyListUpdate?.Invoke();
        }
    }
}
