using System;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace FontSpirit.Runtime
{
	[Serializable]
	public struct SerializedPropertyValue
	{
		[SerializeField] private string _name;
		[SerializeField] private int _id;

		public string Name => _name;
		public int Id => _id;


        public SerializedPropertyValue(UnityEngine.Object value)
        {
			_name = value.name;
			_id = value.GetInstanceID();
        }
    }
}
