using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Fields
    [SerializeField] int maxHealth = 6;
    [SerializeField] int damage = 2;
    [SerializeField] int currentHealth;
    [SerializeField] Canvas windowUI;
    [SerializeField] AudioSource gotShotSound;
    [SerializeField] AudioSource DeathSound;
    #endregion

    #region Methods
    private void Awake()
    {
        currentHealth = maxHealth;
        windowUI.gameObject.SetActive(false);
    }

    #region Handle Damage
    private void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            gotShotSound.Play();
            currentHealth -= damage;
        }

        if (currentHealth <= 0)
        {
            gotShotSound.Stop();
            DeathSound.Play();
            Die();
        }
    }
    #endregion

    #region Handle Death & Activate End Game Menu
    private void Die()
    {
        Destroy(gameObject);
        windowUI.gameObject.SetActive(true);
    }
    #endregion

    #region Collision Handle
    private void OnCollisionEnter(Collision other)
    {
        //Check if got shot by player bullet
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(damage);
        }
        //Check if enemy fall down
        if (other.gameObject.CompareTag("Finish"))
        {
            Die();
        }
    }
    #endregion
    #endregion
}
