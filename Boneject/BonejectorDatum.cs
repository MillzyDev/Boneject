namespace Boneject
{
    public class BonejectorDatum
    {
        public bool Enabled { get; set; }
        public Bonejector Bonejector { get; }

        public BonejectorDatum(Bonejector bonejector)
        {
            Bonejector = bonejector;
        }
    }
}
