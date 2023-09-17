using UnityEngine;

namespace CreatureTrackerMinimap
{
    internal class CreatureTrackerComponent : MonoBehaviour
    {
        private string displayName;
        private Sprite displayIcon;
        private Minimap.PinData pinData;

        public void Initialize(string displayName, Sprite displayIcon)
        {
            this.displayName = displayName;
            this.displayIcon = displayIcon;
        }

        public void SetName(string displayName)
        {
            this.displayName = displayName;
            if (pinData != null) pinData.m_name = displayName;
        }

        private void Update()
        {
            if (pinData == null)
            {
                pinData = Minimap.instance.AddPin(base.transform.position, Minimap.PinType.Hildir1, displayName, false, false);
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
