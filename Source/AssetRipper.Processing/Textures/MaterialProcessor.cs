using AssetRipper.Import.Logging;
using AssetRipper.SourceGenerated.Classes.ClassID_21;

namespace AssetRipper.Processing.Textures;

public sealed partial class MaterialProcessor : IAssetProcessor
{

    private string _timberbornModdingFolder;

    public MaterialProcessor(string timberbornModdingFolder)
    {
        _timberbornModdingFolder = timberbornModdingFolder;
    }

	public void Process(GameData gameData)
	{
		foreach (IMaterial mat in gameData.GameBundle.FetchAssets().OfType<IMaterial>())
		{
            Logger.Info(LogCategory.Processing, $"{mat.Name}: {mat.Shader_C21P!.Name}");
		}
	}
}