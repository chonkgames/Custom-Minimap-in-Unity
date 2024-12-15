using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
	#region Variables
	
	public static MapManager Instance;

	[SerializeField] private Transform target;

	[SerializeField] private GameObject iconPrefab;
	[SerializeField] private Transform iconParent;

	[SerializeField] private RectTransform mapImage;
	[SerializeField] private RectTransform maskImage;

	[SerializeField] private Transform boundsMin;
	[SerializeField] private Transform boundsMax;

	private List<MapIcon> _icons = new();

	private Camera _mainCamera;
	
	#endregion
	
	#region Unity Methods
	
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		_mainCamera = Camera.main;
	}

	private void Update()
	{
		var mapPosition = WorldToMapPosition(target.position);
		mapImage.anchoredPosition = -mapPosition;

		SetMapRotation();

		foreach (var icon in _icons)
		{
			var position = WorldToMapPosition(icon.Entity.transform.position);
			icon.SetPosition(position);

			var rotation = maskImage.localRotation.eulerAngles.z;
			icon.SetRotation(rotation);
		}
	}
	
	#endregion
	
	#region Custom Methods
	
	private void SetMapRotation()
	{
		maskImage.localRotation = Quaternion.Euler(0.0f, 0.0f, _mainCamera.transform.localRotation.eulerAngles.y);
	}

	public MapIcon RegisterMapEntity(MapEntity mapEntity)
	{
		var icon = Instantiate(iconPrefab, iconParent);
		if (!icon.TryGetComponent<MapIcon>(out var mapIcon))
		{
			Destroy(icon);
		}

		icon.name = $"Icon_{mapEntity.name}";
		mapIcon.Init(mapEntity);

		_icons.Add(mapIcon);

		return mapIcon;
	}

	public void UnregisterMapEntity(MapIcon mapIcon)
	{
		_icons.Remove(mapIcon);
	}

	private Vector2 WorldToMapPosition(Vector3 worldPosition)
	{
		var normalizedX = Mathf.InverseLerp(boundsMin.position.x, boundsMax.position.x, worldPosition.x);
		var normalizedY = Mathf.InverseLerp(boundsMin.position.z, boundsMax.position.z, worldPosition.z);

		var mapX = normalizedX * mapImage.sizeDelta.x - mapImage.sizeDelta.x / 2.0f;
		var mapY = normalizedY * mapImage.sizeDelta.y - mapImage.sizeDelta.y / 2.0f;

		return new Vector2(mapX, mapY);
	}
	
	#endregion
}
