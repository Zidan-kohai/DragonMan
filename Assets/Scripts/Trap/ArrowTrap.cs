using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attactCoolDown;
    [SerializeField] private Transform arrowPoint;
    [SerializeField] private GameObject[] arrow;
    private float coolDownTimer;

    private void Attack()
    {
        coolDownTimer = 0;
        int index = Findarrow();
        arrow[index].transform.position = arrowPoint.transform.position;
        arrow[index].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int Findarrow()
    {
        for (int i = 0; i < arrow.Length; i++)
        {
            if (!arrow[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;

    }
    private void Update()
    {
        if (coolDownTimer >= attactCoolDown)
        {
            Attack();
        }
        coolDownTimer += Time.deltaTime;
    }
}
