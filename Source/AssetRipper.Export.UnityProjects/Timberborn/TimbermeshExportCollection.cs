using AssetRipper.Assets;
using AssetRipper.Import.Structure.Assembly;
using AssetRipper.Import.Structure.Assembly.Serializable;
using AssetRipper.Processing.Prefabs;
using AssetRipper.SourceGenerated.Classes.ClassID_1001480554;
using AssetRipper.SourceGenerated.Classes.ClassID_1001;
using AssetRipper.SourceGenerated.Classes.ClassID_114;
using AssetRipper.SourceGenerated.Classes.ClassID_115;
using AssetRipper.Export.UnityProjects.Project;

namespace AssetRipper.Export.UnityProjects.Timberborn;

public sealed class TimbermeshExportCollection : AssetsExportCollection<IPrefabInstance>
{
    public TimbermeshExportCollection(IAssetExporter assetExporter, PrefabHierarchyObject asset) : base(assetExporter, asset.Prefab)
	{
		AddAssets(asset.Assets);
		AddAsset(asset);
	}

	protected override bool ExportInner(IExportContainer container, string filePath, string dirPath, FileSystem fileSystem)
	{
		if (!TryGetTimbermesh(Asset, out byte[]? data))
			return false;
		fileSystem.File.WriteAllBytes(filePath, data);
		return true;
	}

	protected override string GetExportExtension(IUnityObjectBase asset) => TimbermeshKeyword;

    public static bool IsTimbermesh(IUnityObjectBase asset)
    {
        if (asset.MainAsset is PrefabHierarchyObject prefab) {
            foreach (IUnityObjectBase prefabAsset in prefab.Assets)
            {
                if (prefabAsset is IMonoBehaviour monoBehaviour)
                {
                    if (!monoBehaviour.Script.TryGetAsset(monoBehaviour.Collection, out IMonoScript? script))
                        continue;
                    if (script.Namespace.String == "Timberborn.TimbermeshEditorTools")
                        return true;
                }
            }
        }
        return false;
    }

    public static bool TryGetTimbermesh(IUnityObjectBase asset, [NotNullWhen(true)] out byte[]? data)
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
	public const string TimbermeshKeyword = "timbermesh";
}