using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Core;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("InvasivePlants", "headtapper", "1.0.0")]
    [Description("Seed and clone planting controller. Requires players to plant seeds and clones in planter boxes.")]
    class InvasivePlants : RustPlugin
    {
        private PluginConfig _config;

        #region Config
        void Init()
        {
            _config = Config.ReadObject<PluginConfig>();
        }

        class PluginConfig
        {
            public bool EnableChatMessage;
            public bool ReturnItem;
            public List<string> ItemIgnoreListShortnames;
            public bool CallOnDenyPlantHook;
        }

        protected override void LoadDefaultConfig()
        {
            PluginConfig config = new PluginConfig{
                EnableChatMessage = true,
                ReturnItem = true,
                ItemIgnoreListShortnames = new List<string>(),
                CallOnDenyPlantHook = false
            };
            Config.WriteObject(config, true);
        }

        #endregion

        #region Localization
        protected override void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>()
            {
                {"requiresPlanter", "You may only plant {type} inside of a planter!" }
            }, this, "en");
        }
        #endregion

        #region Hooks
        private void OnEntityBuilt(Planner plan, GameObject go)
        {
            var player = plan.GetOwnerPlayer();
            if (player == null)
                return;

            var plant = go.GetComponent<GrowableEntity>();
            if (plant == null)
                return;

            var sourceItem = player.GetActiveItem();

            NextTick(() =>
            {
                if (plant.GetParentEntity() is PlanterBox)
                    return;

                if (sourceItem == null)
                    return;

                foreach (string ignoredShortname in _config.ItemIgnoreListShortnames)
                {
                    if (sourceItem.info.shortname.Equals(ignoredShortname))
                        return;
                }

                if (_config.EnableChatMessage)
                    player.ChatMessage(lang.GetMessage("requiresPlanter", this, player.UserIDString).Replace("{type}", sourceItem.info.displayName.english));

                GrowableGenes originalGenes = plant.Genes;
                plant.Kill(BaseNetworkable.DestroyMode.None);

                if (_config.CallOnDenyPlantHook)
                    Interface.Call("OnDenyPlant", player, plant.SourceItemDef.shortname, sourceItem.info.displayName.english);

                if (!_config.ReturnItem)
                    return;

                var returnItem = ItemManager.CreateByName(sourceItem.info.shortname, 1);
                if (returnItem != null)
                {
                    if (returnItem.info.shortname.Contains("clone"))
                    {
                        GrowableGeneEncoding.EncodeGenesToItem(GrowableGeneEncoding.EncodeGenesToInt(originalGenes), returnItem);
                    }
                    player.inventory.GiveItem(returnItem);
                }
            });
        }
        #endregion
    }
}
