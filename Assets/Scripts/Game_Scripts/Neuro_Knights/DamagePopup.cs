using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Neuro_Knights
{
	public class DamagePopup : MonoBehaviour
	{
		[SerializeField] private TMP_Text damageText;
		private Sequence animSeq;

		public static DamagePopup Create(Vector3 position, float damage)
		{
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
