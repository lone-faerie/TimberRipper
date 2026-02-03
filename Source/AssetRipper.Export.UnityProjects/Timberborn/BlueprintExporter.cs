using AssetRipper.Assets;
using AssetRipper.Import.Logging;
using AssetRipper.Import.Structure.Assembly;
using AssetRipper.Import.Structure.Assembly.Serializable;
using AssetRipper.SourceGenerated.Classes.ClassID_114;
using AssetRipper.SourceGenerated.Classes.ClassID_115;

namespace AssetRipper.Export.UnityProjects.Timberborn;

public sealed class BlueprintExporter : BinaryAssetExporter
{
	public override bool TryCreateCollection(IUnityObjectBase asset, [NotNullWhen(true)] out IExportCollection? exportCollection)
    {
        if (IsBlueprint(asset))
        {
            exportCollection = new BlueprintExportCollection(this, (IMonoBehaviour) asset);
            return true;
        }
        exportCollection = null;
        return false;
    }

	public override bool Export(IExportContainer container, IUnityObjectBase asset, string path, FileSystem fileSystem)
    {
        if (!TryGetBlueprint(asset, out string? data))
            return false;
        Logger.Info(LogCategory.Export, $"Blueprint has length {data.Length}");
        fileSystem.File.WriteAllText(path, data);
        return true;
    }

    public static bool IsBlueprint(IUnityObjectBase asset)
    {
        if (asset is IMonoBehaviour monoBehaviour)
        {
            if (monoBehaviour.Script.TryGetAsset(monoBehaviour.Collection, out IMonoScript? script))
                if (script.Namespace.String == "Timberborn.BlueprintSystem")
                    return asset.GetBestName().EndsWith("blueprint");
        }
        return false;
    }

    private static bool TryGetBlueprint(IUnityObjectBase asset, [NotNullWhen(true)] out string? data)
    {
        data = null;
        if (asset is IMonoBehaviour monoBehaviour)
        {
            SerializableStructure? structure = monoBehaviour.LoadStructure();
            if (structure is null)
                return false;
            if (structure.TryGetField("_content", out SerializableValue content))
            {
                data = content.AsString;
                return true;
            }
        }
        return false;
    }
}