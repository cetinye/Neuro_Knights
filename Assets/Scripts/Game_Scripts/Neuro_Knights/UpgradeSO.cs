using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Neuro_Knights
{
	[CreateAssetMenu(menuName = "Neuro Knights/Upgrade", fileName = "Upgrade")]
	public class UpgradeSO : ScriptableObject
	{
		public Sprite cardImage;
		public StatType statType;
		public int value;
		public Sprite iconImage;

		public StatType GetStatType()
		{
			return statType;
		}

		public int GetValue()
		{
			return value;
		}
	}

	public enum StatType
	{
		MaxHP,
		HPRegeneration,
		LifeSteal,
		Damage,
		MeleeDamage,
		RangedDamage,
		ElementalDamge,
		AttackSpeed,
		CritChance,
		Engineering,
		Range,
		Armor,
		Dodge,
		Speed,
		Luck,
		Harvesting
	}
}
