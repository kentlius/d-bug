using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    public float invincibilityTime = 1f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false;
    public SpriteRenderer spriteRenderer;
    public GameObject gameOverScreen;
    public TextMeshProUGUI TMP_Text;

    void Update()
    {
        if (isInvincible)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
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
                gameOverScreen.SetActive(true);
                Time.timeScale = 0;
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
    }
}
