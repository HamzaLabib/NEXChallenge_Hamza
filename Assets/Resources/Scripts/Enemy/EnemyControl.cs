using UnityEngine;

enum EnemyStats { Wander, Attacking, CoolDown }

public class EnemyControl : MonoBehaviour
{
    #region Fields
    [SerializeField] float speed = 3.0f;
    [SerializeField] float rotationSpeed = 5f; //Smooth Rotation
    [SerializeField] float attackForce = 20f;
    [SerializeField] float attackRange = 4f; //Distance point to Start Attacking
    [SerializeField] float attackCooldown = 4f; //Cooldown duration between attacks
    [SerializeField] Transform target;
    [SerializeField] GameObject bulletPrefab;
    
    private float lastAttackTime = 0; //Time since last attack
    private float distanceToTarget; //To Decide Attack or chase
    private Quaternion targetRotation; //Facing to Target
    private Vector3 direction; //Direction to the target to move towards it
    [HideInInspector] public bool isAlive; //To Stop run Enemy Logic When Player died
    #endregion

    #region Methods
    #region Unity Methods
    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/EnemyBullet");
        isAlive = true;
    }
    private void Update()
    {
        distanceToTarget = direction.magnitude;
        if (isAlive)
            FindTaget();
    }
    #endregion

    // We can use Navmesh instead, easier and faster implementation but this one shows more deep coding skills
    #region Movement Hndle
    private void FindTaget()
    {
        direction = target.position - transform.position;

        targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Apply the target rotation to the player character.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Move();
    }
    #endregion

    #region Movement
    private void Move()
    {
        if (distanceToTarget > attackRange)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
        else
            CoolDown();
    }
    #endregion

    #region Handle Attack
    private void CoolDown()
    {
        if (Time.time > lastAttackTime + attackCooldown)
            Attack();
    }
    #endregion

    #region Attack
    private void Attack()
    {
        lastAttackTime = Time.time;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * attackForce, ForceMode.Impulse);
        Destroy(bullet, 1f);
    }
    #endregion
    #endregion
}
