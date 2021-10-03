using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    Moving,
    Idle,
    Knockback,
    Jump,
    MoveLinear,
}
public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float baseSpeed;
    public bool isGravityEnabled;
    public GameObject target;

    private Rigidbody2D rigidBody;

    private EnemyState enemyState = EnemyState.Moving;

    private float stateCancelTime = 0f;
    private float stateChangedTime;

    public float timeSinceLastChange = 0f;

    [SerializeField] float health;
    [SerializeField] float maxIFrames;
    private float iFrames;
    private bool invincible = false;

    void Start()
    {
        if(!target) target = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(-baseSpeed, 0);
        stateChangedTime = Time.realtimeSinceStartup;

        if(!isGravityEnabled) rigidBody.gravityScale = 0;

        iFrames = maxIFrames;
    }

    // Update is called once per frame
    void Update()
    {   
        // if(Input.GetKeyDown("space")){
        //     applyKnockback(new Vector2(2.5f, 5), 1f);
        // }

        // if(Input.GetMouseButtonDown(0)){
        //     Vector3 mousePos = Input.mousePosition;
        //     mousePos.z = Camera.main.nearClipPlane;
        //     Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        //     applyJump(new Vector2(worldPosition.x, worldPosition.y), 1);
        // }
        switch (enemyState) {
            case EnemyState.Moving: 
                rigidBody.AddForce( new Vector2(-baseSpeed - this.rigidBody.velocity.x, isGravityEnabled? 0 : -this.rigidBody.velocity.y), ForceMode2D.Impulse); 
                break;
            case EnemyState.Idle:
                Vector2 impulse = -this.rigidBody.velocity;
                if(isGravityEnabled) impulse.y = 0;
                rigidBody.AddForce(impulse, ForceMode2D.Impulse);
                break;
            case EnemyState.Knockback:
                if(stateCancelTime <= Time.realtimeSinceStartup) setState(EnemyState.Moving);
                break;
            case EnemyState.Jump:
                if(stateCancelTime <= Time.realtimeSinceStartup) setState(EnemyState.Moving);
                break;
            // case EnemyState.MoveLinear:

            //     if(stateCancelTime <= Time.realtimeSinceStartup) resetState();
            //     break;
        }

        timeSinceLastChange = Time.realtimeSinceStartup - stateChangedTime;

        //iframes
        if (invincible) iFrames -= Time.deltaTime;
        if(iFrames < 0)
        {
            invincible = false;
            iFrames = maxIFrames;
        }
    }

    public void setState(EnemyState s){
        enemyState = s;
        stateChangedTime = Time.realtimeSinceStartup;
    }

    public EnemyState getState(){
        return enemyState;
    }
    
    public void applyKnockback(Vector2 strength, float duration){
        if(enemyState == EnemyState.Knockback) return;

        rigidBody.AddForce(strength - rigidBody.velocity, ForceMode2D.Impulse);

        setState(EnemyState.Knockback);
        stateCancelTime = Time.realtimeSinceStartup + duration;
    }

    public void applyJump(Vector2 targetLocation, float duration){
        if(enemyState == EnemyState.Knockback) return;

        Vector2 currentPos = transform.position;
        float gravity = rigidBody.gravityScale;

        float dx = targetLocation.x - transform.position.x;

        float yVel = (1/duration) * (targetLocation.y - transform.position.y) + 9.8f * 0.5f * gravity * duration;
        float xVel = dx / duration;

        rigidBody.AddForce(new Vector2(xVel, yVel) - rigidBody.velocity, ForceMode2D.Impulse);

        stateCancelTime = Time.realtimeSinceStartup + duration;
        setState(EnemyState.Jump);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            invincible = true;
            applyKnockback(new Vector2(col.gameObject.GetComponent<ProjectileData>().getKnockback(), 0f), maxIFrames);
            health -= col.gameObject.GetComponent<ProjectileData>().getDamage();
            if (!col.gameObject.GetComponent<ProjectileData>().isLingering()) Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "Ground")
        {
            if (rigidBody.velocity.y < 0) rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            var groundY = col.gameObject.transform.position.y;
            var groundScaleY = col.gameObject.transform.localScale.y;
            var colliderSize = GetComponent<BoxCollider2D>().size.y * transform.localScale.y;
            transform.position = new Vector3(transform.position.x, groundY + groundScaleY / 2 + colliderSize / 2, transform.position.z);
        }
    }

    // public void moveLinear(Vector2 targetLocation, float duration){

    //     rigidBody.AddForce(targetLocation - transform.position)

    //     stateCancelTime = Time.realtimeSinceStartup + duration;
    //     enemyState = EnemyState.MoveLinear;
    // }


}