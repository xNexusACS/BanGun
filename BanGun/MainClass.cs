using System;
using Exiled.API.Features;
using PlayerHandler = Exiled.Events.Handlers.Player;

namespace BanGun
{
    public class MainClass : Plugin<Config>
    {
        public static MainClass singleton;
        public override string Author { get; } = "xNexusACS";
        public override string Name { get; } = "BanGun";
        public override string Prefix { get; } = "bangun";
        public override Version Version { get; } = new Version(0, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 1);
        
        public EventHandlers ev { get; private set; }

        public override void OnEnabled()
        {
            singleton = this;
            ev = new EventHandlers();

            PlayerHandler.Shooting += ev.OnShooting;
            PlayerHandler.Hurting += ev.OnHurting;
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            PlayerHandler.Shooting -= ev.OnShooting;
            PlayerHandler.Hurting -= ev.OnHurting;
            
            singleton = null;
            ev = null;
            base.OnDisabled();
        }
    }
}