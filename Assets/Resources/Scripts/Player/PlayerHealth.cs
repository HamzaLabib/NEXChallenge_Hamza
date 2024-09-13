using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthControl
{
    #region Fields
    [SerializeField] Transform enemy;
    #endregion

    #region Methods
    protected override void Awake()
    {
        base.Awake();
        BulletSource = "EnemyBullet";
    }

    protected override void Die()
    {
        base.Die();
        enemy.GetComponent<EnemyControl>().isAlive = false;
        this.GetComponent<PlayerAttack>().enabled = false;
        this.GetComponent<PlayerControl>().isAlive = false;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
    #endregion
}
