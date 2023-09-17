﻿using HarmonyLib;
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
                    if (drop.m_prefab.name.ToLower().StartsWith("trophy"))
                    {
                        ItemDrop item = drop.m_prefab.GetComponent<ItemDrop>();
                        displayIcon = item.m_itemData.GetIcon();
                        break;
                    }
                }
            }
            gameObject.AddComponent<CreatureTrackerComponent>().Initialize(displayName, displayIcon);
        }
    }
}
