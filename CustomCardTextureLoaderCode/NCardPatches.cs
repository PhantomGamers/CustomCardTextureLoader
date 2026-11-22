using Godot;
using HarmonyLib;
using JetBrains.Annotations;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace CustomCardTextureLoader.CustomCardTextureLoaderCode;

[HarmonyPatch(typeof(NCard))]
[UsedImplicitly]
internal class NCardPatches
{
    [HarmonyPatch(nameof(NCard.SetPortraitTexture))]
    [UsedImplicitly]
    internal static void Prefix(ref Texture texture, NCard __instance)
    {
        if (__instance.Model == null) return;

        var path = Path.GetRelativePath("res://images/packed/card_portraits", __instance.Model.PortraitPngPath);
        var fileName = Path.ChangeExtension(path, "");

        if (string.IsNullOrWhiteSpace(fileName)) return;
        
        foreach (var filePath in MainCode.FindTextureFiles(fileName))
        {
            try
            {
                var image = Image.LoadFromFile(filePath);
                texture = ImageTexture.CreateFromImage(image);
            }
            catch
            {
                continue;
            }

            break;
        }
    }
}