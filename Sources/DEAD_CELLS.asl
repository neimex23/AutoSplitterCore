/* Dead Cells Autosplitter (19-AUG-2024)
 * Maintained by R30hedron (@R30hedron#9520 on Discord)
 * Special thanks to Mintys (@Minty#4831) and Blargel (@Blargel#0213) for previously creating/maintaining the autosplitter.
 * Thanks to Midknight13 (@Midknight13#3966) for verifying GOG version addresses
 * Thanks to Evian (@Evian#6930) for verifying version addresses for 23.9 and 30.5
 * Thanks to Vord (@Vord#0016) for an older update 33 MD5 hash
 * 
 * Currently works for the following Dead Cells versions. Additional versions added by request.
 *     Steam v. 16.2
 *     Steam v. 17.1
 *     Steam v. 17.4
 *     Steam v. 18.5
 *     Steam v. 19.7
 *     Steam v. 20.8
 *     Steam v. 21.5
 *     Steam v. 22.0
 *     Steam v. 23.9
 *     Steam v. 24.3 
 *     Steam v. 25.4
 *     Steam v. 26.0
 *     Steam v. 27.1
 *     Steam v. 28.1
 *     Steam v. 29.3
 *     Steam v. 30.7
 *     Steam v. 31.5
 *     Steam v. 32.4
 *     Steam v. 33.3
 *     Steam v. 34.2
 *     Steam v. 35.8 (Current Patch)
 *
 *     GOG   v. 17.1
 */

 /*
  * Update notes:
  * To add a new version of the game to the autosplitter, do the following:
  * 1) Find the following memory addresses:
  *    - Stage name: Will be hooked into the "discord.hdll" module
  *    - Time: Will be hooked into the "libhl.dll" module.
  *    - X and Y coordinates for the player
  *       We need both the X and Y coordinates for the beheaded and the head separately.
  *       Easiest way to do so is to use the left door in the Throne Room, which should have an
  *       X coordinate of 490.8
  * 2) Add a new state, following the conventions below.
  * 3) Calculate the MD5 hashes for the version's deadcells.exe and deadcells_gl.exe and
  *    add them to the switch cases in the init function
  * 4) Add version number to list above
  */

state("deadcells", "16.2") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x49184, 0x434, 0x0, 0x58, 0x5C, 0x28;
    int      control : "libhl.dll", 0x49184, 0x434, 0x0, 0x58, 0x68, 0xF8, 0xA0;
    double   headx   : "libhl.dll", 0x49184, 0x434, 0x0, 0x58, 0x68, 0xF8, 0xA0, 0x200;
    double   playerx : "libhl.dll", 0x49184, 0x434, 0x0, 0x58, 0x64, 0x200;
    double   playery : "libhl.dll", 0x49184, 0x434, 0x0, 0x58, 0x64, 0x208;
    int      health  : "libhl.dll", 0x49184, 0x434, 0x0, 0x58, 0x64, 0xE8;
}

state("deadcells", "17.1, 17.4, or 18.5") {//Includes GoG 17.1
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x68, 0xF8, 0xA0;
    double   headx   : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x68, 0xF8, 0xA0, 0x200;
    double   playerx : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x64, 0x200;
    double   playery : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x64, 0x208;
    int      health  : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x64, 0xE8;
}

state("deadcells", "19.7") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x68, 0xF8, 0xA0;
    double   headx   : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x68, 0xF8, 0xA0, 0x210;
    double   playerx : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x64, 0x210;
    double   playery : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x64, 0x218;
    int      health  : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x64, 0xE8;
}

state("deadcells", "20.8") { //20.7
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x68, 0xFC, 0xA0;
    double   headx   : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x68, 0xFC, 0xA0, 0x218;
    double   playerx : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x64, 0x218;
    double   playery : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x64, 0x220;
    int      health  : "libhl.dll", 0x49184, 0x440, 0x0, 0x58, 0x64, 0xF0;
}

