using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewValley;

namespace Nice_Messages{
    /*
     Class meant to take in infromation form a .json text file
     table will hold a key, which will be used to build an array
     from there, during gameplay, can make a key and check it vs the one held in the arr
     //TODO: get all message tables into an array.
     */
    class MessageTables {
        private MessageKey key;
        private string[] msgTable;
        private Dictionary<MessageKey, string[]> finalDict;
    }

    class MessageKey {
        public string season { get; set; }
        public int weatherNumber { get; set; }

        public MessageKey(string currSeason, int weatherNumb) {
            this.season = currSeason;
            this.weatherNumber = weatherNumb;
        }

    }
}