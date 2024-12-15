using UnityEngine;

[CreateAssetMenu(fileName = "MapEntityData", menuName = "Minimap/Entity Data")]
public class MapEntityData : ScriptableObject
{
	[SerializeField] private MapCategory category;
	public MapCategory Category => category;

	[SerializeField] private bool rotateWithTarget;
	public bool RotateWithTarget => rotateWithTarget;

	[SerializeField] private float rotateSpeed;
	public float RotateSpeed => rotateSpeed;
}
