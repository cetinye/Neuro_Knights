using UnityEngine;

namespace Neuro_Knights
{
	public class Lifebox : MonoBehaviour
	{
		[SerializeField] private GameObject lifeboxGlowParticle;

		void Awake()
		{
			lifeboxGlowParticle.SetActive(true);
		}

		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.TryGetComponent(out Player player))
			{
				/// TODO: Player kademeli can artışı ve lifeboxPickUpParticle
				// player.AddLife();
				Destroy(gameObject);
			}
		}
	}
}
