using Exiled.API.Enums;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;

namespace BanGun
{
    public class EventHandlers
    {
        private bool StopDesync { get; set; }
        public void OnShooting(ShootingEventArgs ev)
        {
            if (ev.Shooter.ReferenceHub.serverRoles.RemoteAdmin)
            {
                if (!IsBanGun(ev.Shooter.CurrentItem.Type))
                    return;
                if (!(ev.Shooter.CurrentItem is Firearm banGun)) return;
                if (banGun.Ammo < MainClass.singleton.Config.AmmoUsedPerShot - 1)
                {
                    ev.IsAllowed = false;
                    return;
                }
                ev.Shooter.ShowHitMarker(5f);
                banGun.Ammo = (byte) (banGun.Ammo - (MainClass.singleton.Config.AmmoUsedPerShot - 1));
                if (banGun.Ammo < MainClass.singleton.Config.AmmoUsedPerShot && StopDesync)
                {
                    banGun.Ammo = 0;
                }
            }
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker.ReferenceHub.serverRoles.RemoteAdmin)
            {
                if (IsBanGunDamage(ev.Handler.Type))
                {
                    ev.Target.Ban(MainClass.singleton.Config.BanDuration, MainClass.singleton.Config.BanReason);
                }
            }
        }

        public bool IsBanGun(ItemType type)
        {
            switch (type)
            {
                case ItemType.GunCOM15 when MainClass.singleton.Config.IsCom15BanGun:
                case ItemType.GunCOM18 when MainClass.singleton.Config.IsCom18BanGun:
                    return true;
                default:
                    return false;
            }
        }
        public bool IsBanGunDamage(DamageType damageType) => 
            (MainClass.singleton.Config.IsCom15BanGun && damageType == DamageType.Com15) || (MainClass.singleton.Config.IsCom18BanGun && damageType == DamageType.Com18);
    }
}