state("deadcells", "21.5") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x49184, 0x3C8, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x49184, 0x3C8, 0x0, 0x58, 0x68, 0x104, 0xA0;
    double   headx   : "libhl.dll", 0x49184, 0x3C8, 0x0, 0x58, 0x68, 0x104, 0xA0, 0x218;
    double   playerx : "libhl.dll", 0x49184, 0x3C8, 0x0, 0x58, 0x64, 0x218;
    double   playery : "libhl.dll", 0x49184, 0x3C8, 0x0, 0x58, 0x64, 0x220;
    int      health  : "libhl.dll", 0x49184, 0x3C8, 0x0, 0x58, 0x64, 0xF0;
}

state("deadcells", "22.0") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x49184, 0x3BC, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x49184, 0x3BC, 0x0, 0x58, 0x68, 0x104, 0xA0;
    double   headx   : "libhl.dll", 0x49184, 0x3BC, 0x0, 0x58, 0x68, 0x104, 0xA0, 0x208;
    double   playerx : "libhl.dll", 0x49184, 0x3BC, 0x0, 0x58, 0x64, 0x208;
    double   playery : "libhl.dll", 0x49184, 0x3BC, 0x0, 0x58, 0x64, 0x210;
    int      health  : "libhl.dll", 0x49184, 0x3BC, 0x0, 0x58, 0x64, 0xFC;
}

state("deadcells", "23.9") { //23.8
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x49184, 0x3B4, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x49184, 0x3B4, 0x0, 0x58, 0x68, 0x104, 0xA0;
    double   headx   : "libhl.dll", 0x49184, 0x3B4, 0x0, 0x58, 0x68, 0x104, 0xA0, 0x208;
    double   playerx : "libhl.dll", 0x49184, 0x3B4, 0x0, 0x58, 0x64, 0x208;
    double   playery : "libhl.dll", 0x49184, 0x3B4, 0x0, 0x58, 0x64, 0x210;
    int      health  : "libhl.dll", 0x49184, 0x3B4, 0x0, 0x58, 0x64, 0xFC;
}

state("deadcells", "24.3") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x3B8, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x48184, 0x3B8, 0x0, 0x58, 0x68, 0x108, 0xA0;
    double   headx   : "libhl.dll", 0x48184, 0x3B8, 0x0, 0x58, 0x68, 0x108, 0xA0, 0x208;
    double   playerx : "libhl.dll", 0x48184, 0x3B8, 0x0, 0x58, 0x64, 0x208;
    double   playery : "libhl.dll", 0x48184, 0x3B8, 0x0, 0x58, 0x64, 0x210;
    int      health  : "libhl.dll", 0x48184, 0x3B8, 0x0, 0x58, 0x64, 0xFC;
}

state("deadcells", "25.4") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x68, 0x108, 0xA0;
    double   headx   : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x68, 0x108, 0xA0, 0x208;
    double   playerx : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x208;
    double   playery : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x210;
    int      health  : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0xFC;
}

state("deadcells", "26.1") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x434, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x48184, 0x434, 0x0, 0x58, 0x68, 0x108, 0xA0;
    double   headx   : "libhl.dll", 0x48184, 0x434, 0x0, 0x58, 0x68, 0x108, 0xA0, 0x208;
    double   playerx : "libhl.dll", 0x48184, 0x434, 0x0, 0x58, 0x64, 0x208;
    double   playery : "libhl.dll", 0x48184, 0x434, 0x0, 0x58, 0x64, 0x210;
    int      health  : "libhl.dll", 0x48184, 0x434, 0x0, 0x58, 0x64, 0xFC;
}

state("deadcells", "27.1") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x420, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x48184, 0x420, 0x0, 0x58, 0x68, 0x10C, 0xA0;
    double   headx   : "libhl.dll", 0x48184, 0x420, 0x0, 0x58, 0x68, 0x10C, 0xA0, 0x208;
    double   playerx : "libhl.dll", 0x48184, 0x420, 0x0, 0x58, 0x64, 0x208;
    double   playery : "libhl.dll", 0x48184, 0x420, 0x0, 0x58, 0x64, 0x210;
    int      health  : "libhl.dll", 0x48184, 0x420, 0x0, 0x58, 0x64, 0xFC;
}

