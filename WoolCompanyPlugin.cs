using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalLib.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WoolCompany.Behaviors;
using WoolCompany.Patches;
using System.Text;
using System.Threading.Tasks;

namespace WoolCompany
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class WoolCompanyPlugin : BaseUnityPlugin
    {
        const string GUID = "Crystal.WoolCompany";
        const string NAME = "WoolCompany";
        const string VERSION = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(GUID);

        public static WoolCompanyPlugin instance;
        
        internal static AssetBundle bundle;

        void Awake()
        {
            instance = this;

            // Loads the asset if in the same folder as the ddl
            string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "woolcompany");
            bundle = AssetBundle.LoadFromFile(assetDir);
            Item candiceItem = bundle.LoadAsset<Item>("Assets/Prefabs/CandiceItem.asset"); // Get the item (Can see the path in the .manifest)
            if (candiceItem == null)
            {
                Logger.LogError("Failed to load item from asset bundle.");
                return;
            }

            // Testing self-damage script. This adds a physics prop. This usually goes on the prefab.
            DamagePlayerItem script = candiceItem.spawnPrefab.AddComponent<DamagePlayerItem>();
            //LaunchPlayerItem script = candiceItem.spawnPrefab.AddComponent<LaunchPlayerItem>();
            script.grabbable= true;
            script.grabbableToEnemies = true;
            script.itemProperties = candiceItem;

            // Register item
            NetworkPrefabs.RegisterNetworkPrefab(candiceItem.spawnPrefab); // Makes it so other clients will know what this is
            Utilities.FixMixerGroups(candiceItem.spawnPrefab); // Fix Audio
            Items.RegisterScrap(candiceItem, 1000, Levels.LevelTypes.All); // 1-100 is very common. 1000 makes it so it pretty much always spawns

            TerminalNode node = ScriptableObject.CreateInstance<TerminalNode>();
            if (node == null)
            {
                Logger.LogError("Failed to create TerminalNode.");
                return;
            }

            node.clearPreviousText = true;
            node.displayText = "This is info about Candice\n\n";
            Items.RegisterShopItem(candiceItem, null, null, node, 0);
            Items.UpdateShopItemPrice(candiceItem, 30);

            Logger.LogInfo("Loaded WoolCompany Successfully");

            // ------------ Sound Patches ---------------
            harmony.PatchAll(typeof(StartOfRoundPatch)); // Patches methods
            harmony.PatchAll(typeof(CompanyMoodPatch)); // Patches methods
        }

    }
}