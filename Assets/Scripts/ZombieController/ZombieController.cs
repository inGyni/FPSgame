using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] GameObject player;
    Rigidbody rb;
    Animator animator;
    public float speed = 0.5f;
    HealthManager playerHealthManager;
    bool dealDamage;
    [SerializeField] float damage = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        playerHealthManager = player.GetComponent<HealthManager>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (dealDamage && playerHealthManager)
            playerHealthManager.TakeDamage(damage);

        if (!player)
        {
            direction = Vector3.zero;
            return;
        }
        direction = player.transform.position - transform.position;
        direction.y = 0;
        rb.rotation = Quaternion.LookRotation(direction);
        rb.angularVelocity = Vector3.zero;
        rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);
        animator.SetFloat("MoveSpeed", 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dealDamage = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dealDamage = true;
        }
    }
}
