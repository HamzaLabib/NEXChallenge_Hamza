using UnityEngine;

public class EnemyHealth : HealthControl
{
    #region Methods
    protected override void Awake()
    {
        base.Awake();
        BulletSource = "PlayerBullet";
    }

    protected override void Die()
    {
        base.Die();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
    #endregion
}
