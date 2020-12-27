using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,ICombat
{
    [SerializeField] float maxHealth = 10;
    [SerializeField] float damage = 2;
    private float currentHealth;
    [SerializeField] Hitbox capsule;
  
    List<ICombat> currentEnemies;

    public bool isAlive => currentHealth > 0;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemies = new List<ICombat>();
        currentHealth = maxHealth;
        capsule.OnEnemyListUpdate += GetCurrentEnemiesList;
    }

    public void GetCurrentEnemiesList()
    {
        currentEnemies = capsule.SubscribedEnemies;
    }

    // Update is called once per frame
   void Update()
   {
       
       if (Input.GetMouseButtonDown(0))
       {
          if (currentEnemies != null)
           {
               foreach(var item in currentEnemies)
               {
                   item.TakeDamage(damage);

               }
           }
       }
      
   }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy dealt {damage} dmg, player's health: {currentHealth}");
    }

    public void DealDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
    
}
