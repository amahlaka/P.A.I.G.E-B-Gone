using SpaceWarp.API.Mods;
using SpaceWarp;
using KSP.Game;
using HarmonyLib;
using UnityEngine;
using BepInEx;
using KSP.Game.Missions.Definitions;



namespace PaigeBGone
{
    [BepInPlugin("com.amahlaka.PaigeBGone", "PaigeBGone", "1.0.0")]
    [BepInDependency(SpaceWarpPlugin.ModGuid, SpaceWarpPlugin.ModVer)]
    public class PaigeBGoneMod : BaseSpaceWarpPlugin
    {
        public override void OnInitialized()
        {
            // Set the Voiceover volume to 0.
            PersistentProfileManager.VoiceVolume = 0f;
            // Set the First Time User Experience to Disabled by default.
            PersistentProfileManager.IsFTUEEnabled = false;
            // Manually call the Harmony Patcher, as SW wont do it automatically.
            Harmony.CreateAndPatchAll(typeof(PaigeBGoneMod).Assembly);
            Logger.LogInfo("PAIGE-B-GONE");
        }
        [HarmonyPatch(typeof(KSP.Game.Missions.KSP2MissionManager))]
        [HarmonyPatch("ApplyProgressFromSavedGame")]
        public class ApplyProgressFromSavedGamePatch
        {
            static void Postfix()
            {
                // Get the active game instance.
                GameInstance gameInstance = GameManager.Instance.Game;
                // Find all Tutorial and FTUE missions and put them to a list.
                var Undesirables = new List<MissionType> () { MissionType.Tutorial, MissionType.FTUE };
                // Get the Mission Manager Missions reference
                var MissionList = gameInstance.KSP2MissionManager.Missions;
                // For each of the tutorial/ftue missions, set them as completed, so that they wont pop-up again
                MissionList.Find((MissionData x) =>  Undesirables.Contains(x.type)).state = KSP.Game.Missions.State.MissionState.Complete;
                Debug.Log("[PAIGEBGONE] Muted P.A.I.G.E forever");
            }
        }
    }
}