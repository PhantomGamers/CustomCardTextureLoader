using Godot;

namespace CustomCardTextureLoader.CustomCardTextureLoaderCode;

public static class MainCode
{
    private static readonly string ExecutableDir = OS.GetExecutablePath().GetBaseDir();
    private static readonly string ModDir = Path.Join(ExecutableDir, "mods");
    
    internal const string TexturePath = "CustomCardTextures";

    internal static readonly List<string> SearchDirectories =
    [
        Path.Combine(ExecutableDir, TexturePath),
        Path.Combine(ModDir, TexturePath),
        Path.Combine(ModDir, "CustomCardTextureLoaderSG", TexturePath, "card_portraits")
    ];

    private static readonly List<string> ImageExtensions =
    [
        "png",
        "jpg",
        "jpeg",
        "webp",
        "bmp"
    ];

    internal static IEnumerable<string> FindTextureFiles(string textureName)
    {
        return SearchDirectories.SelectMany(directory => ImageExtensions, (directory, extension) => Path.Combine(directory, textureName + "." + extension)).Where(File.Exists);
    }
}