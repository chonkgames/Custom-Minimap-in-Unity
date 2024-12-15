using UnityEngine;
using UnityEngine.UI;

public class MapIcon : MonoBehaviour
{
	#region Variables
	
	private MapEntity _entity;
	public MapEntity Entity => _entity;

	private Image _image;
	private RectTransform _rectTransform;
	private bool _rotateWithTarget;
	private float _rotateSpeed;
	
	#endregion
	
	#region Unity Methods
	
	private void Awake()
	{
		_image = GetComponent<Image>();
		_rectTransform = GetComponent<RectTransform>();
	}
	
	private void Update()
	{
		if (_rotateWithTarget)
		{
			var entityRotation = _entity.transform.eulerAngles.y;
			var targetRotation = Quaternion.Euler(0.0f, 0.0f, -entityRotation);

			_rectTransform.localRotation = Quaternion.RotateTowards(_rectTransform.localRotation, targetRotation, _rotateSpeed * Time.deltaTime);
		}
	}
	
	#endregion

	#region Custom Methods
	
	public void Init(MapEntity mapEntity)
	{
		_entity = mapEntity;
		_image.sprite = _entity.Data.Category.CategoryIcon;
		_image.color = _entity.Data.Category.CategoryColor;
		_rotateWithTarget = _entity.Data.RotateWithTarget;
		_rotateSpeed = _entity.Data.RotateSpeed;
	}

	public void SetPosition(Vector2 position)
	{
		_rectTransform.anchoredPosition = position;
	}

	public void SetRotation(float rotation)
	{
		if (_rotateWithTarget) return;
		_rectTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, -rotation);
	}
	
	#endregion
}
