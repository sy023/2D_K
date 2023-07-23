using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    public bool Health, Speed;
    public int HealthRegen, SpeedBoost, Duration;
    private float basemovespeed;
    public PlayerMovement player;
    private bool speedBoostApplied = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Speed || !speedBoostApplied)
        {
            player.moveSpeed += SpeedBoost;
            speedBoostApplied = true;
            StartCoroutine(BackToBaseSpeed());
        }
        if (Health)
        {
            player.HealthPoints += HealthRegen;

            // Limit player's health to 100
            if (player.HealthPoints > 100)
            {
                player.HealthPoints = 100;
            }
        }
    }

    IEnumerator BackToBaseSpeed()
    {

        yield return new WaitForSeconds(3);
        player.moveSpeed = basemovespeed;
        speedBoostApplied = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        basemovespeed = player.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }
}