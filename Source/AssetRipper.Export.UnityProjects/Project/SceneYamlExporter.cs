using AssetRipper.Assets;
using AssetRipper.Export.Configuration;
using AssetRipper.Export.UnityProjects.Timberborn;
using AssetRipper.Processing.Prefabs;

namespace AssetRipper.Export.UnityProjects.Project;

public class SceneYamlExporter : YamlExporterBase
{
	private TimbermeshExportFormat TimbermeshExportFormat;
	private TimberMapExportFormat TimberMapExportFormat;
	public SceneYamlExporter(FullConfiguration settings)
	{
		TimbermeshExportFormat = settings.ExportSettings.TimbermeshExportFormat;
		TimberMapExportFormat = settings.ExportSettings.TimberMapExportFormat;
	}

	public override bool TryCreateCollection(IUnityObjectBase asset, [NotNullWhen(true)] out IExportCollection? exportCollection)
	{
		switch (asset.MainAsset)
		{
			case SceneHierarchyObject sceneHierarchyObject:
				exportCollection = new SceneExportCollection(this, sceneHierarchyObject);
				return true;
			case PrefabHierarchyObject prefabHierarchyObject:
				if (TimbermeshExportFormat == TimbermeshExportFormat.Default && TimbermeshExportCollection.IsTimbermesh(asset))
					exportCollection = new TimbermeshExportCollection(this, prefabHierarchyObject);
				else if (TimberMapExportFormat == TimberMapExportFormat.Default && TimberMapExportCollection.IsMap(asset))
					exportCollection = new TimberMapExportCollection(this, prefabHierarchyObject);
				else
					exportCollection = new PrefabExportCollection(this, prefabHierarchyObject);
				return true;
			default:
				exportCollection = null;
				return false;
		}
	}
}
