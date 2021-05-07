using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(Animator))] //This should have been on most script before.
public class GreedyOpportunity : MonoBehaviour
{
    #region Variables
    [SerializeField] private GreedyOpportunityStorage Storage;

    [Header("Hand Controls")]
    private float moveSpeed;
    private int handHealth;
    private string obstacleTags;
    private string projectileTags;

    [Header("Hand Attack")]
    [Tooltip("How long before greed strikes and the hand grabs the unit infront.")]
    private float timeBeforeGreed;
    private float timerForGreed;
    private bool canGreedStrike;
    private bool isRetreating;

    [HideInInspector] public bool obstacleInTheWay = true;//Is there a unit blocking the path?
    private bool isUnitInFront;
    private Rigidbody2D rg2D;
    private float knockbackPower = 0; //If the hand could be knocked back, then it would be useless.
    private int quackDamage; //Make this changable?
    public GameObject CorporateBoss;

    [Header("Spell card effects")]
    private float moveSpeedSave;
    private bool isBeingPreventedFromDoingAnything; //Prevents actions
    private bool isBeingPreventedFromMoving; //Prevents Movement
    private bool isBeingPreventedFromAttacking; //Prevents attack

    [Header("Harm Effect Variables")]
    private Animator animatorOfHand; //The Animator for the enemies change in apperance during damaged periods.
    private float healthSave; //A place to store the original health pool.
    private bool isDead;
    #endregion
    #region Standard Voids
    private void Start()
    {
        HandInforFeed(); //Updates all stats
        rg2D = GetComponent<Rigidbody2D>();
        animatorOfHand = GetComponent<Animator>();
        healthSave = handHealth; //Saves the max health of the unit.
        canGreedStrike = false;
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
            if (!isBeingPreventedFromAttacking) { GreedStrikes(); }
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
        if (!obstacleInTheWay && !isRetreating)//If there are no obstacles the enemy will start moving
        {
            rg2D.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0); //Move to the right timed with deltatime for now, have to check build if change has to be done.
            gameObject.GetComponent<Collider2D>().enabled = true; //Makes sure the collider is on for fight
            timerForGreed = timeBeforeGreed; //Resets the greed Timer when moving forward.
        }
        else if(isRetreating) //Turns invinsible after killing. Returns home.
        {
            rg2D.velocity = new Vector2(moveSpeed * Time.deltaTime, 0); //Move to the left
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else { if (!isBeingPreventedFromAttacking) { GreedStrikes(); } } //If you have someone blocking your path, kill them.

        if (obstacleInTheWay)
        {
            rg2D.velocity = new Vector2(0, 0); //Stops movement
        }
    }

    private void OnCollisionEnter2D(Collision2D other) //Stops movement upon reaching a blockade
    {
        if (other.collider.CompareTag(obstacleTags)) //Is there an obstacle blocking your path.
        {
            isUnitInFront = true;
            obstacleInTheWay = true;
        }
        if (other.collider.CompareTag("InstaKill"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (canGreedStrike && other.collider.CompareTag(obstacleTags))
        {
            other.gameObject.GetComponent<UnitPrototypeScript>().HandDeath(); //Tells the unit it was killed, not just removed by scene.
            Destroy(other.gameObject); // Destroys enemy object.

            isRetreating = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag(obstacleTags))
        {
            isUnitInFront = false;
            obstacleInTheWay = false;
        }
    }

    private void OnBecameInvisible() // When the hand leave the camera after retreating, do this
    {
        if (isRetreating) //Is it retreating?
        {
            obstacleInTheWay = true; //In a way being blocked, Will not move anymore until this is changed.
            isRetreating = false; // No longer retreating
        }
    }

    #endregion
    #region Greed
    private void GreedStrikes()
    {
        if (obstacleInTheWay && isUnitInFront)
        {
            timerForGreed = timerForGreed- Time.deltaTime;
            //print(timerForGreed + (" is this now"));
        }

        if(timerForGreed <= 0 && isUnitInFront)
        {
            canGreedStrike = true;

            Collider2D collisionPiece = GetComponent<Collider2D>();
            collisionPiece.enabled = false;
            collisionPiece.enabled = true;
        }
    }
    #endregion
    #region Take Damage
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(projectileTags))
        {
            ProjectileScript projectileScript = other.gameObject.GetComponent<ProjectileScript>();
            handHealth = handHealth - projectileScript.projectileDamage;//Reads damage from the projectile script(Which reads it from their parent)
            projectileScript.numberOfMaxTargets--;
            //StartCoroutine(PeriodOfBeingDamaged());
        }

        if (other.gameObject.tag == "Quack")
        {
            handHealth = handHealth - quackDamage;
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage, bool isKnockback, float knockbackStrenght)
    {
        handHealth -= damage;
        if (!isKnockback)
        {
            //StartCoroutine(PeriodOfBeingDamaged());
        }
        else if (isKnockback)
        {
            knockbackPower = knockbackStrenght;
            //StartCoroutine(PeriodOfBeingDamagedWithKnockback());
        }
    }
    IEnumerator PeriodOfBeingDamaged() //This entire thing can do whatever is put in here(Rotation is just a short representation.
    {
        Quaternion orgRot;
        orgRot = transform.rotation; //Retain the original rotational value

        transform.Rotate(0, 0, -10);//Tilts the figure on hit,,, to note Will change their attack area.
        yield return new WaitForSeconds(0.2f);
        transform.rotation = orgRot;
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


        isBeingPreventedFromDoingAnything = false; //No longer being knockedback
        yield return null;
    }
    private void EnemyDeath()//If there is no more health, die.
    {
        if (handHealth <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    private void CurrentDamageTaken()
    {
        float currentDamage;

        currentDamage = (handHealth / healthSave) * 100;
        animatorOfHand.SetFloat("DamageTaken", currentDamage);
    }
    private void OnDestroy() //On death function ready to be used!
    {
        if (isDead) //A condition to preven scene loading to cause errors.
        {
        CorporateBoss.GetComponent<TheCorporate>().Health--;
        }
    }

    #endregion
    #region Spell effects

    public void Slow(float slowDebuff, float debuffTime)
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
        handHealth -= damage; //Deal damage straight to the core
        StartCoroutine(PeriodOfBeingDamaged());
    }
    #endregion

    private void HandInforFeed()
    {
        moveSpeed = Storage.moveSpeed;
        handHealth = Storage.handHealth;
        obstacleTags = Storage.obstacleTags;
        projectileTags = Storage.projectileTags;

        timeBeforeGreed = Storage.timeBeforeGreed;

        quackDamage = Storage.quackDamage;
    }
}
