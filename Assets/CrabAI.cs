using UnityEngine;

public class CrabAI : MonoBehaviour
{
    private Animator animator;
    private EnemyController enemy;

    private bool playerInRange;
    private GameObject player;

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<EnemyController>();
    }


    void Update()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");

        if (player == null)
            return;

        var wasInRange = playerInRange;

        playerInRange = Vector2.Distance(player.transform.position, transform.position) < Settings.CrabAttackPlayerDistance &&
                        player.transform.position.y > transform.position.y - .5f;

        if (wasInRange != playerInRange)
        {
            if (playerInRange)
            {
                enemy.stopMoving = true;
                animator.Play("Crab Attack");
            }
            else
            {
                enemy.stopMoving = false;
                animator.Play("Crab Walk");
            }
        }
    }
}