using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace CreatureTrackerMinimap
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class CreatureTrackerMinimap : BaseUnityPlugin
    {
        private const string modGUID = "Fusionette.CreatureTrackerMinimap";
        private const string modName = "Creature Tracker Minimap";
        private const string modVersion = "0.9.0";
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