state("deadcells", "28.1") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x68, 0x10C, 0xA0;
    double   headx   : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x68, 0x10C, 0xA0, 0x208;
    double   playerx : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x64, 0x208;
    double   playery : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x64, 0x210;
    int      health  : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x64, 0xFC;
}

state("deadcells", "29.3") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x68, 0x110, 0xA0;
    double   headx   : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x68, 0x110, 0xA0, 0x210;
    double   playerx : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x64, 0x210;
    double   playery : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x64, 0x218;
    int      health  : "libhl.dll", 0x48184, 0x428, 0x0, 0x58, 0x64, 0x104;
}

state("deadcells", "30.7") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x68, 0x110, 0xA0;
    double   headx   : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x68, 0x110, 0xA0, 0x210;
    double   playerx : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x210;
    double   playery : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x218;
    int      health  : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x104;
}


state("deadcells", "31.5") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x68, 0x110, 0xA0;
    double   headx   : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x68, 0x110, 0xA0, 0x218;
    double   playerx : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x218;
    double   playery : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x220;
    int      health  : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x104;
}

state("deadcells", "32.4") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x5C, 0x20;
    int      control : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x68, 0x110, 0xA0;
    double   headx   : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x68, 0x110, 0xA0, 0x220;
    double   playerx : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x220;
    double   playery : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x228;
    int      health  : "libhl.dll", 0x48184, 0x42C, 0x0, 0x58, 0x64, 0x10C;
}

state("deadcells", "33.3") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x49184, 0x450, 0x0, 0x58, 0x60, 0x20;
    int      control : "libhl.dll", 0x49184, 0x450, 0x0, 0x58, 0x6C, 0x118, 0xB0;
    double   headx   : "libhl.dll", 0x49184, 0x450, 0x0, 0x58, 0x6C, 0x118, 0xB0, 0x260;
    double   playerx : "libhl.dll", 0x49184, 0x450, 0x0, 0x58, 0x68, 0x260;
    double   playery : "libhl.dll", 0x49184, 0x450, 0x0, 0x58, 0x68, 0x268;
    int      health  : "libhl.dll", 0x49184, 0x450, 0x0, 0x58, 0x68, 0x124;
}

state("deadcells", "34.x") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x498, 0x0, 0x58, 0x60, 0x20;
    int      control : "libhl.dll", 0x48184, 0x498, 0x0, 0x58, 0x6C, 0x118, 0xB0;
    double   headx   : "libhl.dll", 0x48184, 0x498, 0x0, 0x58, 0x6C, 0x118, 0xB0, 0x268;
    double   playerx : "libhl.dll", 0x48184, 0x498, 0x0, 0x58, 0x68, 0x268;
    double   playery : "libhl.dll", 0x48184, 0x498, 0x0, 0x58, 0x68, 0x270;
    int      health  : "libhl.dll", 0x48184, 0x498, 0x0, 0x58, 0x68, 0x12C;
}

state("deadcells", "35.8") {
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x60, 0x20;
    int      control : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x6C, 0x118, 0xB0;
    double   headx   : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x6C, 0x118, 0xB0, 0x268;
    double   playerx : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x68, 0x268;
    double   playery : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x68, 0x270;
    int      health  : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x68, 0x12C;
}

state("deadcells", "Unknown Version") {
    //Default to 35.x
    string6  stage   : "discord.hdll", 0x1574, 0x1C;
    double   time    : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x60, 0x20;
    int      control : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x6C, 0x118, 0xB0;
    double   headx   : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x6C, 0x118, 0xB0, 0x268;
    double   playerx : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x68, 0x268;
    double   playery : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x68, 0x270;
    int      health  : "libhl.dll", 0x48184, 0x4B0, 0x0, 0x58, 0x68, 0x12C;
}

/* Variable Info
 * [stage]   : Contains the current level name. Used for splits
 * 
 * [time]    : Stores the current in-game timer value.
 *
 * [control] : Memory address for what object is controlled by the player.
 *             If equal to zero, player does not have control.
 *
 * [headx]   : Stores the x coordinates for the head of the Beheaded.
 *             headx > 2020 means you just (probably) barely entered the fountain
 * 
 * [playerx]
 * [playery] : Stores the x and y coordinates for the Beheaded, but not the head.
 *             Used to determine if the player's body is in a certain region to mean you've ended the run
 *             playerx > 1465 means you are far enough forward in Throne Room
 *             playery < 1100 means you are in the final room in the Collector boss fight
 * 
 * [health]  : Stores current player health
 *             Used as a failsafe to prevent the timer splitting if the player dies in the Collector boss fight
 */

