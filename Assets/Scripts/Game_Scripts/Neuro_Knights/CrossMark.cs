using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class CrossMark : MonoBehaviour
	{
		private SpriteRenderer spriteRenderer;
		private Tween fadeTween;
		private bool isInterrupted = false;

		void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out Player player))
			{
				fadeTween.Complete();
				isInterrupted = true;
			}
		}

		public Tween AnimSequence()
		{
			fadeTween = spriteRenderer.DOFade(0.5f, 0.5f).SetLoops(6, LoopType.Yoyo).OnComplete(() => gameObject.SetActive(false));
			return fadeTween;
		}

		public bool IsInterrupted()
		{
			return isInterrupted;
		}
	}
}
