using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float attackForce = 10f;
    private Camera mainCamera;
    #endregion

    #region Methods
    #region Unity Methods
    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    #endregion

    #region Attack Handling by Mouse Cursor
    private void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 shootDirection = (hit.point - transform.position).normalized;
            //Adjust the rotation of the bullet to face the direction of the shoot
            Quaternion bulletRotation = Quaternion.LookRotation(shootDirection);
            //Create a bullet and shoot it towards the hit point
            GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // Apply force to the bullet in the direction it is facing
            rb.AddForce(rb.transform.forward * attackForce, ForceMode.Impulse);

            Destroy(bullet, 2.0f);
        }
    }
    #endregion
    #endregion
}
