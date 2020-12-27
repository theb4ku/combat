using UnityEngine;
using System.Collections;
public class AI : MonoBehaviour, ICombat
{
    [SerializeField] float speed = 10;
    [SerializeField] float maxHealth = 4;
    [SerializeField] float damage = 2;
    [SerializeField] float cooldown = 2;
    private float currentHealth;
    private bool canAttack;
    
    public bool isAlive { get; private set; }

    void Start()
    {
        isAlive = true;
        currentHealth = maxHealth;
        canAttack = true;
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
    public void DealDamageTo(Player player)
    {
        if (canAttack)
        {
            player.TakeDamage(damage);
            StartCoroutine(SetCooldown());
        }
    }
    public IEnumerator SetCooldown()
    {
        canAttack = false;
        yield return  new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
