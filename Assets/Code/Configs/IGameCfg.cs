namespace Code.Configs
{
    public interface IGameCfg
    {
        public float PlayerHealth { get; }
        public float PlayerSpeed{ get; }
        
        public float ShootDelay{ get; }

        public float JerkDistance{ get; }
        public float JerkSpeed{ get; }
        public float JerkRechargeTime{ get; }
    }
}