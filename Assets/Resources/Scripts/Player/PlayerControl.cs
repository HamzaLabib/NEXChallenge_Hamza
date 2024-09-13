using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    #region Fields
    #region Visible Unity Fields
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float minimumRotationSpeed = 0.1f;
    [SerializeField] GameObject windowUI;
    #endregion

    #region Private Fields
    [HideInInspector] public bool isAlive;
    private bool isGrounded;
    private Rigidbody rb;
    private Camera mainCamera;
    #endregion
    #endregion

    #region Methods
    #region Unity Methods
    private void Awake()
    {
        isGrounded = true;
        isAlive = true;
        mainCamera = Camera.main;
        windowUI.gameObject.SetActive(false);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Condition to stop controlling when player die
        if (isAlive)
        {
            KeyboardMovement();
            RotateToMouseCursor();

            if (isGrounded)
                Jump();
        }
    }
    #endregion

    #region Movement Handling
    private void KeyboardMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * vertical + right * horizontal).normalized * speed;

        if (movement.magnitude > 0)
            rb.MovePosition(rb.position + movement * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    #endregion

    #region Rotation Handling by Mouse Cursor
    private void RotateToMouseCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 pointToLook = ray.GetPoint(rayLength);
            Vector3 directionToLook = pointToLook - transform.position;
            directionToLook.y = 0;

            if (directionToLook != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
                //Adjust rotation speed based on distance
                float dynamicRotationSpeed = Mathf.Max(minimumRotationSpeed, rotationSpeed * (directionToLook.magnitude / 5));
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, dynamicRotationSpeed * Time.deltaTime);
            }
        }
    }
    #endregion

    #region Jump Handling
    private void OnCollisionEnter(Collision other)
    {
        #region Handle Jumping
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        #endregion
    }
    #endregion
    #endregion
}
