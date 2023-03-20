using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    private bool died;
    public float currentHealth { get; private set; }
    private Animator anim;

    [Header("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Audio")]
    [SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip dieAudio;
    private void Awake()
    {
        currentHealth = startingHealth;  
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamege(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            AudioManager.instanse.PlayAudio(hurtAudio);
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!died)  
            {
                AudioManager.instanse.PlayAudio(dieAudio);
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                died = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.2f);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }

        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

}
