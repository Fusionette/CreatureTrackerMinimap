using HarmonyLib;
using UnityEngine;

namespace CreatureTrackerMinimap
{
    [HarmonyPatch]
    internal class CreatureTrackerPatch
    {
        [HarmonyPatch(typeof(Character), "Awake")]
        [HarmonyPostfix]
        static void Character_Awake(Character __instance)
        {
            if (Player.m_localPlayer == null) return;
            GameObject gameObject = __instance.gameObject;
            string displayName = gameObject.GetComponent<Character>().m_name;
            //Player.m_localPlayer.Message(MessageHud.MessageType.TopLeft, displayName);
            gameObject.AddComponent<CreatureTrackerComponent>().Initialize(displayName);
        }
    }
}
