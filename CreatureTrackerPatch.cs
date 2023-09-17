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
            gameObject.AddComponent<CreatureTrackerComponent>().Initialize(displayName, displayIcon);
        }
    }
}
