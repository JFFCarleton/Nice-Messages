using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using System.IO;
using System.Collections;

namespace Nice_Messages
{
    class NiceMessages {
        private string[] loadInMessages;
        private string[] newDayMessages;
        private IModHelper helperMcHelperson; //because this is a hobby project and I think he's a cute helper!

     
        public NiceMessages(string season, IModHelper helper){
                this.helperMcHelperson = helper;
                this.newDayMessages = File.ReadAllLines(
                    helperMcHelperson.Content.Load<string>(getNewSeasonMessages(season), ContentSource.ModFolder)
                );
        }
       
        private string getNewSeasonMessages(string season){
            switch (season){
                case "spring":
                    return "springMessages.json";

                case "summer":
                    return "summerMessages.json";

                case "fall":
                    return "fallMessages.json";

                case "winter":
                    return "winterMessages.json";
            }
                //Monitor.Log("no season found", LogLevel.Error);
            return null;
        }

        public string randomMorningMessage() {
            //todo, add in RNG. set to 0 for now for testing
            return this.newDayMessages[0];
        }
        public String[] getLoadInMessages() { return this.loadInMessages; }
    }
}
