using CustomCardTextureLoader.CustomCardTextureLoaderCode;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace CustomCardTextureLoader;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    private const string
        ModId = "CustomCardTextureLoader";

    private static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);
    
    public static void Initialize()
    {
        Harmony harmony = new(ModId);
        harmony.PatchAll();
        
        Logger.Info("Initialized");
        Logger.Info("Looking for card textures in the following directories:");
        foreach (var dir in AtlasManagerPatches.SearchDirectories)
        {
            Logger.Info(dir);
        }
    }
}