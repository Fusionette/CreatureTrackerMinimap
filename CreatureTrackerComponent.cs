using UnityEngine;

namespace CreatureTrackerMinimap
{
    internal class CreatureTrackerComponent : MonoBehaviour
    {
        private string displayName;
        private Minimap.PinData pinData;

        public void Initialize(string displayName)
        {
            this.displayName = displayName;
        }

        private void Update()
        {
            if (pinData == null) pinData = Minimap.instance.AddPin(base.transform.position, Minimap.PinType.Icon3, displayName, false, false);
            pinData.m_pos = base.transform.position;
        }

        private void OnDestroy()
        {
            if (pinData == null) return;
            Minimap.instance.RemovePin(pinData);
        }
    }
}
