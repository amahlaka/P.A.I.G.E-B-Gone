using SpaceWarp.API.Mods;
using KSP.Game;

namespace PaigeBGone
{
    [MainMod]
     public class PaigeBGoneMod : Mod
    {
        public override void OnInitialized()
        {
            PersistentProfileManager.VoiceVolume = 0f;
            PersistentProfileManager.IsFTUEEnabled = false;
        }
    }
}