using FontSpirit.Runtime;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace FontSpirt.Editor
{
	public class SpirtAssetPostprocessor : AssetPostprocessor
	{
		private void OnPostprocessPrefab(GameObject gameObject)
		{
			var texts = gameObject.GetComponentsInChildren<TMP_Text>(true);
			foreach (var text in texts)
			{
				var spirit = text.GetComponent<Spirit>();
				if (spirit == null)
					text.gameObject.AddComponent<Spirit>();
			}
		}
	}
}