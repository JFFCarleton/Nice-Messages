using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Minigames;


namespace Nice_Messages
{
    public class NiceMessagesMain : Mod{
        private MorningMessages morningMessages;
        private TownMessages townMessages;
        private ModConfig Config;
        private IModHelper mainHelper;
        
        //SM Api set up
        public override void Entry(IModHelper mainHelper){
            this.mainHelper = mainHelper;
            this.Config = this.Helper.ReadConfig<ModConfig>();
            mainHelper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;
            mainHelper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
            mainHelper.Events.Player.Warped += Player_Warped;
        }
        //listeners
        private void GameLoop_SaveLoaded(object sender, SaveLoadedEventArgs e){
            this.morningMessages = new MorningMessages(mainHelper);
            this.townMessages = new TownMessages(mainHelper);
        }

        private void GameLoop_DayStarted(object sender, DayStartedEventArgs e){
            //chance a message will appear, editable in config.json
            if (Config.msgChance < new Random().Next(1, 99)) { return; }

            /* The below script will generate a string from the NiceMessages object, make it into a HUDmessage object,
             * then it will change the timeLeft (in miliseconds) attribute of that object according to the user's setting 
             * loaded form the config.json file. */
            try
            {
            HUDMessage morningMsg = new HUDMessage
                (morningMessages.getMorningMessage(Game1.currentSeason, Game1.weatherIcon), "");
            morningMsg.timeLeft = Config.morningMsgFadeOut;
            Game1.addHUDMessage(morningMsg);
            }
            
            catch (System.Collections.Generic.KeyNotFoundException) //Catch a bad formatting excption 
            {
                Monitor.Log("Invalid key: Make sure there are no spelling errors in the keys in unifiedMessages.json\n"+
                    "For a list of valid keys, please see the README", LogLevel.Error);
            }
        }

        /*
         * the idea was to use a single function to display messages in both cases, but the signature isn't the same
         * IE: morning looks for season and curr weather icon, where town would be looking at location
         * I mean, I guess I could put it all into one object and load all the logic in there, but I don't like that
         * In main I can just access the game stuff and pass it in,
        private HUDMessage getMsgToDisplay(Object messageObject) {
            return new HUDMessage("message", "");
        }*/
        private void Player_Warped(object sender, WarpedEventArgs e){
            //if (Context.CanPlayerMove) { return; }   revisit later  //if there's a cutscene, do nothing
            if (Config.twnMsgChance < new Random().Next(1,60) ) { return; } //set to 60 for testings, 100 percet chance

            GameLocation newLoc = Game1.currentLocation;


            //If there's an event, set the flag and then check it. true means that this is the 1st warp so return,
            //then reset the flag and now that it's false, we can safely do the correct warp/message display.
            if (Game1.CurrentEvent != null ) { 
                townMessages.dubWarpFlagSet();
                if (townMessages.doubleWarpFlag == true) { return; }
                
                //impliment the fest messages in this block.
                //      townMessages.getFestMsg(whichFest);
            }

            //i just need to pass the string location name.


            /*1. config roll chance {return;}
             *2. get and pass in the game location
             *3. display the message.
             *4. try/catch out of bounds key. put in message saying that event or location supported.*/


            try
            {
                Monitor.Log(townMessages.getTownMessage(newLoc.Name), LogLevel.Warn);
            }
            catch (System.Collections.Generic.KeyNotFoundException) 
            {
                Monitor.Log("key doesn't exit for this locaiton yet", LogLevel.Error);
            }
            
            
            Monitor.Log("Current location: " + newLoc.Name, LogLevel.Info);
            try
            {
                Monitor.Log("Current Fest: " + newLoc.currentEvent.FestivalName, LogLevel.Info);
            }
            catch (System.NullReferenceException) { Monitor.Log("Fest Name Null", LogLevel.Info); }

            //this'll show up during cutscenes. //TODO: Rememer the player is can move condition
            Game1.showGlobalMessage(morningMessages.getMorningMessage(Game1.currentSeason, Game1.weatherIcon));
        }

    }//end of class

    public class ModConfig {
        public int msgChance { get; set; } = 100;
        public int twnMsgChance { get; set; } = 100;
        public float morningMsgFadeOut { get; set; } = 5500;
        public float townMsgFadeOut { get; set; } = 5500;
    
    }//end of class
}//end of namespace
