state("Dishonored_DO", "1.1") {
    // 1.142.3.8
    // 188620800
    bool isLoading:      0x2809DC8;
    string128 levelName: 0x3FEB2B0;
}

state("Dishonored_DO", "1.2") {
    // 1.144.0.17
    // 194486272
    bool isLoading:      0x280AE48;
    string128 levelName: 0x3FEC390;
}

state("Dishonored_DO", "1.3") {
    // 1.145.0.0
    // 71852032
    bool isLoading:      0x2977E88;
    string128 levelName: 0x415CF50;
}

state("Dishonored_DO", "Epic") {
    // 1.145.0.0
    // 105369600
    bool isLoading:      0x4232168;
    string128 levelName: 0x500E3E0;
}

startup {
    vars.autoSplits = new Tuple<string,string>[]{
        Tuple.Create("Follow the Ink"     ,"dlc01/boat/boat_02/boat_02_p"     ),
        Tuple.Create("Quiet as a Moose"   ,"dlc01/boat/boat_03/boat_03_p"     ),
        Tuple.Create("The Stolen Archive" ,"dlc01/conservatory/conservatory_p"),
        Tuple.Create("A Hole in the World","dlc01/hollow/hollow_p"            ),
    };

    int i = 0;
    foreach(var autoSplit in vars.autoSplits){
        settings.Add("autosplit_"+i.ToString(),true,"Split on \""+autoSplit.Item1+"\" start");

        ++i;
    }

    vars.autoSplitIndex = -1;
}

init {
    switch (modules.First().ModuleMemorySize) {
        case 188620800: version = "1.1"; break;
        case 194486272: version = "1.2"; break;
        case 105369600: version = "Epic"; break;
        case  71852032: version = "1.3"; break;
        default:        version = "1.3"; break;
    }

    // print(modules.First().FileVersionInfo.FileVersion);
    // print(modules.First().ModuleMemorySize.ToString());

    if(vars.autoSplitIndex == -1){
        for(vars.autoSplitIndex = 0;vars.autoSplitIndex < vars.autoSplits.Length;++vars.autoSplitIndex){
            if(settings["autosplit_"+vars.autoSplitIndex.ToString()]){
                break;
            }
        }
    }
}

exit {
    timer.IsGameTimePaused = true;
}

isLoading {
    return current.isLoading;
}

update {
    if(old.isLoading || current.isLoading){
        vars.runStarting = current.levelName.Equals("dlc01/boat/boat_01/boat_01_p");

        if(vars.runStarting){
            for(vars.autoSplitIndex = 0;vars.autoSplitIndex < vars.autoSplits.Length;++vars.autoSplitIndex){
                if(settings["autosplit_"+vars.autoSplitIndex.ToString()]){
                    break;
                }
            }
        }
    }else{
        vars.runStarting = false;
    }
}

reset {
    return current.isLoading && vars.runStarting;
}

start {
    return !current.isLoading && vars.runStarting;
}

split {
    if(vars.autoSplitIndex < vars.autoSplits.Length){
        if(current.isLoading && current.levelName.StartsWith(vars.autoSplits[vars.autoSplitIndex].Item2)){
            for(++vars.autoSplitIndex;vars.autoSplitIndex < vars.autoSplits.Length;++vars.autoSplitIndex){
                if(settings["autosplit_"+vars.autoSplitIndex.ToString()]){
                    break;
                }
            }

            return true;
        }
    }

    return false;
}
