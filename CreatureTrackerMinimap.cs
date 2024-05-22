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
        private const string modVersion = "0.9.8";
        private readonly Harmony harmony = new Harmony(modGUID);

        public static ConfigEntry<int> cfgNexusID;
        public static ConfigEntry<bool> cfgTrackFish;
        public static ConfigEntry<bool> cfgShowNames;
        public static ConfigEntry<float> cfgHudDistance;
        public static ConfigEntry<float> cfgHudDuration;

        void Awake()
        {
            cfgNexusID = Config.Bind<int>("General", "NexusID", 2706, "Nexus ID to check for updates.");
            cfgTrackFish = Config.Bind<bool>("General", "TrackFish", true, "Show Fish on the minimap.");
            cfgShowNames = Config.Bind<bool>("General", "ShowNames", true, "Show creature names on the map screen.");
            cfgHudDistance = Config.Bind<float>("Hud", "ShowDistance", 10f, "Show enemy health bars on mouse hover from this distance.");
            cfgHudDuration = Config.Bind<float>("Hud", "ShowDuration", 60f, "Keep enemy health bars visible for this many seconds.");

            harmony.PatchAll();
        }

        void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }
}
