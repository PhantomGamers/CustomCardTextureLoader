using CustomCardTextureLoader.CustomCardTextureLoaderCode;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using Steamworks;

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
        try
        {
            AddWorkshopFoldersToSearchDirectories();
        }
        catch (Exception e)
        {
            Logger.Error("Failed to add workshop folders to search directories");
            Logger.Error(e.Message);
        }
        Logger.Info("Looking for card textures in the following directories:");
        foreach (var dir in AtlasManagerPatches.SearchDirectories)
        {
            Logger.Info(dir);
        }
    }

    private static void AddWorkshopFoldersToSearchDirectories()
    {
        var subscribedItemCount = SteamUGC.GetNumSubscribedItems();
        var workshopItems = new PublishedFileId_t[subscribedItemCount];
        subscribedItemCount = SteamUGC.GetSubscribedItems(workshopItems, subscribedItemCount);
        for (var i = 0; i < subscribedItemCount; i++)
        {
            var publishedFileIdT = workshopItems[i];

            if (!SteamUGC.GetItemInstallInfo(publishedFileIdT, out _, out var path, 256, out _))
                continue;
            var workshopTexturePath = Path.Combine(path, AtlasManagerPatches.TexturePath);
            if (Directory.Exists(workshopTexturePath))
            {
                AtlasManagerPatches.SearchDirectories.Add(workshopTexturePath);
            }
        }
    }
}