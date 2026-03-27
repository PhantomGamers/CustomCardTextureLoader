using Godot;
using HarmonyLib;
using JetBrains.Annotations;
using MegaCrit.Sts2.Core.Assets;

namespace CustomCardTextureLoader.CustomCardTextureLoaderCode;

[HarmonyPatch(typeof(AtlasManager))]
[UsedImplicitly]
internal class AtlasManagerPatches
{
    internal static readonly string ExecutableDir = OS.GetExecutablePath().GetBaseDir();
    internal static readonly string ModDir = Path.Combine(ExecutableDir, "mods");
    
    private const string TexturePath = "CustomCardTextures";

    private static readonly List<string> SearchDirectories =
    [
        Path.Combine(ExecutableDir, TexturePath),
        Path.Combine(ModDir, TexturePath)
    ];
    
    [HarmonyPatch(nameof(AtlasManager.GetSprite))]
    [UsedImplicitly]
    internal static bool Prefix(string atlasName, string spriteName, ref AtlasTexture __result)
    {
        if (atlasName != "card_atlas") return true;
        var fileName = spriteName + ".png";

        foreach (var dir in SearchDirectories)
        {
            fileName = Path.Combine(dir, fileName);
            if (!File.Exists(fileName)) continue;
            __result = GetAtlasTextureFromFile(fileName);
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