using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activateTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool activated;
    private void Awake()
    {
        anim = GetComponent<Animator>();    
        spriteRend = GetComponent<SpriteRenderer>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (activated)
            {
                collision.GetComponent<Health>().TakeDamege(damage);
            }
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        triggered = true;
        yield return new WaitForSeconds(activationDelay);
        activated = true;
        anim.SetBool("activated", true);
        yield return new WaitForSeconds(activateTime);
        activated = false;
        triggered = false;
        anim.SetBool("activated", false);
    }

}
