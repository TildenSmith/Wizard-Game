﻿using UnityEngine;

public class wizardAttack : MonoBehaviour
{
    
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Vector2 offset;
    public Transform player;
    private Vector2 player2;
    public SpriteRenderer playerSP;
    public Animator attackAnim;
    bool isAnimating;
    public float damage = 1f;
    public GameObject enemy;
    public Transform summonPoint;
    public GameObject playerObj;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("l"))
        {
            Instantiate(enemy, summonPoint.position, summonPoint.rotation);
        }

        if (this.attackAnim.GetCurrentAnimatorStateInfo(0).IsTag("1"))
        {
            isAnimating = true;
        }
        else
        {
            isAnimating = false;
        }

        if (playerSP.flipX && !Input.GetKey("w") && !Input.GetKey("s") && !isAnimating)
        {
            attackPoint.position = player2 - offset;
            attackPoint.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if(!Input.GetKey("w") && !Input.GetKey("s") && !isAnimating)
        {
            attackPoint.position = player2 + offset;
            attackPoint.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        
        player2 = player.position;

        if (Input.GetKey("j") && !isAnimating)
        {
            Attack();
        }

        if (Input.GetKey("w") && !playerSP.flipX && !Input.GetKey("s") && !isAnimating)
        {
            attackPoint.position = player2 + new Vector2(0, .8f);
            attackPoint.transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        else if (Input.GetKey("w") && playerSP.flipX && !Input.GetKey("s") && !isAnimating)
        {
            attackPoint.position = player2 + new Vector2(0, .8f);
            attackPoint.transform.eulerAngles = new Vector3(0f, 180f, 90f);
        }

        if (Input.GetKey("s") && !playerSP.flipX && !Input.GetKey("w") && !isAnimating && !playerObj.GetComponent<playerMove>().isGrounded)
        {
            attackPoint.position = player2 + new Vector2(-0.1f, -0.8f);
            attackPoint.transform.eulerAngles = new Vector3(0f, 0f, -90f);
        }
        else if (Input.GetKey("s") && playerSP.flipX && !Input.GetKey("w") && !isAnimating && !playerObj.GetComponent<playerMove>().isGrounded)
        {
            attackPoint.position = player2 + new Vector2(0.1f, -0.8f);
            attackPoint.transform.eulerAngles = new Vector3(0f, 180f, -90f);
        }
    }

    void Attack()
    {
        

        Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            hitEnemies[i].GetComponent<enemyAI>().TakeDamage(damage);
        }

    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }

}