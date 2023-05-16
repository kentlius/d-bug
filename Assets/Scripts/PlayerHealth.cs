using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    public float invincibilityTime = 1f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false;
    public SpriteRenderer spriteRenderer;
    public GameObject gameOverMenuUI;
    public TextMeshProUGUI TMP_Text;
    public static bool GameIsOver = false;

    void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if (isInvincible)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

    public void TakeDamage(int damage, Collider2D collision)
    {
        if (!isInvincible)
        {
            lives -= damage;
            TMP_Text.text = "Lives: " + lives;
            if (lives <= 0)
            {
                Destroy(gameObject);
                // FindObjectOfType<GameManager>().GameOver();
                GameIsOver = true;
                gameOverMenuUI.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-400f, 200f));
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(400f, 200f));
                }

                StartCoroutine(InvincibilityFlash());
            }
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1, collision);
        }
        if (collision.gameObject.CompareTag("Death"))
        {
            TakeDamage(3, collision);
        }
    }
}
