using AssetRipper.Export.Configuration;

namespace AssetRipper.GUI.Web.Pages.Settings.DropDown;

public sealed class TimberMapExportFormatDropDownSetting : DropDownSetting<TimberMapExportFormat>
{
	public static TimberMapExportFormatDropDownSetting Instance { get; } = new();

	public override string Title => Localization.TimberMapExportTitle;

	protected override string GetDisplayName(TimberMapExportFormat value) => value switch
	{
		TimberMapExportFormat.Prefab => Localization.TimbermeshFormatPrefab,
		TimberMapExportFormat.Default => Localization.TimberMapFormatDefault,
		_ => base.GetDisplayName(value),
	};

	protected override string? GetDescription(TimberMapExportFormat value) => value switch
	{
		TimberMapExportFormat.Prefab => Localization.TimbermeshFormatPrefabDescription,
		TimberMapExportFormat.Default => Localization.TimberMapFormatDefaultDescription,
		_ => base.GetDescription(value),
	};
}
