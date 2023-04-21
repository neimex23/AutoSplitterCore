//Resident Evil 4 Remake Autosplitter V1.0.2 (07/04/2023)
//Supports IGT and Game Splits
//Script & Pointers by TheDementedSalad
//Special Thanks to:
//Yuushi & AvuKamu for going through the game and collecting data for splits

state("re4","Release")
{
   long GameElapsedTime		: 0xD234048, 0x18, 0x38;
   long DemoSpendingTime	: 0xD234048, 0x18, 0x40;
   long PauseSpendingTime	: 0xD234048, 0x18, 0x50;
   long ChapterTimeStart	: 0xD20FF80, 0x20, 0x10, 0x18;
   
   int Cutscene				: 0xD21B2C8, 0x17C;	
   int Chapter				: 0xD2368B0, 0x30;				//21100 Chapter 1, 21200 Chapter 2
   int Map					: 0xD2368B0, 0x38, 0x14;		//50500 next to the beginning car, 50501 after bushes (CampaignManager in REFramework)
   int ItemID				: 0xD22B258, 0xE0, 0xE8;
}

state("re4","7/4/23")
{
   long GameElapsedTime		: 0xD22D7D0, 0x18, 0x38;
   long DemoSpendingTime	: 0xD22D7D0, 0x18, 0x40;
   long PauseSpendingTime	: 0xD22D7D0, 0x18, 0x50;
   long ChapterTimeStart	: 0xD217780, 0x20, 0x10, 0x18;
	
   int Cutscene			: 0xD222610, 0x17C;	
   int Chapter			: 0xD22B018, 0x30;				//21100 Chapter 1, 21200 Chapter 2
   int Map			: 0xD22B018, 0x38, 0x14;		//50500 next to the beginning car, 50501 after bushes
   int ItemID			: 0xD22B240, 0xE0, 0xE8;
   
   byte DARank			: 0xD22B1A0, 0x10;
   float ActionPoint 		: 0xD22B1A0, 0x14;
   float ItemPoint		: 0xD22B1A0, 0x18;
}

init
{
	vars.StartTime = 0;
	vars.completedSplits = new List<int>();
	
	switch (modules.First().ModuleMemorySize)
	{
		case (548831232):
		case (538660864):
			version = "7/4/23";
			break;
		default:
			version = "Release";
			break;
	}
}

