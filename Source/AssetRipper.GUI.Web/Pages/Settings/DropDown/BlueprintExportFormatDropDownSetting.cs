using AssetRipper.Export.Configuration;

namespace AssetRipper.GUI.Web.Pages.Settings.DropDown;

public sealed class BlueprintExportFormatDropDownSetting : DropDownSetting<BlueprintExportFormat>
{
	public static BlueprintExportFormatDropDownSetting Instance { get; } = new();

	public override string Title => Localization.BlueprintExportTitle;

	protected override string GetDisplayName(BlueprintExportFormat value) => value switch
	{
		BlueprintExportFormat.Json => Localization.BlueprintFormatJson,
		BlueprintExportFormat.Default => Localization.BlueprintFormatDefault,
		_ => base.GetDisplayName(value),
	};

	protected override string? GetDescription(BlueprintExportFormat value) => value switch
	{
		BlueprintExportFormat.Json => Localization.BlueprintFormatJsonDescription,
		BlueprintExportFormat.Default => Localization.BlueprintFormatDefaultDescription,
		_ => base.GetDescription(value),
	};
}
