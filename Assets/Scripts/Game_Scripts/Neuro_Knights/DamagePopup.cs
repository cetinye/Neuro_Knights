using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Neuro_Knights
{
	public class DamagePopup : MonoBehaviour
	{
		private static float minRange = -0.25f;
		private static float maxRange = 0.25f;
		[SerializeField] private TMP_Text damageText;
		private Sequence animSeq;

		public static DamagePopup Create(Vector3 position, float damage)
		{
			position = new Vector3(position.x + UnityEngine.Random.Range(minRange, maxRange), position.y + UnityEngine.Random.Range(minRange, maxRange), position.z);

			DamagePopup popup = Instantiate(LevelManager.instance.damagePopupPrefab, position, Quaternion.identity);
			popup.SetText(damage);
			popup.Anim();
			return popup;
		}

		public void SetText(float value)
		{
			damageText.text = value.ToString();
		}

		private void Anim()
		{
			animSeq = DOTween.Sequence();

			animSeq.Append(transform.DOScale(Vector3.one * 1.5f, 0.5f));
			animSeq.Append(damageText.DOFade(0, 0.5f));
		}
	}
}
