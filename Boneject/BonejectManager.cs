using System.Collections.Generic;
using System.Linq;
using MelonLoader;

namespace Boneject
{
    public class BonejectManager
    {
        private readonly HashSet<BonejectorDatum> _bonejectors = new();

        internal void MelonRegistered(MelonBase melon)
        {
            if (melon.MelonTypeName != "Mod") return;

            BonejectorDatum? datum =
                _bonejectors.FirstOrDefault(datum => Equals(datum.Bonejector.MelonInfo, melon.Info));
            if (datum is not null)
                datum.Enabled = true;
        }
        
        internal void MelonUnregistered(MelonBase melon)
        {
            if (melon.MelonTypeName != "Mod") return;

            BonejectorDatum? datum =
                _bonejectors.FirstOrDefault(datum => Equals(datum.Bonejector.MelonInfo, melon.Info));
            if (datum is not null)
                datum.Enabled = false;
        }

        public void Enable()
        {
            foreach (BonejectorDatum? datum in _bonejectors)
            {
                MelonInfoAttribute info = datum.Bonejector.MelonInfo;
                datum.Enabled = MelonBase.FindMelon(info.Name, info.Author) != null;
            }
        }
    }
}
