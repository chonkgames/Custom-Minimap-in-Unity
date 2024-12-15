using UnityEngine;

[CreateAssetMenu(fileName = "MapCategory", menuName = "Minimap/Category")]
public class MapCategory : ScriptableObject
{
	[SerializeField] private Sprite categoryIcon;
	public Sprite CategoryIcon => categoryIcon;

	[SerializeField] private Color categoryColor;
	public Color CategoryColor => categoryColor;
}
