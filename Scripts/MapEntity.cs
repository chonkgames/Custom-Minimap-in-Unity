using UnityEngine;

public class MapEntity : MonoBehaviour
{
	#region Variables

	[SerializeField] private MapEntityData data;
	public MapEntityData Data => data;

	private MapIcon _icon;

	#endregion
	
	#region Unity Methods
	
	private void Start()
	{
		_icon = MapManager.Instance.RegisterMapEntity(this);
	}

	private void OnDisable()
	{
		if (_icon != null)
		{
			MapManager.Instance.UnregisterMapEntity(_icon);
		}
	}
	
	#endregion
}
