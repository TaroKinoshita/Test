using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    Animator animator;
    public Collider weaponCollider;
    int hp = 100;
    public int maxHp = 100;
    public EnemyUIManager enemyUIManager;

    public GameObject gameClearText;

    void Start()
    {
        hp = maxHp;
        enemyUIManager.Init(this);

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        HideColliderWeapon();
    }

    
    void Update()
    {
        agent.destination = target.position;
        animator.SetFloat("Distance", agent.remainingDistance);
        
    }

    public void LookAtTarget()
    {
        transform.LookAt(target);
    }

    public void HideColliderWeapon()
    {
        weaponCollider.enabled = false;
    }

    public void ShowColliderWeapon()
    {
        weaponCollider.enabled = true;
    }
    void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            animator.SetTrigger("Die");
            Destroy(gameObject, 2f);
            gameClearText.SetActive(true);
        }
        Debug.Log("Enemy"+ hp);
        enemyUIManager.UpdateHP(hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        Damager damager = other.GetComponent<Damager>();
        if(damager != null)
        {
            Debug.Log("敵はダメージ");
            animator.SetTrigger("Hurt");
            Damage(damager.damage);
        }
    }
}
