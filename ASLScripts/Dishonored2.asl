state("Dishonored2")
{
    // default unknown version
}

state("Dishonored2", "1.3")
{
    // 1.75.0.12
    // 166068224
    bool isLoading : 0x26BD368; // 26BD374
    float x : 0x26BD688, 0x1E8960, 0xE50, 0x540, 0x10, 0x40, 0x0, 0x3C;
    float y : 0x26BD688, 0x1E8960, 0xE50, 0x540, 0x10, 0x40, 0x0, 0x40;
    float z : 0x26BD688, 0x1E8960, 0xE50, 0x540, 0x10, 0x40, 0x0, 0x44;
    string128 levelName : 0x3E04E50;
    float screenFade : 0x2518A50, 0xEEF38, 0x34;
    uint interaction : 0x2566CA8, 0x1F6EB0, 0x88, 0x80, 0x4;
    uint canInteract : 0x2566CA8, 0x1F6EB0, 0xE90, 0x0;
}

state("Dishonored2", "1.4")
{
    // 1.76.0.18
    // 163115008
    bool isLoading : 0x26DF8E8; // 26DF8F4
    float x : 0x26DFC08, 0x1E8960, 0xE50, 0x540, 0x10, 0x40, 0x0, 0x3C;
    float y : 0x26DFC08, 0x1E8960, 0xE50, 0x540, 0x10, 0x40, 0x0, 0x40;
    float z : 0x26DFC08, 0x1E8960, 0xE50, 0x540, 0x10, 0x40, 0x0, 0x44;
    string128 levelName : 0x3E69FF0;
    float screenFade : 0x253B0D0, 0xEEF38, 0x34;
    uint interaction : 0x26DFC08, 0x1F6EC0, 0x88, 0x80, 0x4;
    uint canInteract : 0x26DFC08, 0x1F6EC0, 0xE90, 0x0;
}

state("Dishonored2", "1.9")
{
    // 1.77.9.0
    // 70369280
    bool isLoading : 0x288D2E8;// Alternatively 0x3BDA0A8
    float x : 0x288D608, 0x1E8960, 0xE58, 0x540, 0x10, 0x40, 0x0, 0x3C;
    float y : 0x288D608, 0x1E8960, 0xE58, 0x540, 0x10, 0x40, 0x0, 0x40;
    float z : 0x288D608, 0x1E8960, 0xE58, 0x540, 0x10, 0x40, 0x0, 0x44;
    string128 levelName : 0x4025C50;
    float screenFade : 0x26C2B10, 0xEEF38, 0x34;
    uint interaction : 0x288D608, 0x1F6EE8, 0x88, 0x80, 0x4;
    uint canInteract : 0x288D608, 0x1F6EE8, 0x13B0, 0x0;
}

state("Dishonored2_x64ShippingRetail", "1.9ms")
{
    // 1.77.9.0
    // 113065984
    bool isLoading : 0x4A685A8;
    //float x : 0x51f57d4; //camx
    //float y : 0x51f57d8; //camy
    //float z : 0x51f57dc; //camz
	float x : 0x48A3318, 0x10, 0x330, 0x78;  //player pos from arkPlayer object
    float y : 0x48A3318, 0x10, 0x330, 0x7c;
    float z : 0x48A3318, 0x10, 0x330, 0x80;
    string128 levelName : 0x5841040;
    float screenFade : 0x61c7b50, 0, 8, 0x7DC;
    uint interaction : 0x4A10448, 0x6D0, 0x10, 0xa0, 0x88, 0x80, 4;
    uint canInteract : 0x4A10448, 0x6D0, 0x10, 0xa0, 0x13b0, 0;
}

state("Dishonored2_x64ShippingRetail", "1.9gp")
{
    // 1.77.9.0
    // 113065984
    bool isLoading : 0x4A685A8;
	float x : 0x045F7C40, 0x8, 0x8, 0x10, 0x268, 0xE0, 0x3C;
	float y : 0x045F7C40, 0x8, 0x8, 0x250, 0x268, 0xE0, 0x40;
	float z : 0x045F7C40, 0x8, 0x8, 0x250, 0x268, 0xE0, 0x44;
    string128 levelName : 0x5841040;
    float screenFade : 0x61c7b50, 0, 8, 0x7DC;
    uint interaction : 0x4A10448, 0x6D0, 0x10, 0xa0, 0x88, 0x80, 4;
    uint canInteract : 0x4A10448, 0x6D0, 0x10, 0xa0, 0x13b0, 0;
}


