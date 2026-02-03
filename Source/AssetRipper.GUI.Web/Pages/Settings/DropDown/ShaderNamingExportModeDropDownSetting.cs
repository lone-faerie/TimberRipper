using AssetRipper.Export.Configuration;

namespace AssetRipper.GUI.Web.Pages.Settings.DropDown;

public sealed class ShaderNamingExportModeDropDownSetting : DropDownSetting<ShaderNamingExportMode>
{
	public static ShaderNamingExportModeDropDownSetting Instance { get; } = new();

	public override string Title => Localization.ShaderNamingExportTitle;

	protected override string GetDisplayName(ShaderNamingExportMode value) => value switch
	{
		ShaderNamingExportMode.Ripped => Localization.ShaderNamingModeRipped,
		ShaderNamingExportMode.Default => Localization.ShaderNamingModeDefault,
		_ => base.GetDisplayName(value),
	};

	protected override string? GetDescription(ShaderNamingExportMode value) => value switch
	{
		ShaderNamingExportMode.Ripped => Localization.ShaderNamingModeRippedDescription,
		ShaderNamingExportMode.Default => Localization.ShaderNamingModeDefaultDescription,
		_ => base.GetDescription(value),
	};
}
