using System.Collections.Generic;

namespace SilverHunterMod
{
    public class SlayerData
    {
        public List<SlayerQuestCluster> SlayerQuestClusters;
    }

    public class SlayerQuestCluster
    {
        public string Name;
        public string LocalizationKey;
        public List<string> Entries;
        public int KillsRequired;
    }
}
