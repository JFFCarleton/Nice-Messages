using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;

namespace Nice_Messages
{
    class TownMessages
    {
        /*
         * This class will be used to display messages when the player goes into differnt areas of town
         * MIN WORKING PROTOTYPE:
         *      - Lets get a thing going that'll read the location on warp.
         *          -Main locations I want to hit are stores and festivals.
         *          -Locations. are they objects? Should I be using the objects as keys or should I tumble them?
         *      -Should pop in a flag to see if we've visted a certain place already
         *      
         *      
         *      - Also lets see if we can center the hud messages.
         * EXTENDED IDEAS:
         *      - Could have a config that sets likelyhood of messages seperately based on location
         */






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
