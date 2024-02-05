using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalLib.Modules;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using WoolCompany.Behaviors;

namespace WoolCompany.Behaviors
{
    internal class LaunchPlayerItem : PhysicsProp
    {
        public RaycastHit hitLocation;
        private ManualLogSource Logger;
        public override void DiscardItem()
        {
            Vector3 des = GetThrowDestination();
            Logger.LogInfo("Throwing Item1: " + des);
            base.DiscardItem();
            //FallWithCurve();
            //playerHeldBy.DiscardHeldObject(placeObject: true, null, GetThrowDestination());
            
            Logger.LogInfo("Throwing Item: " + des);
        }

        public Vector3 GetThrowDestination()
        {
            Vector3 position = base.transform.position;
            Debug.DrawRay(playerHeldBy.gameplayCamera.transform.position, playerHeldBy.gameplayCamera.transform.forward, Color.yellow, 15f);
            Ray grenadeThrowRay = new Ray(playerHeldBy.gameplayCamera.transform.position, playerHeldBy.gameplayCamera.transform.forward);
            position = ((!Physics.Raycast(grenadeThrowRay, out hitLocation, 12f, StartOfRound.Instance.collidersAndRoomMaskAndDefault)) ? grenadeThrowRay.GetPoint(10f) : grenadeThrowRay.GetPoint(hitLocation.distance - 0.05f));
            Debug.DrawRay(position, Vector3.down, Color.blue, 15f);
            grenadeThrowRay = new Ray(position, Vector3.down);
            if (Physics.Raycast(grenadeThrowRay, out hitLocation, 30f, StartOfRound.Instance.collidersAndRoomMaskAndDefault))
            {
                return hitLocation.point + Vector3.up * 0.05f;
            }
            return grenadeThrowRay.GetPoint(30f);
        }
    }
}
