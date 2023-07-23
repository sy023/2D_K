using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    public TextMeshProUGUI coinCountText, healthPointsText;

    public int CoinCount, healthCount = 100;

    public float moveSpeed;
    public Rigidbody2D rigidBody;

    private Vector2 playerMovementInput;

    // private GameObject StartScreen;

    // health points
    public int HealthPoints
    {
        get { return healthCount; }
        set
        {
            healthCount = Mathf.Clamp(value, 0, 100);
            UpdateHealthPointsText();
        }
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        coinCountText = GameObject.Find("CoinCountText").GetComponent<TextMeshProUGUI>();
        healthPointsText = GameObject.Find("HealthPointsText").GetComponent<TextMeshProUGUI>();
        UpdateCoinCountText();
        UpdateHealthPointsText();
    }

    // movement
    void Update()
    {
        anim.SetFloat("Horizontal", playerMovementInput.x);
        anim.SetFloat("Vertical", playerMovementInput.y);
        anim.SetFloat("Speed", playerMovementInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = playerMovementInput * moveSpeed;
    }

    private void OnMove(InputValue inputValue)
    {
        playerMovementInput = inputValue.Get<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Speed"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(BackToBaseSpeed());
        }
        else if (collision.CompareTag("Coins"))
        {
           CoinCount++;
           Destroy(collision.gameObject);
            UpdateCoinCountText();

            if (CoinCount == 10)
           {
                SceneManager.LoadScene("Win");
            }
        }
        else if (collision.CompareTag("Health"))
        {
            Destroy(collision.gameObject);
            HealthPoints += 20;
        }

    }
        IEnumerator BackToBaseSpeed()
    {

        yield return new WaitForSeconds(3);
        moveSpeed = 5;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Coins"))
    //    {
    //        CoinCount++;
    //        Destroy(collision.gameObject);
    //    }
    //}
    private void UpdateCoinCountText()
    {
        coinCountText.text = "Coins: " + CoinCount.ToString();
    }

    // Update the Player Health Points UI text
    private void UpdateHealthPointsText()
    {
        healthPointsText.text = "Health: " + HealthPoints.ToString();
        if (HealthPoints < 1)
        {
            SceneManager.LoadScene("Lose");
        }
    }

 }
