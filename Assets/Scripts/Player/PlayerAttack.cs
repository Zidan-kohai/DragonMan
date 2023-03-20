using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private PlayerMovement playerMovement;

    [SerializeField] private AudioClip fireBallSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();    
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCoolDown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        AudioManager.instanse.PlayAudio(fireBallSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireBalls[FindFireBall()].transform.position = firePoint.position;
        fireBalls[FindFireBall()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBall()
    {
        for(int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
