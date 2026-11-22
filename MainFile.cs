using CustomCardTextureLoader.CustomCardTextureLoaderCode;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Debug;
using MegaCrit.Sts2.Core.Modding;
using Steamworks;
using Version = System.Version;

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
        
        var releaseInfo = ReleaseInfoManager.Instance.ReleaseInfo;
        if (Version.TryParse(releaseInfo?.Version[1..], out var gameVer))
        {
            if (gameVer <= Version.Parse("0.111.1"))
            {
                Logger.Info("LegacyOnly");
                harmony.PatchCategory("LegacyOnly");
            }
            else
            {
                harmony.PatchAll();
            }
        }
        else
        {
            Logger.Info("Game version parse failed");
            harmony.PatchAll();
        }
        
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
        foreach (var dir in MainCode.SearchDirectories)
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
            var workshopTexturePath = Path.Combine(path, MainCode.TexturePath);
            if (Directory.Exists(workshopTexturePath))
            {
                MainCode.SearchDirectories.Add(workshopTexturePath);
            }
        }
    }
}