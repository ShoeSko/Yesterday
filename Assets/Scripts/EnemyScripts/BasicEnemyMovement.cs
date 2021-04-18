using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    public EnemyScript enemy;

    [Header("Enemy Controls")]
    private float moveSpeed;
    private int enemyHealth;
    private string obstacleTags;
    private string projectileTags;

    [Header("Enemy Attack")]
    private int attackDamage;
    private float attackSpeed;
    private LayerMask whatIsUnitLayer;

    [Tooltip("How big is the attack range?")][SerializeField]private float attackRange; //These two stay here as they are harder to do from a scriptableobject.
    [Tooltip("Where does it attack from?")][SerializeField]private Transform attackPosition;

    private bool obstacleInTheWay;//Is there a unit blocking the path?
    private Rigidbody2D rg2D;
    private bool hasAttacked; //Has it attacked, wait until ready again.
    private bool isRecharging; //Is it recharging because then you should not make another wait timer.
    private int quackDamage = 80;

    private void Start()
    {
        EnemyInfoFeed();
        rg2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        EnemyDeath();//Death comes for us all.
    }
    private void FixedUpdate()
    {
        MovingEnemy();//Currently both moving & attacking
    }
    void MovingEnemy()
    {
        if (!obstacleInTheWay)//If there are no obstacles the enemy will start moving
        {
            rg2D.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0); //Move to the right timed with deltatime for now, have to check build if change has to be done.
        }
        else { AttackObstacle(); } //If you have someone blocking your path, kill them.
    }

    private void OnCollisionEnter2D(Collision2D other) //Stops movement upon reaching a blockade
    {
        if (other.collider.CompareTag(obstacleTags)) //Is there an obstacle blocking your path.
        {
            obstacleInTheWay = true;
        }
        if (other.collider.CompareTag("InstaKill"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(projectileTags))
        {
            ProjectileScript projectileScript = other.gameObject.GetComponent<ProjectileScript>();
            enemyHealth = enemyHealth - projectileScript.projectileDamage;//Reads damage from the projectile script(Which reads it from their parent)
            projectileScript.numberOfMaxTargets--;
            StartCoroutine(PeriodOfBeingDamaged());
            ////Destroy(other.gameObject);//Current issue for later, bullet takes time to dissapear.
        }

        if (other.gameObject.tag == "Quack")
        {
            enemyHealth = enemyHealth - quackDamage;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other) //Restarts movement upon destroying the obstacle
    {
        if (other.collider.CompareTag(obstacleTags))
        {
            obstacleInTheWay = false;
        }        
    }
    
    private void AttackObstacle() //Attack time
    {
        if (!hasAttacked) //If you have yet to attack
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position,attackRange, whatIsUnitLayer); //Overlap of all Units withing the attack range
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if(enemiesToDamage[i])
                {
                    enemiesToDamage[i].GetComponent<UnitPrototypeScript>().TakeDamage(attackDamage); //Sent attackDamage to Unit
                    hasAttacked = true;
                }
            }
        }
        else if (hasAttacked && !isRecharging) { StartCoroutine(AttackRecharge()); } //Start Coroutine to recharge
    }

    private void OnDrawGizmosSelected() //Gizmo to represent attack range.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
    IEnumerator AttackRecharge() //Recharges the attack speed
    {
        isRecharging = true;
        yield return new WaitForSeconds(attackSpeed);
        hasAttacked = false;
        isRecharging = false;
        yield return null;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        print(enemyHealth);
        StartCoroutine(PeriodOfBeingDamaged());
    }
    IEnumerator PeriodOfBeingDamaged() //This entire thing can do whatever is put in here(Rotation is just a short representation.
    {
        Quaternion orgRot;
        orgRot = transform.rotation; //Retain the original rotational value

        transform.Rotate(0, 0, -10);
        yield return new WaitForSeconds(0.2f);
        transform.rotation = orgRot;
        print("Got hit");
        yield return null;
    }

    private void EnemyDeath()//If there is no more health, die.
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void EnemyInfoFeed()
    {
        moveSpeed = enemy.moveSpeed;
        enemyHealth = enemy.enemyHealth;
        obstacleTags = enemy.obstacleTags;
        projectileTags = enemy.projectileTags;
        attackDamage = enemy.attackDamage;
        attackSpeed = enemy.attackSpeed;
        whatIsUnitLayer = enemy.whatIsUnitLayer;
    }
}