startup {
    // "campaign/bunker/bunker_p_lowchaos" is used before killing/darting stilton regardless of chaos
    // "campaign/bunker/bunker_p_highchaos" is used after regardless of chaos
    vars.autoSplits = new Tuple<string,string,bool>[]{
        Tuple.Create("A Strange Visit"       ,"campaign/void/void_01/void_01_p"                   ,true ),
        Tuple.Create("Edge of the World"     ,"campaign/boat/edge_of_the_world/boat_eotw_p"       ,false),
        Tuple.Create("The Good Doctor"       ,"campaign/institute/institute_p_"                   ,true ),
        Tuple.Create("The Lockwork Mansion"  ,"campaign/boat/madinventor/boat_madinventor_p"      ,true ),
        Tuple.Create("Another Strange Visit" ,"campaign/void/void_02/void_02_p"                   ,true ),
        Tuple.Create("The Royal Conservatory","campaign/boat/curator/boat_curator_p"              ,false),
        Tuple.Create("Dust District"         ,"campaign/boat/dust/boat_dust_p"                    ,true ),
        Tuple.Create("A Crack in the Slab"   ,"campaign/bunker/bunker_p_lowchaos"                 ,true ),
        Tuple.Create("The Grand Palace"      ,"campaign/boat/duke_palace/boat_palace_p"           ,true ),
        Tuple.Create("Death to the Empress"  ,"campaign/boat/dunwall_return/boat_dunwall_return_p",true ),
    };

    int i = 0;
    foreach (var autoSplit in vars.autoSplits) {
        settings.Add("autosplit_"+i.ToString(),autoSplit.Item3,"Split on \""+autoSplit.Item1+"\" start");

        ++i;
    }

    settings.Add("autosplit_end",true,"Split on run end");
    settings.Add("gamepass_support",true,"Game pass support");
    settings.SetToolTip("gamepass_support", "When unchecked support \"Windows Store\" version instead");

    vars.autoSplitIndex = -1;
}

init
{
    switch (modules.First().ModuleMemorySize) {
		case 113065984: 
				if(settings["gamepass_support"])
				{ version = "1.9gp"; break; } // Gamepass version
				else
				{ version = "1.9ms"; break; } // Windows Store
        case  70369280: version = "1.9"; break;
        case 163115008: version = "1.4"; break;
        case 166068224: version = "1.3"; break;
        default:        version = ""; break;
    }

    // print(modules.First().FileVersionInfo.FileVersion);
    // print(modules.First().ModuleMemorySize.ToString());
	
	

    if (vars.autoSplitIndex == -1) {
        for (vars.autoSplitIndex = 0;vars.autoSplitIndex < vars.autoSplits.Length;++vars.autoSplitIndex) {
            if (settings["autosplit_"+vars.autoSplitIndex.ToString()]) {
                break;
            }
        }
    }
}

exit
{
    timer.IsGameTimePaused = true;
}

isLoading
{
    return current.isLoading;
}

update
{
    const float posX =  -28.09f,
                posY = -153.96f,
                posZ =   66.40f,
                delta =   0.005f;

    if (old.isLoading || current.isLoading) {
        vars.runStarting =
            current.levelName.Equals("campaign/dunwall/escape/tower/dunwall_escape_tower_p") &&
        // Check if inside starting area.
            posX-delta < current.x && current.x < posX+delta &&
            posY-delta < current.y && current.y < posY+delta &&
            posZ-delta < current.z && current.z < posZ+delta;

        if(vars.runStarting){
            for (vars.autoSplitIndex = 0;vars.autoSplitIndex < vars.autoSplits.Length;++vars.autoSplitIndex) {
                if (settings["autosplit_"+vars.autoSplitIndex.ToString()]) {
                    break;
                }
            }
        }
    } else {
        vars.runStarting = false;
    }
}

reset
{
    return current.isLoading && vars.runStarting;
}

start
{
    return !current.isLoading && vars.runStarting;
}

split
{
    if (vars.autoSplitIndex < vars.autoSplits.Length) {
        if (current.isLoading && current.levelName.StartsWith(vars.autoSplits[vars.autoSplitIndex].Item2)) {
            for (++vars.autoSplitIndex;vars.autoSplitIndex < vars.autoSplits.Length;++vars.autoSplitIndex) {
                if (settings["autosplit_"+vars.autoSplitIndex.ToString()]) {
                    break;
                }
            }

            return true;
        }
    } else if(vars.autoSplitIndex == vars.autoSplits.Length && settings["autosplit_end"]) {
        bool isFadingOut = (old.screenFade < current.screenFade && current.screenFade < 1.0f);

        // For some reason 0x4B indicates that we can interact with something.
        // Also, if it just changed, we assume the user can interact.
        // This is necessary since `canInteract` is only updated on the second frame.
        bool canInteract = (current.canInteract == 0x4B || old.interaction != current.interaction);

        // 0x9C673C93 is the id (possibly a hash) for "Revive your father", 0xA2674605 is for "Revive your daughter".
        bool isFinalInteraction = (current.interaction == 0x9C673C93 || current.interaction == 0xA2674605);

        if(isFadingOut && canInteract && isFinalInteraction) {
            ++vars.autoSplitIndex;
            return true;
        }
    }

    return false;
}
