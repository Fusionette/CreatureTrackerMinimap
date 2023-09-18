using BepInEx;
using HarmonyLib;

namespace CreatureTrackerMinimap
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class CreatureTrackerMinimap : BaseUnityPlugin
    {
        private const string modGUID = "Fusionette.CreatureTrackerMinimap";
        private const string modName = "Creature Tracker Minimap";
        private const string modVersion = "0.9.6";
        private readonly Harmony harmony = new Harmony(modGUID);

        void Awake()
        {
            harmony.PatchAll();
        }

        void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }
}
