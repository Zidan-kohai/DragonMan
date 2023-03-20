using System.Collections;
using UnityEngine;

public class EnemyMovement : Enemy
{
    [SerializeField] private float damage;
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;
    [SerializeField] private float speed;
    [SerializeField] private float rest;
    [SerializeField] private AudioClip diedSound;
    private Animator anim;
    private bool toleft;
    private bool isIdle;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!toleft && !isIdle)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
            if (transform.position.x > rightBound.position.x)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                toleft = true;
                StartCoroutine(Idle());

            }
        }
        else if(toleft && !isIdle)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            if (transform.position.x < leftBound.position.x)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                toleft = false;
                StartCoroutine(Idle());
            }
        }
    }

    private IEnumerator Idle()
    {
        isIdle = true;
        anim.SetBool("walk", false);
        yield return new WaitForSeconds(rest);
        isIdle = false;
        anim.SetBool("walk", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamege(damage);
        }
    }

    public override void TakeDamage(float _damage)
    {
        base.Health -= _damage;
        if (base.Health <= 0)
        {
            gameObject.SetActive(false);
            AudioManager.instanse.PlayAudio(diedSound);
        }
        anim.SetTrigger("hurt");
        
    }
}
