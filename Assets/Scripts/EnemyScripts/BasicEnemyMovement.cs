using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    #region Variables
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
    private float knockbackPower; //Grabbing a refrence of the knockback Strenght
    private int quackDamage = 80;

    [Header("Spell card effects")]
    private float moveSpeedSave;
    private bool isBeingPreventedFromDoingAnything; //Prevents actions
    private bool isBeingPreventedFromMoving; //Prevents Movement
    private bool isBeingPreventedFromAttacking; //Prevents attack


    [Header("Harm Effect Variables")]
    private Animator animatorOfEnemies; //The Animator for the enemies change in apperance during damaged periods.
    private float healthSave; //A place to store the original health pool.
    #endregion
    #region Standard Voids
    private void Start()
    {
        EnemyInfoFeed();
        rg2D = GetComponent<Rigidbody2D>();
        animatorOfEnemies = GetComponent<Animator>();
        healthSave = enemyHealth; //Saves the max health of the unit.
    }

    private void Update()
    {
        EnemyDeath();//Death comes for us all.
        CurrentDamageTaken(); //Calculates damage taken, activates the appropriate animation.
    }
    private void FixedUpdate()
    {
        if (isBeingPreventedFromMoving) //Allows attack during root.
        {
            if (!isBeingPreventedFromAttacking) { AttackObstacle(); }
        }
        else
        {
            if (!isBeingPreventedFromDoingAnything) { MovingEnemy(); } //Currently both moving & attacking
        }
    }
    #endregion
    #region Movement
    void MovingEnemy()
    {
        if (!obstacleInTheWay)//If there are no obstacles the enemy will start moving
        {
            rg2D.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0); //Move to the right timed with deltatime for now, have to check build if change has to be done.
        }
        else { if (!isBeingPreventedFromAttacking) { AttackObstacle(); } } //If you have someone blocking your path, kill them.
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
    private void OnCollisionExit2D(Collision2D other) //Restarts movement upon destroying the obstacle
    {
        if (other.collider.CompareTag(obstacleTags))
        {
            obstacleInTheWay = false;
        }        
    }
    #endregion
    #region Attack
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
    #endregion
    #region Take Damage

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

    public void TakeDamage(int damage, bool isKnockback, float knockbackStrenght)
    {
        enemyHealth -= damage;
        print(enemyHealth);
        if (!isKnockback)
        {
        StartCoroutine(PeriodOfBeingDamaged());
        }
        else if (isKnockback)
        {
            knockbackPower = knockbackStrenght;
            StartCoroutine(PeriodOfBeingDamagedWithKnockback());
        }
    }
    IEnumerator PeriodOfBeingDamaged() //This entire thing can do whatever is put in here(Rotation is just a short representation.
    {
        Quaternion orgRot;
        orgRot = transform.rotation; //Retain the original rotational value

        transform.Rotate(0, 0, -10);//Tilts the figure on hit,,, to note Will change their attack area.
        yield return new WaitForSeconds(0.2f);
        transform.rotation = orgRot;
        //print("Got hit");
        yield return null;
    }
    IEnumerator PeriodOfBeingDamagedWithKnockback() //This entire thing can do whatever is put in here(Rotation is just a short representation.
    {
        isBeingPreventedFromDoingAnything = true;
        Quaternion orgRot;
        orgRot = transform.rotation; //Retain the original rotational value

        transform.Rotate(0, 0, -10);
        rg2D.AddForce(new Vector2(knockbackPower, 0)); //Does the knockback
        yield return new WaitForSeconds(1); //Dictates how long the knockback takes effect
        transform.rotation = orgRot;
        //print("Got hit");


        isBeingPreventedFromDoingAnything = false; //No longer being knockedback
        yield return null;
    }
    private void EnemyDeath()//If there is no more health, die.
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CurrentDamageTaken()
    {
        float currentDamage;

        currentDamage = (enemyHealth / healthSave) * 100;
        //print("The " + gameObject.name + " has takken " + currentDamage + " damage.");
        animatorOfEnemies.SetFloat("DamageTaken", currentDamage);
    }

    #endregion
    #region Spell effects

    public void Slow(float slowDebuff,float debuffTime)
    {
        StartCoroutine(SlowedTime(slowDebuff, debuffTime));
    }
    IEnumerator SlowedTime(float slowStrength, float waitTime)
    {
        moveSpeedSave = moveSpeed; //Stores speed
        moveSpeed = moveSpeed - (moveSpeed * slowStrength); //Calculates how much slower %
        yield return new WaitForSeconds(waitTime); // Debuff lenght
        moveSpeed = moveSpeedSave; //Resets speed
        yield return null;
    }
    public void Stun(float debuffTime)
    {
        StartCoroutine(StunTime(debuffTime));
    }
    IEnumerator StunTime(float waitTime)
    {
        isBeingPreventedFromDoingAnything = true;
        yield return new WaitForSeconds(waitTime);
        isBeingPreventedFromDoingAnything = false;
        yield return null;
    }
    public void Root(float debuffTime)
    {
        StartCoroutine(RootTime(debuffTime));
    }
    IEnumerator RootTime(float waitTime)
    {
        isBeingPreventedFromMoving = true;
        yield return new WaitForSeconds(waitTime);
        isBeingPreventedFromMoving = false;
        yield return null;
    }
    public void Pacify(float debuffTime)
    {
        StartCoroutine(PacifyTime(debuffTime));
    }
    IEnumerator PacifyTime(float waitTime)
    {
        isBeingPreventedFromAttacking = true;
        yield return new WaitForSeconds(waitTime);
        isBeingPreventedFromAttacking = false;
        yield return null;
    }
    public void Harm(int damage)
    {
        enemyHealth -= damage; //Deal damage straight to the core
        StartCoroutine(PeriodOfBeingDamaged());
    }
    #endregion

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
