using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    #region Fields
    [SerializeField] protected int maxHealth = 6;
    [SerializeField] protected int damage = 2;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected GameObject windowUI;
    [SerializeField] protected AudioSource gotShotSound;
    [SerializeField] protected AudioSource DeathSound;
    [SerializeField] protected Slider healthSlider;
    protected string BulletSource;
    #endregion

    #region Methods
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        windowUI.SetActive(false);
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }


    #region Handle Damage
    protected virtual void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            gotShotSound.Play();
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
            Die();
    }
    #endregion

    #region Handle Death & Activate End Game Menu
    protected virtual void Die()
    {
        gotShotSound.Stop();
        DeathSound.Play();
        Destroy(gameObject);
        windowUI.SetActive(true);
    }
    #endregion

    #region Collision Handle
    protected virtual void OnCollisionEnter(Collision other)
    {
        //Check if got shot by player bullet
        if (other.gameObject.CompareTag(BulletSource))
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
