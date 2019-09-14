using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Menus;

namespace SilverHunterMod
{
    public class HunterMenu : IClickableMenu
    {
        private const int Width = 800;
        private const int ClusterOffset = 50;
        private readonly Dictionary<string, string> Locs;
        private readonly SlayerData SlayerData;
        private readonly Rectangle Inbounds;
        private readonly Vector2 Offsets;
        private ModEntry Entry;

        public HunterMenu(ModEntry entry, SlayerData slayerData, Dictionary<string, string> locs, int x, int y, int width, int height) : base(Game1.activeClickableMenu.xPositionOnScreen, Game1.activeClickableMenu.yPositionOnScreen, Width, Game1.activeClickableMenu.height)
        {
            Entry = entry;
            Inbounds = new Rectangle(xPositionOnScreen + 50, yPositionOnScreen + 110, width - 100, height - 200);
            Offsets = new Vector2(375, 25);
            Locs = locs;
            SlayerData = slayerData;
        }

        public override void draw(SpriteBatch batch)
        {
            Game1.drawDialogueBox(xPositionOnScreen, yPositionOnScreen, width, height, false, true);
            SpriteText.drawString(batch, Locs["title"], Inbounds.X, Inbounds.Y);

            for (int i = 0; i < SlayerData.SlayerQuestClusters.Count; i++)
            {
                SlayerQuestCluster cluster = SlayerData.SlayerQuestClusters[i];
                drawMonsterCluster(batch, cluster, i);
            }
        }

        /// <summary>Draws the text for each monster slayer quest</summary>
        /// <param name="batch"></param>
        /// <param name="cluster"></param>
        /// <param name="index"></param>
        private void drawMonsterCluster(SpriteBatch batch, SlayerQuestCluster cluster, int index)
        {
            int amount = cluster.Entries.Sum(e => Game1.stats.specificMonstersKilled.ContainsKey(e) ? Game1.stats.specificMonstersKilled[e] : 0);

            SpriteFont font = Game1.smallFont;
            Vector2 position = new Vector2(index % 2 == 0 ? Inbounds.X : Inbounds.X + Offsets.X,
                Inbounds.Y + Offsets.Y + ClusterOffset * (index / 2 + 1));
            if (amount == 0)
            {
                Utility.drawTextWithShadow(batch,
                    Locs["UnknownMonsterString"],
                    font,
                    position,
                    Game1.textColor * 0.33f,
                    1f,
                    0.1f);
            }
            else if (amount < cluster.KillsRequired)
            {
                Utility.drawTextWithShadow(batch,
                    $"{amount} / {cluster.KillsRequired} {cluster.Name}",
                    font,
                    position,
                    Game1.textColor,
                    1f,
                    0.1f);
            }
            else
            {
                Utility.drawTextWithShadow(batch,
                    $"{amount} {cluster.Name} *",
                    font,
                    position,
                    Game1.textColor,
                    1f,
                    0.1f);
            }
        }
    }
}
