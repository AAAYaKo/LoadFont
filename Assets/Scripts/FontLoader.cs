using UnityEngine;
using System;
using System.IO;
using TMPro;
using System.Collections.Generic;
using System.Text;

namespace Overmobile.Orcs
{
	[CreateAssetMenu(menuName = "Overmobile/FontLoader")]
	public class FontLoader : ScriptableObject
	{
		private static FontLoader s_Instance;

		[SerializeField] private Material mythicFontMaterial;
		
		[SerializeField] private Material legendaryFontMaterial;

		[SerializeField] private Material legendFontMaterial;

		[SerializeField] private Material normalFontMaterial;

		[SerializeField] private Material chatLegendaryFontMaterial;

		[SerializeField] private Material chatNormalFontMaterial;

		[SerializeField] private Material chatLegendFontMaterial;
		
		[SerializeField] private Material chatMythicFontMaterial;

		public Material GetMaterialForChat(int index)
		{
			return index switch
			{
				6 => chatMythicFontMaterial,
				5 => chatLegendFontMaterial,
				4 => chatLegendaryFontMaterial,
				_ => chatNormalFontMaterial,
			};
		}

		public Material GetMaterialForList(int index)
		{
			return index switch
			{
				6 => mythicFontMaterial,
				5 => legendFontMaterial,
				4 => legendaryFontMaterial,
				_ => normalFontMaterial,
			};
		}

		[SerializeField] private TMP_FontAsset[] listFonts;

		private Font GetFontByName(string fontName)
		{
			string[] fontPaths = Font.GetPathsToOSFonts();
			foreach (var path in fontPaths)
			{
				var font = new Font(path);
				if (font.name.Equals(fontName, StringComparison.OrdinalIgnoreCase))
					return font;

				if (font.fontNames != null)
				{
					foreach (var fn in font.fontNames)
					{
						if (fn.Equals(fontName, StringComparison.OrdinalIgnoreCase))
							return font;
					}
				}
			}

			return null;
		}

		private Font GetFontByFileName(string fileName, out string fontPath)
		{
			string[] fontPaths = Font.GetPathsToOSFonts();
			foreach (var path in fontPaths)
			{
				var fn = Path.GetFileNameWithoutExtension(path);
				if (fn.Equals(fileName, StringComparison.OrdinalIgnoreCase))
				{
					fontPath = path;
					return new Font(path);
				}
			}

			fontPath = null;
			return null;
		}

		private string[] androidFonts =
		{
			"ComingSoon",
			"Noto Sans CJK SC",
			"Noto Sans SC",
			"Noto Sans CJK TC",
			"Noto Sans TC",
			"Noto Sans CJK KR",
			"Noto Sans KR",
			"DroidSans"
		};

		private string[] iOSFonts =
		{
			"ArialMT",
		};

		private string[] windowsFonts =
		{
			"arial"
		};

		private void PrintAllFonts()
		{
			string[] fontPaths = Font.GetPathsToOSFonts();
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("System fonts:");

			Array.ForEach(fontPaths, path =>
			{
				var font = new Font(path);
				sb.Append(font.name);

				if (font.fontNames != null && font.fontNames.Length > 0)
				{
					sb.Append(" [");

					for (var i = 0; i < font.fontNames.Length; ++i)
					{
						sb.Append(font.fontNames[i]);
						if (i > 0)
							sb.Append(", ");
					}

					sb.Append("]");
				}

				sb.Append(": ");
				sb.AppendLine(path);
			});
			Debug.Log(sb.ToString());
		}

		private void LoadFonts()
		{
			//if (ApplicationSettings.IsDebugActive.Value)
			//{
			//	PrintAllFonts();
			//}

			/*string[] fontsNames;

			if (Application.platform == RuntimePlatform.Android)
				fontsNames = androidFonts;
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
				fontsNames = iOSFonts;
			else
				fontsNames = windowsFonts;

			List<TMP_FontAsset> fontAssets = new List<TMP_FontAsset>();
			//foreach (var fontName in fontsNames)
			//{
			//	var font = GetFontByFileName(fontName, out string fontPath);
			//	if (font != null)
			//	{
			//		TMP_FontAsset osFontAsset = TMP_FontAsset.CreateFontAsset(font, 40, 9, UnityEngine.TextCore.LowLevel.GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic, true);
			//		if (osFontAsset != null)
			//			fontAssets.Add(osFontAsset);

			//		Debug.Log($"Font {fontName} loaded from path {fontPath}");
			//	}
			//	else
			//	{
			//		Debug.Log($"Font {fontName} not found");
			//	}
			//}

			string[] fontPaths = Font.GetPathsToOSFonts();
			foreach (var fontPath in fontPaths)
			{
				var font = new Font(fontPath);
				if (font != null)
				{
					TMP_FontAsset osFontAsset = TMP_FontAsset.CreateFontAsset(font, 40, 9, UnityEngine.TextCore.LowLevel.GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic, true);
					if (osFontAsset != null)
						fontAssets.Add(osFontAsset);

					Debug.Log($"Font loaded from path {fontPath}");
				}
				else
				{
					Debug.Log($"Font {fontPath} can't create");
				}
			}

			Array.ForEach(listFonts, f => { f.fallbackFontAssetTable = fontAssets; });*/
		}

		public static FontLoader LoadDefaultSettings()
		{
			if (s_Instance == null)
			{
				FontLoader settings = Resources.Load<FontLoader>("FontLoader");
				if (settings != null)
					s_Instance = settings;

				s_Instance.LoadFonts();
			}

			return s_Instance;
		}

		public static FontLoader Instance
		{
			get
			{
				if (s_Instance == null)
				{
					FontLoader settings = Resources.Load<FontLoader>("FontLoader");
					if (settings != null)
						s_Instance = settings;

					s_Instance.LoadFonts();
				}

				return s_Instance;
			}
		}
	}
}
