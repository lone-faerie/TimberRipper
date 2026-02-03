using AssetRipper.Assets;
using AssetRipper.Processing.Prefabs;
using AssetRipper.Import.Structure.Assembly;
using AssetRipper.Import.Structure.Assembly.Serializable;
using AssetRipper.SourceGenerated.Classes.ClassID_1001480554;
using AssetRipper.SourceGenerated.Classes.ClassID_1001;
using AssetRipper.SourceGenerated.Classes.ClassID_114;
using AssetRipper.SourceGenerated.Classes.ClassID_115;
using AssetRipper.Export.UnityProjects.Project;


namespace AssetRipper.Export.UnityProjects.Timberborn;

public sealed class TimberMapExportCollection : AssetsExportCollection<IPrefabInstance>
{
    public TimberMapExportCollection(IAssetExporter assetExporter, PrefabHierarchyObject asset) : base(assetExporter, asset.Prefab)
	{
		AddAssets(asset.Assets);
		AddAsset(asset);
	}

	protected override bool ExportInner(IExportContainer container, string filePath, string dirPath, FileSystem fileSystem)
	{
		if (!TryGetMap(Asset, out byte[]? data))
			return false;
		fileSystem.File.WriteAllBytes(filePath, data);
		return true;
	}

	protected override string GetExportExtension(IUnityObjectBase asset) => TimberKeyword;

    public static bool IsMap(IUnityObjectBase asset)
    {
        if (asset.MainAsset is PrefabHierarchyObject prefab) {
            foreach (IUnityObjectBase prefabAsset in prefab.Assets)
            {
                if (prefabAsset is IMonoBehaviour monoBehaviour)
                {
                    if (!monoBehaviour.Script.TryGetAsset(monoBehaviour.Collection, out IMonoScript? script))
                        continue;
                    if (script.Namespace.String == "Timberborn.AssetSystem" && MapDirectories.Contains(asset.OriginalDirectory)) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static bool TryGetMap(IUnityObjectBase asset, [NotNullWhen(true)] out byte[]? data)
    {
        data = null;
        SerializableStructure? structure = null;
        if (asset.MainAsset is PrefabHierarchyObject prefab)
        {
            foreach (IUnityObjectBase prefabAsset in prefab.Assets) {
                if (prefabAsset is IMonoBehaviour monoBehaviour) {
                    structure = monoBehaviour.LoadStructure();
                    if (structure is not null)
                        break;
                }
            }
        }
        if (structure is null)
            return false;
        if (structure.TryGetField("_bytes", out SerializableValue bytes))
        {
            data = bytes.AsByteArray;
            return true;
        }
        return false;
    }
	public const string TimberKeyword = "timber";

    public static string[] MapDirectories = {
        "Assets/Resources/maps",
        "Assets\\Resources\\maps"
    };
    public const string MapDirectory1 = "Assets/Resources/maps";
    public const string MapDirectory2 = "Assets\\Resources\\maps";
}