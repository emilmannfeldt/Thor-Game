﻿using UnityEngine;
using System.Collections;

public class PlayerAttack : AbstractBehavior {

    public bool attacking = false;
    private float attackTimer = 0;
    private float attackCD = 0.3f;
    public Collider2D attackTrigger;
    public PlayerHealth dead;
    private Animator anim;

    protected override void Awake() {
        anim = GetComponent<Animator>();
        attackTrigger.enabled = false;
        dead = GetComponent<PlayerHealth>();
        

    }

    void Update() {
            var canAttack = Input.GetKeyDown("z"); //inputState.GetButtonValue(inputButtons[0]);

            if (canAttack && !attacking) {
                attacking = true;
                attackTimer = attackCD;

                attackTrigger.enabled = true;

            }

            if (attacking) {
                if (attackTimer > 0) {
                    attackTimer -= Time.deltaTime;
                    ToggleScripts(false);

                }
                else {
                    attacking = false;
                    attackTrigger.enabled = false;
                    anim.SetInteger("AnimState", 0);
                    ToggleScripts(true);

                }
            }

            anim.SetBool("Attacking", attacking);
        }
    
}
