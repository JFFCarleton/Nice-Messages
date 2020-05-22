using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Nice_Messages
{
    public class NiceMessagesMain : Mod
    {
        private NiceMessages niceMessages;
        private IModHelper oneHelpyBoi;
        private String currSeason;
        
        //SM Api set up
        public override void Entry(IModHelper oneHelpyBoi){
            this.oneHelpyBoi = oneHelpyBoi;
            oneHelpyBoi.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;
            oneHelpyBoi.Events.GameLoop.DayStarted += GameLoop_DayStarted;
            oneHelpyBoi.Events.GameLoop.DayEnding += GameLoop_DayEnding;
        }

        //listeners
        private void GameLoop_SaveLoaded(object sender, SaveLoadedEventArgs e){
            this.currSeason = Game1.currentSeason;
            this.niceMessages = new NiceMessages(currSeason, this.oneHelpyBoi);
            Game1.showGlobalMessage("Game Save Loaded!");
        }

        private void GameLoop_DayStarted(object sender, DayStartedEventArgs e){
            changeSeason(currSeason);
            Game1.showGlobalMessage("Hi i'm a test! "+currSeason+" "+niceMessages.randomMorningMessage());
        }
        
        private void GameLoop_DayEnding(object sender, DayEndingEventArgs e){
            return;
        }
        
        //methods
        private void changeSeason(String checkSeason) {
            if (this.currSeason == Game1.currentSeason) { return; }
            this.currSeason = Game1.currentSeason;
            this.niceMessages = new NiceMessages(currSeason, oneHelpyBoi);
        }
    
    }
}
