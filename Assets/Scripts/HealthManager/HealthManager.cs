using UnityEngine;

public class HealthManager : MonoBehaviour
{
	[SerializeField] float health = 100f;
    [SerializeField] Behaviour[] switchOnDeath;
    private bool dead = false;

	public void TakeDamage(float amount)
	{
		if (dead)
			return;
		health = Mathf.Clamp(health - amount, 0, 100);
		if (health == 0)
			Die();
	}
	void Die()
	{
		dead = true;
		GetComponent<Rigidbody>().isKinematic = true;
		Destroy(gameObject, 4f);
		foreach (Behaviour b in switchOnDeath)
		{
			b.enabled = !b.enabled;
			if (b.GetType().ToString() == "UnityEngine.Camera")
            {
				b.GetComponent<AudioListener>().enabled = b.enabled;
			}
		}

		try
		{
			GetComponent<Animator>().SetTrigger("Dead");
		}
		catch
		{
			Debug.Log("No Animator found");
		}
	}
}
