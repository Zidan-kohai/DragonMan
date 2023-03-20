
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float Health;
    public virtual void TakeDamage(float _damage)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
