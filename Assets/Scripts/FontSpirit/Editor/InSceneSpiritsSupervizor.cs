using System.Collections.Generic;
using FontSpirit.Runtime;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FontSpirt.Editor
{
	[InitializeOnLoad]
	public static class InSceneSpiritsSupervizor
	{
		private static int _objectsCount = 0;
		private static readonly List<GameObject> _rootBuffer = new();
		private static readonly List<Scene> _sceneBuffer = new();


		static InSceneSpiritsSupervizor()
		{
			EditorApplication.hierarchyChanged += OnHierarchyChanged;
			SceneManager.sceneLoaded += OnSceneLoaded;
			SceneManager.sceneUnloaded += OnSceneUnloaded;
			AddLoadedScenesToBuffer();
		}

		private static void OnHierarchyChanged()
		{
			int count = CountAllObjects();
			if (count != _objectsCount)
			{
				if (count > _objectsCount)
				{
					var texts = Object.FindObjectsOfType<TMP_Text>();

					foreach (var text in texts)
					{
						var spirit = text.GetComponent<Spirit>();
						if (spirit == null)
							text.gameObject.AddComponent<Spirit>();
					}
				}
				_objectsCount = count;
			}
		}

		private static void OnSceneLoaded(Scene loaded, LoadSceneMode mode)
		{
			if (!_sceneBuffer.Contains(loaded))
				_sceneBuffer.Add(loaded);
		}

		private static void OnSceneUnloaded(Scene unloaded)
		{
			if (_sceneBuffer.Contains(unloaded))
				_sceneBuffer.Remove(unloaded);
		}

		private static int CountAllObjects()
		{
			int count = 0;
			foreach (var scene in _sceneBuffer)
			{
				scene.GetRootGameObjects(_rootBuffer);
				for (int i = 0; i < _rootBuffer.Count; ++i)
				{
					GameObject gameObject = _rootBuffer[i];
					count += gameObject.transform.hierarchyCount;
				}
			}
			return count;
		}

		private static void AddLoadedScenesToBuffer()
		{
			_sceneBuffer.Clear();
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				var scene = SceneManager.GetSceneAt(i);
				_sceneBuffer.Add(scene);
			}
		}
	}
}