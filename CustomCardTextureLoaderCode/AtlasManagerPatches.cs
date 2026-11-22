using Godot;
using HarmonyLib;
using JetBrains.Annotations;
using MegaCrit.Sts2.Core.Assets;

namespace CustomCardTextureLoader.CustomCardTextureLoaderCode;

[HarmonyPatchCategory("LegacyOnly")]
[HarmonyPatch(typeof(AtlasManager))]
[UsedImplicitly]
internal class AtlasManagerPatches
{
    [HarmonyPatch(nameof(AtlasManager.GetSprite))]
    [UsedImplicitly]
    internal static bool Prefix(string atlasName, string spriteName, ref AtlasTexture __result)
    {
        if (atlasName != "card_atlas") return true;

        foreach (var filePath in MainCode.FindTextureFiles(spriteName))
        {
            if (!TryGetAtlasTextureFromFile(filePath, out var result))
                continue;

            __result = result;
            return false;
        }
        
        return true;
    }

    private static bool TryGetAtlasTextureFromFile(string filePath, out AtlasTexture result)
    {
        try
        {
            var image = Image.LoadFromFile(filePath);
            var texture = ImageTexture.CreateFromImage(image);
            result = new AtlasTexture
            {
                Atlas = texture
            };
            return true;
        }
        catch
        {
            result = null!;
            return false;
        }
    }
}