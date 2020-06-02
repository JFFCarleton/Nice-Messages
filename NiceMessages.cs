using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;

namespace Nice_Messages
{
    class NiceMessages {
        private Dictionary< string, string[] > unifiedMessages;
        private IModHelper modelHelper;

        public NiceMessages(IModHelper helper)
        {
            this.modelHelper = helper;
            this.unifiedMessages = modelHelper.Content.Load<Dictionary<string,string[]>>("unifiedMessages.json", ContentSource.ModFolder);
        }

        /*
         * Takes the indentified weather as a key and accesses the correct table, returning a random message
         * based on the number of messages in the message table array.
         * result of the random number is subtracted by 1 to correct for index starting at 0
         */

        public string getMorningMessage(string currSeason, int weatherIcon, int currDay)
        {
            string weatherKey = identifyWeather(currSeason, weatherIcon, currDay);
            string[] msgTable = unifiedMessages[weatherKey];
            return msgTable[new Random().Next(msgTable.Length - 1)];
        }


        //Creates a key to be used by final dictonary.
        //Keys are strings in "<season>/<weather>" format except for festivals, since they are fixed seasons
        //      planning to add in section for fests after main functionality is working
        //Keys will be used to select the correct table of lines from the master dictonary

        /* 
         * VALD KEYS ********************************************************************
         * spring/wedding       summer/wedding      fall/wedding        winter/wedding  *
         * spring/sunny         summer/sunny        fall/sunny          winter/sunny    *
         * spring/windy                             fall/windy                          *
         * spring/rain          summer/rain         fall/rain                           *
         * spring/lightning     summer/lightning    fall/lightning                      *
         *                                                              winter/snow     *
         * ******************************************************************************
         */
        private string identifyWeather(string currSeason, int currWeather, int currDay) 
        {
            //have to check if night market happens here, b/c it doesn't use the fest icon.
            //if (currSeason == "winter" && ( currDay >= 15 && currDay <= 17 ) ) { return "nightMarket"; } 
            
            switch (currWeather) 
            {
                case 0:     return currSeason+"/sunny";
                case 1:     return currSeason+"/sunny"; 
                case 2:     return currSeason+"/sunny";
                case 3:     return "spring/windy";
                case 4:     return currSeason+"/rain";
                case 5:     return currSeason+"/lightning";
                case 6:     return "fall/windy";
                case 7:     return "winter/snow";
            }
            //put in exception handel here.
            return "error";
        }

        /*CUT FOR NOW: Will reimpliment in future version.
         * I don't think it fits with the "morning messages" idea. I will expand on this if I make messages for player entering
         * new areas.
         * When I get a icon number of "1", it means it's a festival day. since each festival, no matter the season, has a
         * unique day on which it takes place, all I have to do is check the number.
         
        private string identifyFest(int dayOfMonth)
        {
            switch (dayOfMonth)
            {
                case 13:    return "eggFest";
                case 24:    return "flowerDance";
                case 11:    return "luau";
                case 28:    return "moonlightJellies";
                case 16:    return "stardewValleyFair";
                case 27:    return "spiritsEve";
                case 8:     return "festOfIce";
                case 25:    return "winterStar";
            }
            return "error";
        }
        */

    }//end of class
}//end of namespace