startup
{
	// Asks user to change to game time if LiveSplit is currently set to Real Time.
		if (timer.CurrentTimingMethod == TimingMethod.RealTime)
    {        
        var timingMessage = MessageBox.Show (
            "This game uses In Game Time as the main timing method.\n"+
            "LiveSplit is currently set to show Real Time (RTA).\n"+
            "Would you like to set the timing method to Game Time?",
            "LiveSplit | Resident Evil 4 (2023)",
            MessageBoxButtons.YesNo,MessageBoxIcon.Question
        );

        if (timingMessage == DialogResult.Yes)
        {
            timer.CurrentTimingMethod = TimingMethod.GameTime;
        }
    }
	
	settings.Add("Ch1", false, "Chapter 1");
	settings.CurrentDefaultParent = "Ch1";
	settings.Add("40510", false, "Reach Hunter's Cabin");
	settings.Add("10153", false, "Meet First Ganado");
	settings.Add("10154", false, "Dead Police Officer");
	settings.Add("10155", false, "Contact Hunnigan");
	settings.Add("40200", false, "Reach Village");
	settings.Add("10011", false, "La Campana");
	settings.Add("119235200", false, "Wooden Cog");
	settings.Add("43400", false, "Exit Tunnel");
	settings.Add("21200", false, "Finish Chapter 1");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch2", false, "Chapter 2");
	settings.CurrentDefaultParent = "Ch2";
	settings.Add("10128", false, "Meet Merchant");
	settings.Add("44400", false, "Enter TNT Valley");
	settings.Add("119203200", false, "Hexagonal Emblem");
	settings.Add("44201", false, "Open Hexagonal Emblem Door");
	settings.Add("119217600", false, "Crystal Marble");
	settings.Add("21300", false, "Finish Chapter 2");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch3", false, "Chapter 3");
	settings.CurrentDefaultParent = "Ch3";
	settings.Add("40212", false, "Enter Town Hall");
	settings.Add("10137", false, "Reach the Church");
	settings.Add("45400", false, "Reach Fishing Village");
	settings.Add("119273600", false, "Boat Fuel");
	settings.Add("10018", false, "Del Lago Start");
	settings.Add("22100", false, "Finish Chapter 3");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch4", false, "Chapter 4");
	settings.CurrentDefaultParent = "Ch4";
	settings.Add("119256000", false, "Old Wayshrine Key");
	settings.Add("46200", false, "Reach Apostate's Head Island");
	settings.Add("119232000", false, "Apostate's Head");
	settings.Add("46110", false, "Reach Blasphemer's Head Island");
	settings.Add("119230400", false, "Blasphemer's Head");
	settings.Add("277083456", false, "Bawk's Golden Egg");
	settings.Add("119257600", false, "Church Insignia");
	settings.Add("Merch", false, "Reach Merchant Dock");
	settings.Add("10022", false, "Gigante Start");
	settings.Add("20123", false, "Gigante End");
	settings.Add("Church", false, "Enter Church");
	settings.Add("22200", false, "Finish Chapter 4");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch5", false, "Chapter 5");
	settings.CurrentDefaultParent = "Ch5";
	settings.Add("Town", false, "Back to Town Hall");
	settings.Add("Barn", false, "Back to Barn House");
	settings.Add("10026", false, "Start Cabin");
	settings.Add("20132", false, "Cabin Part 2");
	settings.Add("22300", false, "Finish Chapter 5");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch6", false, "Chapter 6");
	settings.CurrentDefaultParent = "Ch6";
	settings.Add("47102", false, "Enter Checkpoint Area");
	settings.Add("10125", false, "Bella Sisters Start");
	settings.Add("119246400", false, "Checkpoint Crank");
	settings.Add("10126", false, "Start Mendez Escape");
	settings.Add("10127", false, "Bridge Collapse");
	settings.Add("10028", false, "Mendez Start");
	settings.Add("20111", false, "Mendez Phase 2 Start");
	settings.Add("20112", false, "Mendez Finish");
	settings.Add("23100", false, "Finish Chapter 6");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch7", false, "Chapter 7");
	settings.CurrentDefaultParent = "Ch7";
	settings.Add("50202", false, "Pass First Mandibula Plaga");
	settings.Add("10104", false, "Catapult Cutscene");
	settings.Add("50400", false, "Enter Castle");
	settings.Add("10114", false, "Meet Garrador");
	settings.Add("50601", false, "Enter Water Hall");
	settings.Add("119206400", false, "Halo Wheel");
	settings.Add("23200", false, "Finish Chapter 7");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch8", false, "Chapter 8");
	settings.CurrentDefaultParent = "Ch8";
	settings.Add("119209600", true, "Crimson Lantern");
	settings.Add("10036", true, "Meet Ada");
	settings.Add("20113", true, "Start Gigante Escape");
	settings.Add("10138", true, "Gigante Under the Bridge");
	settings.Add("23300", true, "Finish Chapter 8");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch9", false, "Chapter 9");
	settings.CurrentDefaultParent = "Ch9";
	settings.Add("52100", false, "Finish Maze");
	settings.Add("119222400", false, "Lion Head");
	settings.Add("119224000", false, "Goat Head");
	settings.Add("119225600", false, "Serpent Head");
	settings.Add("53100", false, "Ashley Start");
	settings.Add("119275200", false, "Bunch of Keys");
	settings.Add("53302", false, "Enter the Crypts");
	settings.Add("119220800", false, "Salazar Family Insignia");
	settings.Add("24100", false, "Finish Chapter 9");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch10", false, "Chapter 10");
	settings.CurrentDefaultParent = "Ch10";
	settings.Add("54200", false, "Enter Novi Hall");
	settings.Add("54300", false, "Enter Double Garrador Hall");
	settings.Add("54400", false, "Sewers Start");
	settings.Add("54401", false, "Tunnels Before Verdugo");
	settings.Add("10042", false, "Verdugo Spawns");
	settings.Add("24200", false, "Finish Chapter 10");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch11", false, "Chapter 11");
	settings.CurrentDefaultParent = "Ch11";
	settings.Add("55103", false, "Enter Dynamite Area");
	settings.Add("119236800", false, "Dynamite");
	settings.Add("10046", false, "Start Double Gigante");
	settings.Add("55201", false, "Enter Minecart Area");
	settings.Add("55202", false, "Finish 1st Minecart Section");
	settings.Add("55418", false, "Begin 2nd Minecart Section");
	settings.Add("10047", false, "Reach Novi Hive");
	settings.Add("10048", false, "Krauser Start");
	settings.Add("24300", false, "Finish Chapter 11");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch12", false, "Chapter 12");
	settings.CurrentDefaultParent = "Ch12";
	settings.Add("10147", false, "Begin Clocktower Climb");
	settings.Add("56104", false, "Finish Clocktower Climb");
	settings.Add("10051", false, "Salazar Start");
	settings.Add("20115", false, "Salazar Finish");
	settings.Add("56300", false, "Enter Docks");
	settings.Add("25100", false, "Finish Chapter 12");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch13", false, "Chapter 13");
	settings.CurrentDefaultParent = "Ch13";
	settings.Add("60103", false, "Turret Skip Double Door");
	settings.Add("61100", false, "Enter Facility");
	settings.Add("10056", false, "Find Ashley");
	settings.Add("61301", false, "Enter Regenerator Labs");
	settings.Add("119211200", false, "Level 1 Keycard");
	settings.Add("119212800", false, "Level 2 Keycard");
	settings.Add("119219200", false, "Wrench");
	settings.Add("119214400", false, "Level 3 Keycard");
	settings.Add("25200", false, "Finish Chapter 13");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch14", false, "Chapter 14");
	settings.CurrentDefaultParent = "Ch14";
	settings.Add("62100", false, "Enter Cargo Depot");
	settings.Add("63100", false, "Enter Facility 2");
	settings.Add("63108", false, "Enter Sewer Drainage");
	settings.Add("62200", false, "Enter Wrecking Ball Area");
	settings.Add("62201", false, "Area After Wrecking Ball");
	settings.Add("10061", false, "Reach Amber");
	settings.Add("10062", false, "Start Krauser Fight");
	settings.Add("10136", false, "Start Final Krauser Fight");
	settings.Add("25300", false, "Finish Chapter 14");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch15", false, "Chapter 15");
	settings.CurrentDefaultParent = "Ch15";
	settings.Add("20130", false, "Mike Blows Up Debris");
	settings.Add("20128", false, "Mike Blows Up Double Door");
	settings.Add("10066", false, "RIP Mike");
	settings.Add("67104", false, "Enter Turret Skip Gauntlet");
	settings.Add("10067", false, "Pick Up Ashley");
	settings.Add("25400", false, "Chapter 15");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Ch16", false, "Chapter 16");
	settings.CurrentDefaultParent = "Ch16";
	settings.Add("10071", false, "Saddler Start");
	settings.Add("20119", false, "Saddler Phase 2 Start");
	settings.Add("10158", false, "Saddler End");
	settings.Add("10074", false, "Reach Boat");
	settings.Add("10075", true, "Chapter 16 (Final Split)");
	settings.CurrentDefaultParent = null;
}

