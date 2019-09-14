using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace SilverHunterMod
{
    public class ModEntry : Mod
    {
        private SlayerData loadedSlayerData;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry (IModHelper helper )
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            loadedSlayerData = helper.Data.ReadJsonFile<SlayerData>("data/SlayerData.json");
        }


        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed( object sender, ButtonPressedEventArgs e )
        {
            if ( !Context.IsWorldReady || Game1.activeClickableMenu == null || e.Button != SButton.R )
                return;

            //Game1.stats.specificMonstersKilled
            //foreach (var monster in Game1.stats.specificMonstersKilled)
            //{
            //    this.Monitor.Log($"Nb of Killed {monster.Key}: {monster.Value}");
            //}

            // Opens the mode menu
            OpenHunterMenu();
        }

        private void OpenHunterMenu()
        {
            Dictionary<string, string> locs = new Dictionary<string, string>();
            locs.Add("title", Helper.Translation.Get("menu.slayer.title"));
            locs.Add("UnknownMonsterString", Helper.Translation.Get("menu.slayer.slayersyntaxunknown"));

            Game1.activeClickableMenu = new HunterMenu(this, loadedSlayerData, locs, 10, 10, 100, 100);
        }
    }
}
