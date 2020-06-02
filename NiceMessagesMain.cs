using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Nice_Messages
{
    public class NiceMessagesMain : Mod{
        private NiceMessages niceMessages;
        private ModConfig Config;
        private IModHelper mainHelper;
        
        //SM Api set up
        public override void Entry(IModHelper mainHelper){
            this.mainHelper = mainHelper;
            this.Config = this.Helper.ReadConfig<ModConfig>();
            mainHelper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;
            mainHelper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
        }

        //listeners
        private void GameLoop_SaveLoaded(object sender, SaveLoadedEventArgs e){
            this.niceMessages = new NiceMessages(mainHelper);
        }

        private void GameLoop_DayStarted(object sender, DayStartedEventArgs e){
            //chance a message will appear, editable in config.json
            if (Config.msgChance < new Random().Next(1, 99)) { return; }
            try 
            { Game1.showGlobalMessage(niceMessages.getMorningMessage(Game1.currentSeason, Game1.weatherIcon)); }

            //Catch a bad formatting excption
            catch (System.Collections.Generic.KeyNotFoundException) {
                Monitor.Log("Invalid key: Make sure there are no spelling errors in the keys in unifiedMessages.json\n"+
                    "For a list of valid keys, please see the README", LogLevel.Error);
                //TODO: make the readme.
            }
        }

    }//end of class

    public class ModConfig {
        public int msgChance { get; set; } = 100;
    
    }//end of class
}//end of namespace
