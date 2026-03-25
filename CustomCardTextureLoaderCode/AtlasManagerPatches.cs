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
    
    [HarmonyPatch(nameof(AtlasManager.GetSprite))]
    [UsedImplicitly]
    internal static bool Prefix(string atlasName, string spriteName, ref AtlasTexture __result)
    {
        if (atlasName != "card_atlas") return true;
        var fileName = "CustomCardTextures/" + spriteName + ".png";
        
        var fileExecPath = Path.Combine(ExecutableDir, fileName);
        if (File.Exists(fileExecPath))
        {
            __result = GetAtlasTextureFromFile(fileExecPath);
            return false;
        }

        fileName = Path.Combine(ModDir, fileName);
        if (!File.Exists(fileName)) return true;
        __result = GetAtlasTextureFromFile(fileName);
        return false;
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