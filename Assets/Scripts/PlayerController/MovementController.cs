using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] CapsuleCollider col;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gravity = -9.81f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.up * gravity;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = transform.right * x + transform.forward * y;
        Vector3 movement = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
        rb.angularVelocity = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Space) && groundCheck())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool groundCheck()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayer);
    }
}
