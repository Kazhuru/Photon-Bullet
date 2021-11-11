﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] private int damage = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }
}
