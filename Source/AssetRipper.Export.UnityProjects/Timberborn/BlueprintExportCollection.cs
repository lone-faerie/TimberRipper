using AssetRipper.Assets;
using AssetRipper.SourceGenerated.Classes.ClassID_114;

namespace AssetRipper.Export.UnityProjects.Timberborn;

public sealed class BlueprintExportCollection : AssetExportCollection<IMonoBehaviour>
{
    public BlueprintExportCollection(BlueprintExporter assetExporter, IMonoBehaviour asset) : base(assetExporter, asset)
    {
    }

	protected override string GetExportExtension(IUnityObjectBase asset) => "json";
}