gameTime {
    //runs at all times when the timer is running.
    //returns in-game time
    
    if (settings["igtreset"])
    {
        return TimeSpan.FromSeconds(vars.igt);
    }
    else
    {
        return TimeSpan.FromSeconds(current.time);
    }
}

isLoading {
    return true;
}

startup
{
    //runs when starting livesplit
    //options added here
    
    //Localization strings
    vars.pq = new List<string> {
        "Prison", // English
        "Quarti", // French
        "Celdas", // Spanish
        "Тюр", // Russian
        "被囚" // Chinese
    };
    vars.passage = new List<string> {
        "Passag", // English
        "Path t",
        "Exit t",
      //"Passag", // French
        "Chemin",
        "Sortir",
        "Pasaje", // Spanish
        "Camino",
        "Sal de",
        "Про", // Russian
        "Пут",
        "Пок",
        "前往", // Chinese
        "离开"
    };
    vars.throne = new List<string> {
        "Throne", // English
        "Salle ", // French
        "Sala d", // Spanish
        "Тро", // Russian
        "王座" // Chinese
    };
    vars.observatory = new List<string> {
        "Observ", // English
      //"Observ", // French
      //"Observ", // Spanish
        "Обс", // Russian
        "观星" // Chinese
    };
    vars.crown = new List<string> {
        "The Cr", // English
        "La Cou", // French
        "La cor", // Spanish
        "Кор",    // Russian
        "塔顶" // Chinese
    };
    
    //autosplitter variable for additive in-game time
    vars.igt = 0f;
    
    settings.Add("enter", false, "Split on entering transition");
    settings.SetToolTip("enter", "Enable splits on entering \"Passage to\" areas.");
    
    settings.Add("leave", true, "Split on leaving transition");
    settings.SetToolTip("leave", "Enable splits on leaving \"Passage to\" areas (on by default).");
    
    settings.Add("debug", false, "Print debug statements");
    settings.SetToolTip("debug", "Prints debug statements to console; can be viewed through DebugView.\n"
                               + "You should only enable this if you are helping to debug the autosplitter.");
    settings.Add("testing", false, "Experimental Bugfixes");
    settings.SetToolTip("testing", "Changes some of the autosplitter's internals to test new changes.\n"
                                 + "Only enable this if R30hedron specifically asks you to to test a bug fix.\n"
                                 + "Currently testing: end of Throne Room split after HotK fight for 0-5BC.");
    settings.Add("igtreset", false, "Don't clear IGT on new run");
    settings.SetToolTip("igtreset", "Compare against In-Game Timer will be additive on new runs.\n"
                                  + "Use this setting if running a category that requires multiple 'runs',\n"
                                  + "such as 0-5BC or All Runes.\n"
                                  + "Note: This setting will automatically disable automatic resets.");
                              
}

