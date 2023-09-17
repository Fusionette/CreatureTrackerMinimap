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
            GameObject gameObject = __instance.gameObject;
            Character character = gameObject.GetComponent<Character>();

            if (character.IsPlayer()) return;

            string displayName = character.m_name;
            Sprite displayIcon = null;

            CharacterDrop drops = gameObject.GetComponent<CharacterDrop>();
            if (drops != null)
            {
                foreach (CharacterDrop.Drop drop in drops.m_drops)
                {
                    bool trophy = drop.m_prefab.name.ToLower().StartsWith("trophy");
                    if (trophy || (displayIcon == null))
                    {
                        // Greylings don't have a trophy, so always assign the icon
                        // for the first drop in the table. Then, if we find a trophy,
                        // use it instead.
                        ItemDrop item = drop.m_prefab.GetComponent<ItemDrop>();
                        displayIcon = item.m_itemData.GetIcon();
                        if (trophy) break;
                    }
                }
            }
            gameObject.AddComponent<CreatureTrackerComponent>().Initialize(displayName, displayIcon, __instance.GetLevel());
        }

        [HarmonyPatch(typeof(Character), "SetLevel")]
        [HarmonyPostfix]
        static void Character_SetLevel(Character __instance, int level)
        {
            __instance.GetComponent<CreatureTrackerComponent>().SetLevel(__instance.GetLevel());
        }

        [HarmonyPatch(typeof(Tameable), "Awake")]
        [HarmonyPostfix]
        static void Tameable_Awake(Tameable __instance, Character ___m_character)
        {
            if (!___m_character.IsTamed()) return;
            ___m_character.GetComponent<CreatureTrackerComponent>().SetName(__instance.GetHoverName());
        }

        [HarmonyPatch(typeof(Tameable), "RPC_SetName")]
        [HarmonyPostfix]
        static void Tameable_RPC_SetName(Tameable __instance, long sender, string name, string authorId)
        {
            __instance.GetComponent<CreatureTrackerComponent>().SetName(__instance.GetHoverName());
        }
    }
}
