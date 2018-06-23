using System;
//using FNA;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace BigGrass {
    
    public class ModEntry : Mod {

        public override void Entry(IModHelper helper) {

            SaveEvents.AfterLoad += this.LoadHook;
            //InputEvents.ButtonPressed += this.InputHook;

            helper.ConsoleCommands.Add("sayhi", "Says hi", this.SayHi);
        }

        private void LoadHook(object sender, EventArgs e) {

            this.Monitor.Log("LoadHook executed");
        }

        private void InputHook(object sender, EventArgsInput e) {

            if(!Context.IsWorldReady) return;

            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}");
        }

        private void SayHi(string command, string[] args) {

            this.Monitor.Log("Hi");
        }

        /// Gets the tile the mouse cursor is at
        private Vector2 GetMouseTile() {
            
            Vector2 vector2 = new Vector2(Game1.getOldMouseX() + Game1.viewport.X,
                                          Game1.getOldMouseY() + Game1.viewport.Y) / Game1.tileSize;
        
            if (Game1.mouseCursorTransparency == 0.0 || !Game1.wasMouseVisibleThisFrame || !Game1.lastCursorMotionWasMouse &&
                (Game1.player.ActiveObject == null || !Game1.player.ActiveObject.isPlaceable() && Game1.player.ActiveObject.Category != -74)) {
            
                vector2 = Game1.player.GetGrabTile();
                if (vector2.Equals(Game1.player.getTileLocation())) {
                    
                    vector2 = Utility.getTranslatedVector2(vector2, Game1.player.facingDirection, 1f);
                }
            }
        
            if (!Utility.tileWithinRadiusOfPlayer((int) vector2.X, (int) vector2.Y, 1, Game1.player)) {
            
                vector2 = Game1.player.GetGrabTile();
                if (vector2.Equals(Game1.player.getTileLocation()) && Game1.isAnyGamePadButtonBeingPressed()) {
                    
                    vector2 = Utility.getTranslatedVector2(vector2, Game1.player.facingDirection, 1f);
                }
            }
        
        return vector2;
        }
    }
}
