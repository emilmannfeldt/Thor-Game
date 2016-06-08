using UnityEngine;
using System.Collections;
using System;

public class PlayerSpecial : AbstractBehavior {

    public int specialSelect = 0;

    
    public GameObject projectilePrefab;
    public Vector2 firePosition = Vector2.zero;
    public Color debugColor = Color.yellow;
    public float debugRadius = 3f;
    public GameObject player;
    public PlayerHealth heal;

    public bool shooting;
    public bool healing;
    public bool punching;
    public bool casting;

    private bool isShooting;
    public float shootDelay = .5f;
    public float RangePwrCap = 0.7f;
    public float RangePwr = 0f;
    private float timeElapsed = 0f;

    public bool attacking = false;
    private float attackTimer = 0;
    private float attackCD = 2f;
    public Collider2D SpecialMelee;

    

    override protected void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        heal = player.GetComponent<PlayerHealth>();
    }


    protected virtual void Shoot(bool value) {
        shooting = value;
        //ToggleScripts(!value);

        if (projectilePrefab != null) {

            var canFire = inputState.GetButtonValue(inputButtons[0]);

            if (isShooting == false) {
                timeElapsed = 0;
            }
            else {
                RangePwr = timeElapsed / RangePwrCap;
                    if (RangePwr > 1) {
                        RangePwr = 1;
                    }
            }
            
            if (canFire) {
                isShooting = true;                
            }else if(isShooting == true) {
                CreateProjectile(CalculateFirePosition());
                isShooting = false;
            }
            //if (canFire && timeElapsed > shootDelay) {                        
            //        CreateProjectile(CalculateFirePosition());
            //        timeElapsed = 0;
            //        shooting = true;
            //        ToggleScripts(false);
            //    }
            //    else {
            //    shooting = false;
            //    ToggleScripts(true);
            //}
            timeElapsed += Time.deltaTime;
          }
    }
    protected virtual void Heal(bool value) {
        //var canHeal = inputState.GetButtonValue(inputButtons[0]);

        //if (canHeal) {
        //    //heal.curHealth += 20;
        //}
    }
    protected virtual void Punch(bool value) {

        punching = value;
        SpecialMelee.enabled = value;

        var canAttack = inputState.GetButtonValue(inputButtons[0]);
        if (canAttack && !attacking) {
            attacking = true;
            attackTimer = attackCD;
        }

        if (attacking) {
            if (attackTimer > 0) {
                attackTimer -= Time.deltaTime;
                ToggleScripts(false);

            }
            else {
                attacking = false;
                ToggleScripts(true);
            }
        }
    }
    //protected virtual void Magic(bool value) {

    //}

    void Update () {
        
        switch (specialSelect) {
            case 1: //ranged

                var canShoot = inputState.GetButtonValue(inputButtons[0]);
                if (canShoot && !shooting) {
                     Shoot(true);
                }
                else if (shooting && !canShoot) {
                    Shoot(false);
                }

                break;
                
            case 2: //heal  
                //var canHeal = inputState.GetButtonValue(inputButtons[0]);
                //if (canHeal && !healing) {
                //    Heal(true);
                //}
                //else if (healing && !canHeal) {
                //    Heal(false);
                //}

                break;
            case 3: //punch
                var canPunch = inputState.GetButtonValue(inputButtons[0]);
                if (canPunch && !punching) {
                    Punch(true);
                }
                else if (punching && !canPunch) {
                    Punch(false);
                }

                break;
            case 4: //spell
                //var canCast = inputState.GetButtonValue(inputButtons[0]);
                //if (canCast && !casting) {
                //    Magic(true);
                //}
                //else if (casting && !canCast) {
                //    Magic(false);
                //}

                break;
            default: 
                break;
                
        }
	}


    Vector2 CalculateFirePosition() {
        var pos = firePosition;
        pos.x *= (float)inputState.direction;
        pos.x += transform.position.x;
        pos.y += transform.position.y;
        return pos;
    }


    public void CreateProjectile(Vector2 pos) {
        var clone = Instantiate(projectilePrefab, pos, Quaternion.identity) as GameObject;
        clone.transform.localScale = transform.localScale;
    }

    void OnDrawGizmos() {
        Gizmos.color = debugColor;

        var pos = firePosition;
        if (inputState != null)
            pos.x *= (float)inputState.direction;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        Gizmos.DrawWireSphere(pos, debugRadius);
    }
    
}
