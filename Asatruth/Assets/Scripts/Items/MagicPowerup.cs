﻿using UnityEngine;
using System.Collections;

public class MagicPowerup : Collectable {

    public int special = 4;
    public GameObject projectilePrefab;
    public PlayerSpecial SpecialSelect;
    public GameObject player;
    private Animator anim;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        SpecialSelect = player.GetComponent<PlayerSpecial>();
        anim = player.GetComponent<Animator>();

    }

    override protected void OnCollect(GameObject target) {
        anim.SetInteger("Special", special);

        SpecialSelect.specialSelect = 4;
    }

}
