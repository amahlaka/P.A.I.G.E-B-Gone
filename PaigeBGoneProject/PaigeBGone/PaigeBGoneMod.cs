using SpaceWarp.API.Mods;
using KSP.Game;
using HarmonyLib;
using KSP.UI;
using UnityEngine;
using KSP.Game.Missions.Definitions;
using System.Collections.Generic;
namespace PaigeBGone
{
    [MainMod]
     public class PaigeBGoneMod : Mod
    {
        public override void OnInitialized()
        {
            PersistentProfileManager.VoiceVolume = 0f;
            PersistentProfileManager.IsFTUEEnabled = false;

            Logger.Info("[PAIGEBGONE]");
        }
        [HarmonyPatch(typeof(KSP.Game.Missions.KSP2MissionManager))]
        [HarmonyPatch("ApplyProgressFromSavedGame")]
        public class ApplyProgressFromSavedGamePatch
        {
            static void Postfix()
            {
                GameInstance a = GameManager.Instance.Game;
                var Undesirables = new List<MissionType> () { MissionType.Tutorial, MissionType.FTUE };
                var MissionList = a.KSP2MissionManager.Missions;
                MissionList.Find((MissionData x) =>  Undesirables.Contains(x.type)).state = KSP.Game.Missions.State.MissionState.Complete;
                Debug.Log("[PAIGEBGONE] Muted P.A.I.G.E forever");
            }
        }
    }
}