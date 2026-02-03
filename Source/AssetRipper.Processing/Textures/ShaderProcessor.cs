using AssetRipper.Assets;
using AssetRipper.Assets.Cloning;
using AssetRipper.Import.Logging;
using AssetRipper.IO.Files;
using AssetRipper.SourceGenerated.Classes.ClassID_213;
using AssetRipper.SourceGenerated.Classes.ClassID_28;
using AssetRipper.SourceGenerated.Classes.ClassID_48;
using AssetRipper.SourceGenerated.Classes.ClassID_687078895;
using AssetRipper.SourceGenerated.Extensions;
using AssetRipper.SourceGenerated.Subclasses.SecondarySpriteTexture;
using AssetRipper.SourceGenerated.Subclasses.SpriteAtlasData;
using AssetRipper.SourceGenerated.Subclasses.SpriteRenderData;
using System.Drawing;
using System.Numerics;

namespace AssetRipper.Processing.Textures;

public sealed partial class ShaderProcessor : IAssetProcessor
{

	public void Process(GameData gameData)
	{
		foreach (IShader shader in gameData.GameBundle.FetchAssets().OfType<IShader>())
		{
			if (shader.ParsedForm is not null)
				shader.ParsedForm.Name += " (Ripped)";
		}
	}
}
