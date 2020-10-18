namespace Samurai.Domain
{
    public class SamuraiBattle
    {
        public int BattleId { get; set; }
        public int SamuraiId { get; set; }
        public Samurai Samurai { get; set; }
        public Battle Battle { get; set; }
    }
}