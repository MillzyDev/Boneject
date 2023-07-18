using MelonLoader;

namespace Boneject
{
    public class Bonejector
    {
        private MelonInfoAttribute _info;
        
        private Bonejector(MelonInfoAttribute info)
        {
            _info = info;
        }

        public static Bonejector Get(MelonInfoAttribute info)
        {
            var bonejector = new Bonejector(info);
            // TODO: Register in BonejectManager
            return bonejector;
        }
    }
}
