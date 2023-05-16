namespace FontSpirit.Runtime
{
	public class SpiritAPI
	{
		public static SpiritAPI Instance
		{
			get
			{
				if (_instance == null)
					_instance = new SpiritAPI();
				return _instance;
			}
		}
		private static SpiritAPI _instance;


		private SpiritAPI()
		{
		}

		public T Load<T>(SerializedPropertyValue serializedPropertyValue) where T : UnityEngine.Object
		{
			return null;
		}
	}
}
