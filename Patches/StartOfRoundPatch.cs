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
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch("Start")] // nameof(PlayerControllerB.Update() would work if its not private.
        [HarmonyPostfix] // Puts the code after
        static void OverrideAudio(StartOfRound __instance)
        {
            // Sets sprintMeter to 1 every frame!
            //__instance.shipIntroSpeechSFX = WoolCompanyPlugin.SoundFX[0];
            //__instance.shipIntroSpeechSFX = WoolCompanyPlugin.bundle.LoadAsset<AudioClip>("Assets/AudoClip/name_game.ogg");

            // Alternative ways:
            //__instance.ship3DAudio.clip = <SOUNDFX HERE>
            //__instance.ship3DAudio.PlayOneShot(<SOUNDFX HERE >)
        }
    }
}