update
{
	//print(modules.First().ModuleMemorySize.ToString());
	
	if(timer.CurrentPhase == TimerPhase.NotRunning)
	{
		vars.completedSplits.Clear();
	}
	
	if(current.Cutscene == 10003 && old.Cutscene == -1 && current.Map == 40500){
		vars.StartTime = current.ChapterTimeStart;
		return true;
	}
}

gameTime
{
	return TimeSpan.FromSeconds((current.GameElapsedTime - current.DemoSpendingTime - current.PauseSpendingTime - vars.StartTime) / 1000000.0);
}

start
{
	return current.Cutscene == 10003 && old.Cutscene == -1 && current.Map == 40500;
}

split
{
	if(settings["" + current.Chapter] && !vars.completedSplits.Contains(current.Chapter)){
		vars.completedSplits.Add(current.Chapter);
		return true;
	}
	
	if(settings["" + current.Cutscene] && !vars.completedSplits.Contains(current.Cutscene)){
		vars.completedSplits.Add(current.Cutscene);
		return true;
	}
	
	if(settings["" + current.Map] && !vars.completedSplits.Contains(current.Map)){
		vars.completedSplits.Add(current.Map);
		return true;
	}
	
	if(settings["" + current.ItemID] && !vars.completedSplits.Contains(current.ItemID)){
		vars.completedSplits.Add(current.ItemID);
		return true;
	}
	
	if(settings["Merch"] && current.Chapter == 22100 && current.Map == 46210 && !vars.completedSplits.Contains(current.Map)){
		vars.completedSplits.Add(current.Map);
		return true;
	}
	
	if((settings["Merch"] && current.Map == 46210 || settings["Church"] && current.Map == 45401) && current.Chapter == 22100 && !vars.completedSplits.Contains(current.Map)){
		vars.completedSplits.Add(current.Map);
		return true;
	}
	
	if((settings["Town"] && current.Map == 40213 || settings["Barn"] && current.Map == 43300) && current.Chapter == 22200 && !vars.completedSplits.Contains(current.Map)){
		vars.completedSplits.Add(current.Map);
		return true;
	}
}


isLoading
{
	return true;
}

reset
{
	return current.Cutscene == 10157 && old.Cutscene == -1;
	vars.StartTime = 0;
}

exit
{
    //pauses timer if the game crashes
	timer.IsGameTimePaused = true;
}
