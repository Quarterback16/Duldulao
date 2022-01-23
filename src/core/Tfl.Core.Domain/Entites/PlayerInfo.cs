namespace Tfl.Core.Domain.Entites
{
    public class PlayerInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Injury { get; set; }

        public int Level { get; set; }

        public string TeamCode { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
