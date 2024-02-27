using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace CreatureTrackerMinimap
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class CreatureTrackerMinimap : BaseUnityPlugin
    {
        private const string modGUID = "Fusionette.CreatureTrackerMinimap";
        private const string modName = "Creature Tracker Minimap";
        private const string modVersion = "0.9.7";
        private readonly Harmony harmony = new Harmony(modGUID);

        public static ConfigEntry<int> cfgNexusID;
        public static ConfigEntry<bool> cfgTrackFish;

        void Awake()
        {
            cfgNexusID = Config.Bind<int>("General", "NexusID", 2706, "Nexus ID to check for updates.");
            cfgTrackFish = Config.Bind<bool>("General", "TrackFish", true, "Show Fish on the minimap.");

            harmony.PatchAll();
        }

        void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }
}
