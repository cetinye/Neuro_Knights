using DG.Tweening;
using UnityEngine;

namespace Neuro_Knights
{
	public class AnimationComponent : MonoBehaviour
	{
		public Sequence walkSeq;
		private SpriteRenderer spriteRenderer;
		private float walkInitialScale;
		private float walkEndScale;
		private float xScaleTime;
		private float yScaleTime;

		public void Initialize(SpriteRenderer spriteRenderer, float xScaleAmount, float xScaleTime, float yScaleTime)
		{
			this.spriteRenderer = spriteRenderer;
			walkInitialScale = spriteRenderer.transform.localScale.x;
			walkEndScale = spriteRenderer.transform.localScale.x + xScaleAmount;
			this.xScaleTime = xScaleTime;
			this.yScaleTime = yScaleTime;

			WalkAnim();
		}

		public void WalkAnim()
		{
			walkSeq = DOTween.Sequence();
			walkSeq.Append(spriteRenderer.transform.DOScaleX(walkEndScale, xScaleTime));
			walkSeq.Join(spriteRenderer.transform.DOScaleY(walkInitialScale, yScaleTime));
			walkSeq.SetLoops(-1, LoopType.Yoyo);
		}
	}
}
