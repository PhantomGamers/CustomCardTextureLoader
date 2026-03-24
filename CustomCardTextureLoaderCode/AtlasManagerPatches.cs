using Godot;
using HarmonyLib;
using JetBrains.Annotations;
using MegaCrit.Sts2.Core.Assets;

namespace CustomCardTextureLoader.CustomCardTextureLoaderCode;

[HarmonyPatch(typeof(AtlasManager))]
[UsedImplicitly]
internal class AtlasManagerPatches
{
    [HarmonyPatch(nameof(AtlasManager.GetSprite))]
    [UsedImplicitly]
    internal static bool Prefix(string atlasName, string spriteName, ref AtlasTexture __result)
    {
        if (atlasName != "card_atlas") return true;
        var fileName = "CustomCardTextures/" + spriteName + ".png";
        
        if (File.Exists(fileName))
        {
            __result = GetAtlasTextureFromFile(fileName);
            return false;
        }

        fileName = "mods/" + fileName;

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