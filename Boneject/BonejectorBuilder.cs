using MelonLoader;

namespace Boneject
{
    public class BonejectorBuilder
    {
        private readonly MelonInfoAttribute _melonInfo;
        
        public BonejectorBuilder(MelonInfoAttribute melonInfo)
        {
            _melonInfo = melonInfo;
        }

        public Bonejector Build() => new Bonejector(_melonInfo);
    }
}
