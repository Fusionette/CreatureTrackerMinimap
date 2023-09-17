using UnityEngine;

namespace CreatureTrackerMinimap
{
    internal class CreatureTrackerComponent : MonoBehaviour
    {
        private string displayName;
        private string displayLevel;
        private Sprite displayIcon;
        private Minimap.PinData pinData;

        public void Initialize(string displayName, Sprite displayIcon, int level)
        {
            this.displayName = displayName;
            this.displayIcon = displayIcon;
            SetLevel(level);
        }

        public void SetName(string displayName)
        {
            this.displayName = displayName;
            if (pinData != null) pinData.m_name = displayName + displayLevel;
        }

        public void SetLevel(int level)
        {
            if (level == 2)
                this.displayLevel = " ⭐";
            else if (level == 3)
                this.displayLevel += " ⭐⭐";
            else
                this.displayLevel = string.Empty;

            if (pinData != null) pinData.m_name = displayName + displayLevel;
        }

        private void Update()
        {
            if (pinData == null)
            {
                pinData = Minimap.instance.AddPin(base.transform.position, Minimap.PinType.Hildir1, displayName + displayLevel, false, false);
                if (displayIcon != null)
                {
                    pinData.m_icon = displayIcon;
                    pinData.m_worldSize = 0f;
                }
            }
            pinData.m_pos = base.transform.position;
        }

        private void OnDestroy()
        {
            if (pinData == null) return;
            Minimap.instance.RemovePin(pinData);
        }
    }
}