init
{
    //runs once when starting Dead Cells.
    //Used to determine version
    //print("init");
    
    //md5 checksum code adapted from Zment's Defy Gravity autosplitter
    
    byte[] exeMD5HashBytes = new byte[0];
    using (var md5 = System.Security.Cryptography.MD5.Create())
    {
        using (var s = File.Open(modules.First().FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            exeMD5HashBytes = md5.ComputeHash(s);
        }
    }
    
    var MD5Hash = exeMD5HashBytes.Select(x => x.ToString("X2")).Aggregate((a, b) => a + b);
    vars.MD5Hash = MD5Hash;
    print("MD5: " + MD5Hash);
    
    
    switch (MD5Hash)
    {
        case "44775C673E44220D82B54A43DDE45606": //16.2
            version = "16.2";
            break;
        case "CE5EE20A1358B472222717129B6BC130": //17.1
        case "CA7B1C70879BC1431C74AE914B9DDA3B": //17.4
        case "B09AA0C6C1B9E323F3DF47ECC6AE40FD": //17.1 GOG
        case "0CE0734F106EBC3DCC2AA6195B221F0F": //18.5
            version = "17.1, 17.4, or 18.5";
            break;
        case "3124766B0AF66816797E8181BF4A1298": //19.7
        case "26FC75193F4B530DE0AE7082F9C7E32E": //19.7 hotfix
        case "2374914D7B73B09EDDDD21778F0DBF07": //19.7 hotfix 2
            version = "19.7";
            break;
        case "E8C26AEEC84DFE64D30AFB25F2972C67": //20.7
        case "52852A88C226BC11CC087BAE5EA748C8": //20.8
            version = "20.8";
            break;
        case "B3645A26A4F1D72080269719B927E0CE": //21.5
            version = "21.5";
            break;
        case "58BEAB263FFE1BDE26161C8743083466": //22.0
            version = "22.0";
            break;
        case "3D7DD501610C37672FEDBDEDEFD78989": //23.8
        case "E31E422C84F968475715489AED00871D": //23.9
            version = "23.9";
            break;
        case "B5409CCCEB559C8E6DFA2BE4AC7FB2B8": //24.2
        case "6789DF62008784E7D883E6CDEA6D3BFA": //24.3
            version = "24.3";
            break;
        case "53C6C30F92AFAF958F4FBB5D0EF3FE3C": //25.3
        case "E9318DC2C41BD686F1373D2426AB7CC2": //25.4
            version = "25.4";
            break;
        case "2D8B6EF259E1BAF4C67C1867711E3351": //26.0_PQ_WrongWarps WoOooo.
        case "9FC4601B85490E38588F7C4CB47CED6E": //26.0
        case "F2B5DDF1E3E8DE6F78302681C0D3E72A": //26.1
            version = "26.1";
            break;
        case "58A4E85AA72B53E7F86E84DDA6C98747": //27.0
        case "480E92CB2564EDD0E402D1FA8864246E": //27.1
            version = "27.1";
            break;
        case "86BA459E2ED4653B0536AC978F9253B0": //28.0
        case "2BE4C82A88FA92299DCDFD56C7E81316": //28.1
        case "FC40A5399202FD1A1F4455A2E18CC123": //28.1 hotfix?
            version = "28.1";
            break;
        case "62A0676829E73C7E770D08D3FEF80E04": //29.2
        case "A787AA380C1CAB266C091A52ADF846A3": //29.3
        case "96B7F75B293AE7BA312C1B8A10CA7372": //29.3 hotfix
            version = "29.3";
            break;
        case "305741518727D862D0BF0E59A2EB3E94": //30.4
        case "F2523CB328FCA755D9B0BE0545BF1230": //30.5 (hotfix?)
        case "C07D7CBE64F0DDE40F45136CC72D4C38": //30.7
            version = "30.7";
            break;
        case "B9F64A4F597632247F88C8C854ACB8C9": //31.3
        case "3B5E290A98799390BE8778AEA45A0264": //31.5 (hotfix)
            version = "31.5";
            break;
        case "A05203DF0EB6FE63BA85421DBE0BA95C": //32.3
        case "954D76713585F5180167B7CF50273003": //32.4
            version = "32.4";
            break;
        case "5748B8BD4DC4107480B3258C9BB91660": //33.2?
        case "2CD38966576F214F573A2F31292B38B3": //33.3
            version = "33.3";
            break;
        case "975026672E1F73032B77C72827354180": //34.1
        case "B49D2F2E628A80FF7E4301D4203ED21A": //34.1 (hotfix)
        case "858E524DA4FEDAB2090853E86D7B200B": //34.x (JUN hotfix?)
            version = "34.x";
            break;
        case "62B5B1FF7B2A89296BD0044B94A53C7B": //35.8
        case "BB1F4791BB84E3997BD026F375F83ABD": //35.8 (hotfix?)
            version = "35.8";
            break;
        default:
            version = "Unknown Version";
            MessageBox.Show(timer.Form,
                "Dead Cells Autosplitter Error:\n\n"
                + "This autosplitter does not support this game version.\n"
                + "Please contact R30hedron (@R30hedron#9520 on Discord)\n"
                + "with the following string and the game's version number.\n\n"
                + "MD5Hash: " + MD5Hash + "\n\n"
                + "Defaulting to the most recent known memory addesses...",
                  "Dead Cells Autosplitter Error",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
            break;
    }
}

update
{
    if (settings["debug"])
    {
        print("--------------------------------------------------\n" + 
        "MD5    : " + vars.MD5Hash + "\n" +
        "stage  : " + old.stage   + " / " + current.stage + "\n" +
        "time   : " + old.time    + " / " + current.time + "\n" +
        "control: " + old.control + " / " + current.control + "\n" +
        "headx  : " + old.headx   + " / " + current.headx + "\n" +
        "playerx: " + old.playerx + " / " + current.playerx + "\n" +
        "playery: " + old.playery + " / " + current.playery + "\n" +
        "health : " + old.health  + " / " + current.health);
    }
    
    //code pulled from Doom64 autosplitter
    double delta = current.time - old.time;
    if (delta < 0)
    {
        delta = 0;
    }
    vars.igt += delta;
}

reset
{
    //runs repeatedly when timer is running.
    //if true, reset splitter
    
    if (settings["debug"] && (current.time == 0 && vars.pq.Contains(current.stage)))
    {
        print("DEBUG: Reset");
    }
    
    if (settings["igtreset"])
    {
        return false;
    }
    
    return current.time == 0 && vars.pq.Contains(current.stage);
}

start
{
    //runs repeatedly when timer is at 0.0 and ready to start.
    //if true, splitter will start
    
    if (settings["debug"] && (old.time == 0 && current.time > 00 && current.time < 0.1))
    {
        print("DEBUG: Start");
    }
    
    if (settings["igtreset"] && (old.time == 0 && current.time > 00 && current.time < 0.1))
    {
        vars.igt = 0f;
    }
    
    return old.time == 0 && current.time > 00 && current.time < 0.1;
}

split
{
    //runs repeatedly when timer is running.
    //if true, split.
    
    //Check if entering or leaving the intermediate areas.
    var enterPassage = false;
    var exitPassage  = false;
    
    if (settings["enter"])
    {
    	enterPassage = current.stage != old.stage && vars.passage.Contains(current.stage);
    } 
    if (settings["leave"])
    {
    	exitPassage  = current.stage != old.stage && vars.passage.Contains(old.stage);
    }
    
    var exitFountain = false;
    
    if (settings["testing"])
    {

    //Check if player loses control in Throne Room and head x coord is different from beheaded x coord
    exitFountain  = vars.throne.Contains(current.stage) && 
                        old.headx > 1960 && //Check if head is far enough to the right
                        old.headx < 2390 && //Check if head is far enough to the left; prevent splits on 5BC door
                        current.playerx > 1460 && //Failsafe to ensure player is to the right of the arena
                        old.control != 0 && current.control == 0;
                        
    }
    else
    {
    
    //Check if player loses control in Throne Room and head x coord is different from beheaded x coord
    exitFountain  = vars.throne.Contains(current.stage) && 
                        old.headx > 2020 && //Check if head is far enough to the right
                        old.headx < 2230 && //Check if head is far enough to the left; prevent splits on 5BC door
                        current.playerx > 1495 && //Failsafe to ensure player is to the right of the arena
                        old.control != 0 && current.control == 0;
                        
    }
    
    //Check if player loses control in Observatory during final cutscene
    var killCollector = vars.observatory.Contains(current.stage) &&
                        old.playery < 1100 && //Check if player was still in the final areana location
                        current.playery < 1100 && //Check if player is in the final areana location
                        current.health != 0 && //Check if player is not dead
                        old.control != 0 && current.control == 0;

    var killQueen = vars.crown.Contains(current.stage) &&
                    old.playery < 650 &&
                    current.playery < 650 &&
                    current.health != 0 &&
                    old.control != 0 && current.control == 0;

    var isSplit = false;
    isSplit = enterPassage || exitPassage || exitFountain || killCollector || killQueen;
    
    if (settings["debug"] && isSplit)
    {
        print("DEBUG: Split");
    }
    
    return isSplit;
}
