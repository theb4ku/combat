using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{
    void TakeDamage(float damage);
    void DealDamage(float damage);
    bool isAlive { get; }
}
