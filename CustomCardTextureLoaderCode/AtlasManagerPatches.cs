using Godot;
using HarmonyLib;
using JetBrains.Annotations;
using MegaCrit.Sts2.Core.Assets;

namespace CustomCardTextureLoader.CustomCardTextureLoaderCode;

[HarmonyPatch(typeof(AtlasManager))]
[UsedImplicitly]
internal class AtlasManagerPatches
{
    private static readonly string ExecutableDir = OS.GetExecutablePath().GetBaseDir();
    private static readonly string ModDir = Path.Join(ExecutableDir, "mods");
    
    private const string TexturePath = "CustomCardTextures";

    internal static readonly List<string> SearchDirectories =
    [
        Path.Combine(ExecutableDir, TexturePath),
        Path.Combine(ModDir, TexturePath),
        Path.Combine(ModDir, "CustomCardTextureLoaderSG", TexturePath, "card_portraits")
    ];
    
    [HarmonyPatch(nameof(AtlasManager.GetSprite))]
    [UsedImplicitly]
    internal static bool Prefix(string atlasName, string spriteName, ref AtlasTexture __result)
    {
        if (atlasName != "card_atlas") return true;
        var fileName = spriteName + ".png";

        foreach (var filePath in SearchDirectories.Select(dir => Path.Combine(dir, fileName)).Where(File.Exists))
        {
            __result = GetAtlasTextureFromFile(filePath);
            return false;
        }
        
        return true;
    }

    private static AtlasTexture GetAtlasTextureFromFile(string filePath)
    {
        var image = Image.LoadFromFile(filePath);
        var texture = ImageTexture.CreateFromImage(image);
        return new AtlasTexture
        {
            Atlas = texture
        };
    }
}