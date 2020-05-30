using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Nice_Messages
{
    public class NiceMessagesMain : Mod
    {
        private NiceMessages niceMessages;
        private IModHelper mainHelper;
        
        //SM Api set up
        public override void Entry(IModHelper mainHelper){
            this.mainHelper = mainHelper;
            mainHelper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;
            mainHelper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
        }

        //listeners
        private void GameLoop_SaveLoaded(object sender, SaveLoadedEventArgs e){
            this.niceMessages = new NiceMessages(mainHelper);
        }

        private void GameLoop_DayStarted(object sender, DayStartedEventArgs e){
            try {
                Game1.showGlobalMessage(niceMessages.getMorningMessage(Game1.currentSeason, Game1.weatherIcon));
            }

            //Catch a bad formatting excption
            //TODO: Reevaluate exceptions that can be caught. new solution shouldn't have an odd index error like this.
                //NULL exception possible
            catch (System.Collections.Generic.KeyNotFoundException) {
                Monitor.Log("Invalid key: Make sure there are no spelling errors in the keys in unifiedMessages.json"+
                    "For a list of valid keys, please see the README", LogLevel.Error);
                //TODO: make the readme.
            }
        }

    }
}
