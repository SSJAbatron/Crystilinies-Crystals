using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //for movement 
    public float speed = 5.0f;
    private float moveInputHorizontal;
    private float moveInputVertical;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //for score
    public int score = 0;
    public GameObject highScoreUI;

    //for timer where the mermaid turns into human and dead
    public float timer = 15.0f; // Initial timer value
    public float maxTimer = 15.0f; // Maximum timer value
    public GameObject timerFill;
    public TMP_Text scoreText;

    //check for death condition
    bool isAlive = true;
    bool gameEnd = false;
    void Start()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateScoreText();
        UpdateTimerFill();
        AudioManager.instance.PlayMusic("BG");
    }

    void Update()
    {
        if (isAlive)
        {
            // Player movement
            moveInputHorizontal = Input.GetAxis("Horizontal");
            moveInputVertical = Input.GetAxis("Vertical");

            rb.velocity = new Vector2(moveInputHorizontal * speed, moveInputVertical * speed);

            // Flip the player's sprite based on horizontal direction
            if (moveInputHorizontal > 0)
            {
                //spriteRenderer.flipX = false;
                spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
                // moving down right
                if (moveInputVertical > 0)
                {
                    spriteRenderer.transform.localScale = new Vector3(1, -1, 1);
                }


            }
            else if (moveInputHorizontal < 0)
            {
                //spriteRenderer.flipX = true;
                spriteRenderer.transform.localScale = new Vector3(-1, 1, 1);
                // moving down left
                if (moveInputVertical > 0)
                {
                    spriteRenderer.transform.localScale = new Vector3(-1, -1, 1);
                }

            }
            //moving down straight
            else if (moveInputVertical > 0)
            {
                spriteRenderer.transform.localScale = new Vector3(1, -1, 1);
            }

            else if (moveInputVertical < 0)
            {
                spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
                moveInputVertical = 0f;
            }

        }
        // Timer countdown
        timer -= Time.deltaTime;
        UpdateTimerFill();

        // Check if timer is zero
        if (timer <= 0 && gameEnd == false)
        {
            timer = 0;
            isAlive = false;
            gameEnd = true;
            Die();
        }
    }
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        //score += 1;
        if (score < 10)
            scoreText.text = "Score: 00" + score.ToString();
        else if (score < 100)
            scoreText.text = "Score: 0" + score.ToString();
        else
            scoreText.text = "Score: " + score.ToString();
    }
    public void AddTime(float value)
    {
        timer += value;
        if (timer > maxTimer) timer = maxTimer; // Ensure the timer doesn't exceed the maximum value
        UpdateTimerFill();
    }

    public void SubtractTime(float value)
    {
        timer -= value;
        if (timer > maxTimer) timer = maxTimer; // Ensure the timer doesn't exceed the maximum value
        UpdateTimerFill();
    }

    void UpdateTimerFill()
    {
        float sliderValue = timer / maxTimer;
        timerFill.GetComponent<Slider>().value = sliderValue;
    }
    void Die()
    {
        // Handle player death (e.g., restart the level, show game over screen, etc.)
        AudioManager.instance.musicSource.Stop();
        animator.SetBool("IsDead", true);
        rb.gravityScale = 0f;
        AudioManager.instance.PlayDeathSound("Death");
        Invoke("StopTime", 2f);
        // You can add more logic here to handle the death scenario
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            AudioManager.instance.PlaySFX("Hurt");
            SubtractTime(3f);
        }
    }

    private void StopTime()
    {

        Time.timeScale = 0f;
        highScoreUI.SetActive(true);

    }
}

