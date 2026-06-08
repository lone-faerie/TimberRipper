using AssetRipper.Export.Configuration;
using AssetRipper.Import.Logging;
using AssetRipper.Processing;
using AssetRipper.SourceGenerated.Classes.ClassID_21;
using AssetRipper.SourceGenerated.Classes.ClassID_48;
using StableNameDotNet;
using System.Text.RegularExpressions;

namespace AssetRipper.Export.UnityProjects.Timberborn;

public sealed class MaterialExporter : IPostExporter
{
    private static Regex regex = new Regex("m_Shader: \\{[^}]*\\}", RegexOptions.Multiline);

	public void DoPostExport(GameData gameData, FullConfiguration settings, FileSystem fileSystem)
    {
        if (string.IsNullOrEmpty(settings.ExportSettings.TimberbornModdingFolder))
            return;
        foreach (IMaterial mat in gameData.GameBundle.FetchAssets().OfType<IMaterial>())
		{
            IShader? shader = mat.Shader_C21P;
            if (shader is null)
                continue;
            string path = Path.Join(mat.GetBestDirectory(), mat.GetBestName()) + ".mat";
            string projectPath = Path.Join(settings.ProjectRootPath, path);
            if (!fileSystem.File.Exists(projectPath))
                continue;
            string? shaderReplace = GetShader(mat, settings, fileSystem);
            if (shaderReplace is null)
                continue;
            string file = fileSystem.File.ReadAllText(projectPath);
            file = regex.Replace(file, shaderReplace, 1);
            // StreamReader stream = new StreamReader(fileSystem.File.OpenRead(projectPath));
            // List<string> lines = new();
            // int index = 0;
            // int shaderLine = -1;
            // while (stream.Peek() >= 0)
            // {
            //     string? line = stream.ReadLine();
            //     if (line is null)
            //         break;
            //     lines.Add(line);
            //     if (shaderLine < 0 && line.TrimStart().StartsWith("m_Shader"))
            //         shaderLine = index;
            //     ++index;
            // }
            // stream.Close();
            // lines[shaderLine] = shaderReplace;
            fileSystem.File.WriteAllText(projectPath, file);
		}
    }

    private string? GetShader(IMaterial mat, FullConfiguration settings, FileSystem fileSystem)
    {
        string path = Path.Join(Path.Join(mat.GetBestDirectory().Split(Path.DirectorySeparatorChar)[1..]), mat.GetBestName() + ".mat");
        foreach (string dir in fileSystem.Directory.EnumerateDirectories(Path.Join(settings.ExportSettings.TimberbornModdingFolder, "Assets", "Resources")))
        {
            string file = Path.Join(dir, path);
            if (!fileSystem.File.Exists(file))
                continue;
            string text = fileSystem.File.ReadAllText(file);
            Match match = regex.Match(text);
            if (match.Success)
                return match.ToString();
            // StreamReader stream = new StreamReader(fileSystem.File.OpenRead(file));
            // while (stream.Peek() >= 0) {
            //     string? line = stream.ReadLine();
            //     if (line is null)
            //         break;
            //     if (line.TrimStart().StartsWith("m_Shader"))
            //     {
            //         Logger.Info(LogCategory.General, $"{file}: {line}");
            //         stream.Close();
            //         return line;
            //     }
            // }
            // stream.Close();
        }
        Logger.Info(LogCategory.General, $"{path}: Not found");
        return null;
    }
}