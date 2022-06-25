using Exiled.API.Interfaces;

namespace BanGun
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool IsCom15BanGun { get; set; } = true;
        public bool IsCom18BanGun { get; set; } = false;
        public float AmmoUsedPerShot { get; set; } = 0f;
        public int BanDuration { get; set; } = 3600;
        public string BanReason { get; set; } = "Banned by a BanGun LOL get good";
    }
}