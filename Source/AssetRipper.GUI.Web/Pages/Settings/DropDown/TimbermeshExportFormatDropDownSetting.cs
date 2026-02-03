using AssetRipper.Export.Configuration;

namespace AssetRipper.GUI.Web.Pages.Settings.DropDown;

public sealed class TimbermeshExportFormatDropDownSetting : DropDownSetting<TimbermeshExportFormat>
{
	public static TimbermeshExportFormatDropDownSetting Instance { get; } = new();

	public override string Title => Localization.TimbermeshExportTitle;

	protected override string GetDisplayName(TimbermeshExportFormat value) => value switch
	{
		TimbermeshExportFormat.Prefab => Localization.TimbermeshFormatPrefab,
		TimbermeshExportFormat.Default => Localization.TimbermeshFormatDefault,
		_ => base.GetDisplayName(value),
	};

	protected override string? GetDescription(TimbermeshExportFormat value) => value switch
	{
		TimbermeshExportFormat.Prefab => Localization.TimbermeshFormatPrefabDescription,
		TimbermeshExportFormat.Default => Localization.TimbermeshFormatDefaultDescription,
		_ => base.GetDescription(value),
	};
}
