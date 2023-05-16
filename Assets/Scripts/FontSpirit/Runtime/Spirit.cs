using FontSpirt.Attributes;
using TMPro;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace FontSpirit.Runtime
{
	[RequireComponent(typeof(TMP_Text))]
#if UNITY_EDITOR
	[ExecuteAlways]
#endif
	public class Spirit : MonoBehaviour
	{
		[SerializeField, HideInInspector] private TMP_Text _text;
		[SerializeField, ReadOnly] private TMP_FontAsset _font;
		[SerializeField, ReadOnly] private Material _sharedMaterial;

		[SerializeField, HideInInspector] private SerializedPropertyValue _fontSave;
		[SerializeField, HideInInspector] private SerializedPropertyValue _sharedMaterialSave;


		private void Start()
		{
			if (_text == null)
				_text = GetComponent<TMP_Text>();

			
		}

#if UNITY_EDITOR
		public void Save()
		{
			_fontSave = new(_font);
			_sharedMaterialSave = new(_sharedMaterial);

			_font = null;
			_sharedMaterial = null;

			_text.font = null;
			_text.fontSharedMaterial = null;
		}

		private void Update()
		{
			if (_text == null)
				_text = GetComponent<TMP_Text>();

			if (_font != _text.font)
				_font = _text.font;

			if (_sharedMaterial != _text.fontSharedMaterial)
				_sharedMaterial = _text.fontSharedMaterial;
		}
#endif
	}
}
