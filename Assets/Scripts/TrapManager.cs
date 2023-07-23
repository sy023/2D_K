using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public Animator anim;
    public int TrapDamage;

    // player
    public PlayerMovement player;
    public bool isPlayerOnTop;

    // trap 
    private bool isActive = false;

    public float trapAnimationDuration;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = true;

            if (!isActive)
            {
                isActive = true;
                StartCoroutine(TriggerTrap());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = false;
        }
    }

    // damage
    private IEnumerator TriggerTrap()
    {
        anim.SetBool("isActive", true);

        yield return new WaitForSeconds(trapAnimationDuration);

        anim.SetBool("isActive", false);
        isActive = false;

        if (isPlayerOnTop)
        {
            player.HealthPoints = Mathf.Max(player.HealthPoints - TrapDamage, 0);
        }
    }
}