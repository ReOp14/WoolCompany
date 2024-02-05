using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WoolCompany.Patches
{
    [HarmonyPatch(typeof(DepositItemsDesk))]
    internal class CompanyMoodPatch
    {
        [HarmonyPatch("Start")] // nameof(PlayerControllerB.Update() would work if its not private.
        [HarmonyPostfix]
        //[HarmonyPrefix]
        static void SetCompanySoundPatch(DepositItemsDesk __instance)
        {
            __instance.currentMood.behindWallSFX = WoolCompanyPlugin.bundle.LoadAsset<AudioClip>("Assets/AudoClip/name_game.ogg");


            // Alternative ways:
            //__instance.ship3DAudio.clip = <SOUNDFX HERE>
            //__instance.ship3DAudio.PlayOneShot(<SOUNDFX HERE >)
        }
    }
}
