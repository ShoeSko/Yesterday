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
    private bool chosenByMom;

    [Header("Enemy confirmation for Animation")]
    [Tooltip("Is the Enemy Merry, so that her animation will play")] private bool isMerry;

    [Header("Enemy Index")]
    [Tooltip("Is it a beast?")] private bool isBeast;
    [Tooltip("Is it a humanoid?")] private bool isHumanoid;
    [Tooltip("Is it a monstrosity?")] private bool isMonstrosity;
    [Tooltip("What index number does it have?")] private int enemyIndex;

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
        MovingAnimation(); //If there is extra animation, activate it.

        if(MinigameSceneScript.Tutorial == false) //Will not update beastiary from Tutorial run
        {
            UnitForBeastiary(enemyIndex); //New enemy has spawned so they will be added to Beastieary (If the load a new scene.
        }
    }

    private void MovingAnimation()
    {
        if (isMerry)
        {
            animatorOfEnemies.SetBool("IsMerry", true);
        }
    }

    private void Update()
    {
        EnemyDeath();//Death comes for us all.
        CurrentDamageTaken(); //Calculates damage taken, activates the appropriate animation.

        //if (chosenByMom) //Potential use for making beasts & Monstrosities chosen by the Guardian to be seen in the dark. (Darkness needs work.
        //{
        //    ChildOfMothSeenInDark(); //Used to show the child of the Moth when darkness consumes
        //}
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
            rg2D.velocity = new Vector2(0, 0); //Stops movement
        }
        //if (other.collider.CompareTag("InstaKill"))
        //{

        //    other.gameObject.GetComponent<UnitPrototypeScript>().HandDeath(); //Tells the unit it was killed, not just removed by scene.
        //    Destroy(other.gameObject);
        //    Destroy(this.gameObject);
        //}
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
        orgRot = new Quaternion(0, 0, 0, 0); //Retain the original rotational value

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
    #region Beastiary storing
    private void UnitForBeastiary(int indexOfEnemy)
    {
        if (FindObjectOfType<SaveSystem>()) //Confirms that the save system is in the scene
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>(); //Finds the save system in the scene

            if (isBeast)
            {
                int indexLenght = saving.data.beastList.Length; //Aquires the lenght of the Array to store in.

                for (int index = 0; index < indexLenght; index++) //Runs a loop through the entire array until it reaches the same index as the enemy.
                {
                    if (index == indexOfEnemy) //If current unit is equal to the in the loop. (Make sure he goes from 0.39)
                    {
                        if (saving.data.beastList[index] == false)
                        {
                            saving.data.beastList[index] = true;

                            print(indexOfEnemy + " is the index of the Beast that was just chosen.");
                        }
                    }
                }
            }

            if (isHumanoid)
            {
                int indexLenght = saving.data.humanoidList.Length; //Aquires the lenght of the Array to store in.

                for (int index = 0; index < indexLenght; index++) //Runs a loop through the entire array until it reaches the same index as the enemy.
                {
                    if (index == indexOfEnemy) //If current unit is equal to the in the loop. (Make sure he goes from 0.39)
                    {
                        if (saving.data.humanoidList[index] == false)
                        {
                            saving.data.humanoidList[index] = true;

                            print(indexOfEnemy + " is the index of the Humanoid that was just chosen.");
                        }
                    }
                }
            }

            if (isMonstrosity)
            {
                int indexLenght = saving.data.monstrosityList.Length; //Aquires the lenght of the Array to store in.

                for (int index = 0; index < indexLenght; index++) //Runs a loop through the entire array until it reaches the same index as the enemy.
                {
                    if (index == indexOfEnemy) //If current unit is equal to the in the loop. (Make sure he goes from 0.39)
                    {
                        if (saving.data.monstrosityList[index] == false)
                        {
                            saving.data.monstrosityList[index] = true;

                            print(indexOfEnemy + " is the index of the Monstrosity that was just chosen.");
                        }
                    }
                }
            }
        }
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
    #region Boss Effects
    public void MotherlyEmbraceBuff()
    {
        //Buffing the Enemy
        enemyHealth += 20;
        enemyHealth = enemyHealth * 2;
        attackDamage = attackDamage * 2;

        //Activates the Moth outline
        for (int child = 0; child < transform.childCount; child++) //Loops trhough all the children(if multiple)
        {
            if (transform.GetChild(child).GetComponent<Animator>())//Finds a child with the animator.
            {
                transform.GetChild(child).GetComponent<Animator>().SetTrigger("Chosen"); //Turns the child on(Animator and all
            }
        }
        chosenByMom = true; //This child was chosen by mom
    }

    //private void ChildOfMothSeenInDark()
    //{
    //    if (transform.position.x > 0)
    //    {
    //        for (int child = 0; child < transform.childCount; child++) //Loops trhough all the children(if multiple)
    //        {
    //            if (transform.GetChild(child).GetComponent<Animator>())//Finds a child with the animator.
    //            {
    //                transform.GetChild(child).GetComponent<SpriteRenderer>().sortingOrder = 10; //Setts the order to high(In front of the darkness to show it.
    //            }
    //        }
    //    }
    //    else
    //    {
    //        for (int child = 0; child < transform.childCount; child++) //Loops trhough all the children(if multiple)
    //        {
    //            if (transform.GetChild(child).GetComponent<Animator>())//Finds a child with the animator.
    //            {
    //                transform.GetChild(child).GetComponent<SpriteRenderer>().sortingOrder = 0; //Outside of the dark, the outline will return to normal
    //            }
    //        }
    //    }

    //}
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

        //Enemy confirmation
        isMerry = enemy.isMerry;

        //Indexing
        isBeast = enemy.isBeast;
        isHumanoid = enemy.isHumanoid;
        isMonstrosity = enemy.isMonstrosity;
        enemyIndex = enemy.enemyIndex;
    }

    public void RatDebuff()//NOT TESTED, MIGHT CRASH 
    {
        float damageFloat;
        damageFloat = attackDamage;
        damageFloat = Mathf.Round(damageFloat * 0.9f);
        attackDamage = (int)damageFloat;

        Debug.Log("Chef Rat reduced the damage of " + enemy.name + "from " + Mathf.Round(attackDamage / 0.9f) + "to " + attackDamage);
    }
}
