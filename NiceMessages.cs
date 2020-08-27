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
        private Dictionary< string, List<string> > weatherKeys;
        private IModHelper modelHelper;

        public NiceMessages(IModHelper helper)
        {
            this.modelHelper = helper;
        }

        //Takes all available message keys from the current translation file 
        //and puts them into different lists depending on the weather
        public void setMessagePool()
        {
            IEnumerable<Translation> unifiedMessages = modelHelper.Translation.GetTranslations();

            weatherKeys = new Dictionary<string, List<string>>() {
                { "spring/sunny", new List<string>() },
                { "spring/windy", new List<string>() },
                { "spring/rain", new List<string>() },
                { "spring/lightning", new List<string>() },
                { "summer/sunny", new List<string>() },
                { "summer/rain", new List<string>() },
                { "summer/lightning", new List<string>() },
                { "fall/sunny", new List<string>() },
                { "fall/windy", new List<string>() },
                { "fall/rain", new List<string>() },
                { "fall/lightning", new List<string>() },
                { "winter/sunny", new List<string>() },
                { "winter/snow", new List<string>() }
            };

            foreach (var message in unifiedMessages)
            {
                foreach (var weather in weatherKeys.Keys)
                {
                    if (message.Key.StartsWith(weather))
                    {
                        weatherKeys[weather].Add(message.Key);
                    }
                }
            }
        }

        /*
         * Takes the indentified weather as a key and accesses the correct table, returning a random message
         * based on the number of messages in the message table array.
         * result of the random number is subtracted by 1 to correct for index starting at 0
         */

        public string getMorningMessage(string currSeason, int weatherIcon)
        {
            string weather = identifyWeather(currSeason, weatherIcon);

            if (weatherKeys[weather].Count == 0) { return ""; }

            string messageKey = weatherKeys[weather][new Random().Next(weatherKeys[weather].Count - 1)];

            return modelHelper.Translation.Get(messageKey);
        }


        //Creates a key to be used by final dictonary.
        //Keys are strings in "<season>/<weather>" format except for festivals, since they are fixed seasons
        //Keys will be used to select the correct table of lines from the master dictonary

        /* 
         * VALD KEYS ********************************************************************
         * spring/sunny         summer/sunny        fall/sunny          winter/sunny    *
         * spring/windy                             fall/windy                          *
         * spring/rain          summer/rain         fall/rain                           *
         * spring/lightning     summer/lightning    fall/lightning                      *
         *                                                              winter/snow     *
         * ******************************************************************************
         */
        private string identifyWeather(string currSeason, int currWeather) 
        {   
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
            return null;
        }

    }//end of class
}//end of namespace
