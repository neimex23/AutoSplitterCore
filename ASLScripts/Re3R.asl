//Resident Evil 3 Remake Autosplitter
//By CursedToast & VideoGameRoulette 04/03/2020
//Special thanks to Squirrelies for collaborating in finding memory values.
//Last updated 05/08/2023

state("re3", "World Public RT 2023")
{
    int gameStartType : "re3.exe", 0x09A70808, 0x54;
	int survivorType : "re3.exe", 0x09A772E8, 0x50, 0x10, 0x20, 0x54;
	int playerCurrentHP : "re3.exe", 0x09A772E8, 0x50, 0x10, 0x20, 0x2C0, 0x58;
    int playerMaxHP : "re3.exe", 0x09A772E8, 0x50, 0x10, 0x20, 0x2C0, 0x54;
	int map : "re3.exe", 0x09A76458;
	int loc : "re3.exe", 0x09A76450;
	int weaponSlot1 : "re3.exe", 0x09A68190, 0x50, 0x98, 0x10, 0x20, 0x18, 0x10, 0x14;
	int bossHP : "re3.exe", 0x09A750C8, 0x78, 0x10, 0x20, 0x300, 0x58;
	byte isCutscene : 0x09A650A8, 0x51;
	long active : 0x09A650A8, 0x60, 0x18;
	long cutscene : 0x09A650A8, 0x60, 0x20;
	long paused : 0x09A650A8, 0x60, 0x30;
}

state("re3", "World DX11 2023")
{
    int gameStartType : "re3.exe", 0x08C5EBF8, 0x54;
	int survivorType : "re3.exe", 0x08C73F98, 0x50, 0x10, 0x20, 0x54;
	int playerCurrentHP : "re3.exe", 0x08C73F98, 0x50, 0x10, 0x20, 0x2C0, 0x58;
    int playerMaxHP : "re3.exe", 0x08C73F98, 0x50, 0x10, 0x20, 0x2C0, 0x54;
	int map : "re3.exe", 0x08C74308;
	int loc : "re3.exe", 0x08C74300;
	int weaponSlot1 : "re3.exe", 0x08C6F648, 0x50, 0x98, 0x10, 0x20, 0x18, 0x10, 0x14;
	int bossHP : "re3.exe", 0x08C72D60, 0x78, 0x10, 0x20, 0x300, 0x58;
	byte isCutscene : 0x08C63308, 0x51;
	long active : 0x08C63308, 0x60, 0x18;
	long cutscene : 0x08C63308, 0x60, 0x20;
	long paused : 0x08C63308, 0x60, 0x30;
}

startup
{
	vars.logToFile = false;
    vars.logPath = "re3.log";
    vars.MAX_ITEMS = 20;

	Action<string> RemoveFirstLine = (filePath) => {
		string[] lines = File.ReadAllLines(filePath);
		File.WriteAllLines(filePath, lines.Skip(1));
	};

	Action<string, string> SaveLogs = (filePath, text) => {
		// Get the current line count of the file
		int lineCount = File.ReadLines(filePath).Count();

		// Check if line count is greater than 10
		if (lineCount > 10)
		    RemoveFirstLine(filePath);

		// Append the new line to the file
		using (StreamWriter sw = File.AppendText(filePath))
			sw.WriteLine(text);
	};

	Action<string> DebugOutput = (text) => {
		string filePath = "re3.log";
		print("[Debug Livesplit]: " + text);
		if (vars.logToFile)
			SaveLogs(filePath, text);
	};
	vars.Log = DebugOutput;

	Action<string, bool, string, string> initSettingGroup = (key, enabled, title, description) => {
		settings.Add(key, enabled, title);
		settings.SetToolTip(key, description);
	};

	Action<string, bool, string, string, string> initSettingGroupOption = (key, enabled, title, group, description) => {
		settings.Add(key, true, title, group);
		settings.SetToolTip(key, description);
	};
	
	initSettingGroup("logToFile", false, "Debug Logging", "Toggles the DebugOutput to output 10 latest logs to log file");
	initSettingGroup("segments", false, "Segment Practice Start", "Enables or disables segmented start trigger for segmented practice.");

	initSettingGroup("part1", true, "Part 1", "");
	initSettingGroupOption("escapedApartment", true, "Escaped Apartment", "part1", "Enter Location 12");
	initSettingGroupOption("byeBrad", true, "Escaped Bar Jack", "part1", "");
	initSettingGroupOption("dario", true, "Met Dario", "part1", "");
	initSettingGroupOption("garageRoof", true, "Parking Garage", "part1", "");
	initSettingGroupOption("reachedSubway", true, "Reached Subway (first time)", "part1", "");
	initSettingGroupOption("drinkOfWater", true, "Tall Drink of Water (cutscene)", "part1", "");
	initSettingGroupOption("kitebros", true, "Reached Kitebros", "part1", "");
	initSettingGroupOption("fireHose", true, "Fire Hose", "part1", "");
	initSettingGroupOption("murphy", true, "Reached Murphy", "part1", "");
	initSettingGroupOption("boltCutters", false, "Boltcutters", "part1", "");
	
	initSettingGroup("part2", true, "Part 2", "");
	initSettingGroupOption("powerSubstation", true, "Reached Power Substation", "part2", "");
	initSettingGroupOption("lockPick", true, "Lockpick (In Box)", "part2", "");
	initSettingGroupOption("finishedPowerSubstation", true, "Finished Power Substation", "part2", "");
	initSettingGroupOption("kitebrosControl", true, "Kitebros Control Room", "part2", "");
	initSettingGroupOption("reachedSewers", true, "Reached Sewers", "part2", "");
	initSettingGroupOption("batteryPack", true, "Battery Pack (Sewers Key)", "part2", "");
	initSettingGroupOption("sewersExit", true, "Escaped Sewers", "part2", "");
	initSettingGroupOption("flameNemmy", true, "Nemesis 1 (Flamethrower)", "part2", "");
	initSettingGroupOption("kendos", true, "Reached Kendo's", "part2", "");
	initSettingGroupOption("kendoKey", true, "Kendo's Gate Key", "part2", "");
	initSettingGroupOption("escapedRocketNemmy", true, "Escaped Rocket Nemesis", "part2", "");
	
	initSettingGroup("part3", true, "Part 3", "");
	initSettingGroupOption("carlosRPDStart", true, "Carlos RPD Start", "part3", "");
	initSettingGroupOption("battery", true, "Battery (for detonator)", "part3", "");
	initSettingGroupOption("emptyDetonator", true, "Detonator", "part3", "");
	initSettingGroupOption("stars", true, "Reached STARS Office", "part3", "");
	
	initSettingGroup("part4", true, "Part 4", "");
	initSettingGroupOption("subwayTunnelExit", true, "Exited Subway Tunnel", "part4", "");
	initSettingGroupOption("clockNemmy", true, "Nemesis 2 (Clocktower)", "part4", "");
	initSettingGroupOption("tapePlayer", true, "Tape Player", "part4", "");
	initSettingGroupOption("lockerKey", true, "Locker Key", "part4", "");
	initSettingGroupOption("hospitalIdCard", true, "Hospital ID Card", "part4", "");
	initSettingGroupOption("cassette", true, "Audiocassette Tape", "part4", "");
	initSettingGroupOption("vaccine", true, "Vaccine", "part4", "");
	initSettingGroupOption("defendedJill", true, "Defended Jill", "part4", "");
	
	initSettingGroup("part5", true, "Part 5", "");
	initSettingGroupOption("hospitalLift", true, "Hospital Underground Lift", "part5", "");
	initSettingGroupOption("fuse1", true, "Fuse 1", "part5", "");
	initSettingGroupOption("fuse2", true, "Fuse 2", "part5", "");
	initSettingGroupOption("fuse3", true, "Fuse 3", "part5", "");
	
	initSettingGroup("part6", true, "Part 6", "");
	initSettingGroupOption("nest2", true, "Reached NEST2", "part6", "");
	initSettingGroupOption("overrideKey", true, "Override Key", "part6", "");
	initSettingGroupOption("cultureSample", true, "Culture Sample", "part6", "");
	initSettingGroupOption("liquidTube", true, "Liquid Filled Test Tube", "part6", "");
	initSettingGroupOption("vaccineBase", true, "Vaccine Base", "part6", "");
	initSettingGroupOption("vaccineCompleted", false, "Vaccine", "part6", "");
	initSettingGroupOption("disposalNemmy", true, "Nemesis 3 (Waste Disposal)", "part6", "");
	initSettingGroupOption("finalNemmy", true, "Nemesis 4 (Final)", "part6", "");
	initSettingGroupOption("end", true, "The Last Escape (End)", "part6", "");
}

init
{
	vars.Log("=== Module Memory Size === " + modules.First().ModuleMemorySize.ToString());
	vars.inventoryPtr = IntPtr.Zero;
	
	switch (modules.First().ModuleMemorySize)
	{
		default:
			version = "World DX11 2023";
			vars.inventoryPtr = 0x08C6F648;
			break;
		case (173076480):
			version = "World Public RT 2023";
			vars.inventoryPtr = 0x08C6F648;
			break;
	}

    // Track inventory IDs
    current.inventory = new int[20];
    for (int i = 0; i < current.inventory.Length; ++i)
    {
        int itemID = 0;
        IntPtr ptr;
        new DeepPointer(vars.inventoryPtr, 0x50, 0x98, 0x10, 0x20 + (i * 8), 0x18, 0x10, 0x10).DerefOffsets(memory, out ptr);
        memory.ReadValue<int>(ptr, out itemID);
        current.inventory[i] = itemID;
    }
}

start
{	
	// isNewGameStart conditions
    bool locationsReset = current.map == 0 && current.loc == 0;
    bool isPlayerInit = old.playerMaxHP != 1200 && current.playerCurrentHP == 1200 && current.playerMaxHP == 1200;
    bool isNewGameInit = old.gameStartType == 1 && current.gameStartType == 1;

    // New Game Run Started
    bool isNewGameStart = locationsReset && isPlayerInit && isNewGameInit;

    // Segmented Runs Started
    bool isSegmentedStart = settings["segments"] && current.gameStartType == 2;

    // Start Conditions
    if (isNewGameStart || isSegmentedStart)
	{
		vars.Log("New Game Timer Started");
		return true;
	}
}

reset
{	
	if (current.loc != 0 && current.map != 0 && current.gameStartType == 0)
	{
		vars.Log("Exited To Title Resetting Timer");
		return true;
	}
}

update
{
	vars.logToFile = settings["logToFile"];

	if (current.gameStartType != old.gameStartType) vars.Log("Game Start Type Changed: " + current.gameStartType);

	if (current.map == 323)
	{
		vars.reachedEnd = 1;
		print("reachedEnd");
	}

	// Track inventory IDs
    current.inventory = new int[20];
    for (int i = 0; i < current.inventory.Length; ++i)
    {
        int itemID = 0;
        IntPtr ptr;
        new DeepPointer(vars.inventoryPtr, 0x50, 0x98, 0x10, 0x20 + (i * 8), 0x18, 0x10, 0x10).DerefOffsets(memory, out ptr);
        memory.ReadValue<int>(ptr, out itemID);
        current.inventory[i] = itemID;
    }
	
	if (timer.CurrentPhase == TimerPhase.NotRunning)
	{
		vars.reachedEnd = 0;
		vars.battery = 0;
		vars.emptyDetonator = 0;
		vars.tapePlayer = 0;
		vars.lockerKey = 0;
		vars.hospitalIdCard = 0;
		vars.cassette = 0;
		vars.vaccine = 0;
		vars.fuse1 = 0;
		vars.fuse2 = 0;
		vars.fuse3 = 0;
		vars.overrideKey = 0;
		vars.cultureSample = 0;
		vars.liquidTube = 0;
		vars.vaccineBase = 0;
		vars.fireHose = 0;
		vars.lockPick = 0;
		vars.batteryPack = 0;
		vars.kendoKey = 0;
		vars.boltCutters = 0;
		vars.escapedApartment = 0;
		vars.byeBrad = 0;
		vars.dario = 0;
		vars.garageRoof = 0;
		vars.reachedSubway = 0;
		vars.drinkOfWater = 0;
		vars.kitebros = 0;
		vars.murphy = 0;
		vars.powerSubstation = 0;
		vars.finishedPowerSubstation = 0;
		vars.kitebrosControl = 0;
		vars.reachedSewers = 0;
		vars.sewersExit = 0;
		vars.flameNemmy = 0;
		vars.kendos = 0;
		vars.escapedRocketNemmy = 0;
		vars.carlosRPDStart = 0;
		vars.stars = 0;
		vars.subwayTunnelExit = 0;
		vars.clockNemmy = 0;
		vars.defendedJill = 0;
		vars.hospitalLift = 0;
		vars.nest2 = 0;
		vars.vaccineCompleted = 0;
		vars.disposalNemmy = 0;
		vars.finalNemmy = 0;
		vars.end = 0;
	}
}

split
{
	Func<string, bool> LogAndSplit = (splitId) => {
  		vars.Log("Splitting: " + splitId);
  		return settings[splitId];
	};
	
	// Item splits
    int[] currentInventory = (current.inventory as int[]);
    int[] oldInventory = (old.inventory as int[]); // throws error first update, will be fine afterwards.

    for (int i = 0; i < currentInventory.Length; i++)
    {
        if (currentInventory[i] != oldInventory[i])
        {
			switch (oldInventory[i])
			{
				case 0x000000D9:
				{
					if (vars.defendedJill == 0)
					{
						vars.defendedJill = 1;
						return LogAndSplit("defendedJill");
					}
					break;
				}
				default:
					break;
			}
			
			switch (currentInventory[i])
            {
				case 0x000000A1:
				{
					if (vars.battery == 0)
					{
						vars.battery = 1;
						return LogAndSplit("battery");
					}
					break;
				}
				case 0x000000A5:
				{
					if (vars.emptyDetonator == 0)
					{
						vars.emptyDetonator = 1;
						return LogAndSplit("emptyDetonator");
					}
					break;
				}
				case 0x000000D6:
				{
					if (vars.tapePlayer == 0)
					{
						vars.tapePlayer = 1;
						return LogAndSplit("tapePlayer");
					}
					break;
				}
				case 0x000000DA:
				{
					if (vars.lockerKey == 0)
					{
						vars.lockerKey = 1;
						return LogAndSplit("lockerKey");
					}
					break;
				}
				case 0x000000D3:
				{
					if (vars.hospitalIdCard == 0)
					{
						vars.hospitalIdCard = 1;
						return LogAndSplit("hospitalIdCard");
					}
					break;
				}
				case 0x000000D5:
				{
					if (vars.cassette == 0)
					{
						vars.cassette = 1;
						return LogAndSplit("cassette");
					}
					break;
				}
				case 0x000000D7:
				{
					if (vars.vaccine == 0)
					{
						vars.vaccine = 1;
						return LogAndSplit("vaccine");
					}
					break;
				}
				case 0x000000DE:
				{
					if (vars.fuse3 == 0)
					{
						vars.fuse3 = 1;
						return LogAndSplit("fuse3");
					}
					break;
				}
				case 0x000000E0:
				{
					if (vars.fuse1 == 0)
					{
						vars.fuse1 = 1;
						return LogAndSplit("fuse1");
					}
					break;
				}
				case 0x000000DF:
				{
					if (vars.fuse2 == 0)
					{
						vars.fuse2 = 1;
						return LogAndSplit("fuse2");
					}
					break;
				}
				case 0x000000E8:
				{
					if (vars.overrideKey == 0)
					{
						vars.overrideKey = 1;
						return LogAndSplit("overrideKey");
					}
					break;
				}
				case 0x000000EA:
				{
					if (vars.cultureSample == 0)
					{
						vars.cultureSample = 1;
						return LogAndSplit("cultureSample");
					}
					break;
				}
				case 0x000000EB:
				{
					if (vars.liquidTube == 0)
					{
						vars.liquidTube = 1;
						return LogAndSplit("liquidTube");
					}
					break;
				}
				case 0x000000EC:
				{
					if (vars.vaccineBase == 0)
					{
						vars.vaccineBase = 1;
						return LogAndSplit("vaccineBase");
					}
					break;
				}
				case 0x000000B5:
				{
					if (vars.fireHose == 0)
					{
						vars.fireHose = 1;
						return LogAndSplit("fireHose");
					}
					break;
				}
				case 0x000000B9:
				{
					if (vars.lockPick == 0)
					{
						vars.lockPick = 1;
						return LogAndSplit("lockPick");
					}
					break;
				}
				case 0x000000BA:
				{
					if (vars.batteryPack == 0)
					{
						vars.batteryPack = 1;
						return LogAndSplit("batteryPack");
					}
					break;
				}
				case 0x000000B6:
				{
					if (vars.kendoKey == 0)
					{
						vars.kendoKey = 1;
						return LogAndSplit("kendoKey");
					}
					break;
				}
				case 0x00000098:
				{
					if (vars.boltCutters == 0)
					{
						vars.boltCutters = 1;
						return LogAndSplit("boltCutters");
					}
					break;
				}
				case 0x000000E9:
				{
					if (vars.vaccineCompleted == 0)
					{
						vars.vaccineCompleted = 1;
						return LogAndSplit("vaccineCompleted");
					}
					break;
				}
				
                default:
                {
                    break; // No work to do.
                }
            }
        }
    }
	
	// Boss Splits
	if (current.map == 242 && current.bossHP < 1 && vars.clockNemmy == 0)
	{
		vars.clockNemmy = 1;
		return LogAndSplit("clockNemmy");
	}
	
	if (current.map == 231 && current.bossHP < 1 && vars.flameNemmy == 0)
	{
		vars.flameNemmy = 1;
		return LogAndSplit("flameNemmy");
	}
	
	if (current.map == 316 && !(current.bossHP >= 1) && vars.disposalNemmy == 0)
	{
		vars.disposalNemmy = 1;
		return LogAndSplit("disposalNemmy");
	}
	
	if (current.map == 319 && current.bossHP < 5000  && vars.finalNemmy == 0)
	{
		vars.finalNemmy = 1;
		return LogAndSplit("finalNemmy");
	}

	// Map splits

	//End split
	if (vars.reachedEnd == 1 && current.map == 0 && current.loc == 0 && current.isCutscene == 1)
	{
		vars.end = 1;
		return LogAndSplit("end");
	}
	
	if (current.map == 200 && current.cutscene > old.cutscene && vars.finishedPowerSubstation == 0)
	{
		vars.finishedPowerSubstation = 1;
		return LogAndSplit("finishedPowerSubstation");
	}
		
	if (current.map == 204 && current.cutscene > old.cutscene && vars.kitebrosControl == 0)
	{
		vars.kitebrosControl = 1;
		return LogAndSplit("kitebrosControl");
	}

	if (current.map != old.map)
	{
		vars.Log("Current Map Changed: " + current.map);
		if (current.map == 12 && vars.escapedApartment == 0)
		{
			vars.escapedApartment = 1;
			return LogAndSplit("escapedApartment");
		}
		
		if (current.map == 18 && vars.byeBrad == 0)
		{
			vars.byeBrad = 1;
			return LogAndSplit("byeBrad");
		}
		
		if (current.map == 21 && vars.dario == 0)
		{
			vars.dario = 1;
			return LogAndSplit("dario");
		}
		
		if (current.map == 26 && vars.garageRoof == 0)
		{
			vars.garageRoof = 1;
			return LogAndSplit("garageRoof");
		}
		
		if (current.map == 187 && vars.reachedSubway == 0)
		{
			vars.reachedSubway = 1;
			return LogAndSplit("reachedSubway");
		}
		
		if (current.map == 142 && vars.drinkOfWater == 0)
		{
			vars.drinkOfWater = 1;
			return LogAndSplit("drinkOfWater");
		}
		
		if (current.map == 203 && vars.kitebros == 0)
		{
			vars.kitebros = 1;
			return LogAndSplit("kitebros");
		}
		
		if (current.map == 155 && vars.murphy == 0)
		{
			vars.murphy = 1;
			return LogAndSplit("murphy");
		}
		
		if (current.map == 199 && vars.powerSubstation == 0)
		{
			vars.powerSubstation = 1;
			return LogAndSplit("powerSubstation");
		}
		
		if (current.map == 206 && vars.reachedSewers == 0)
		{
			vars.reachedSewers = 1;
			return LogAndSplit("reachedSewers");
		}
		
		if (current.map == 162 && vars.sewersExit == 0)
		{
			vars.sewersExit = 1;
			return LogAndSplit("sewersExit");
		}
		
		if (current.map == 124 && vars.kendos == 0)
		{
			vars.kendos = 1;
			return LogAndSplit("kendos");
		}
		
		if (current.map == 138 && vars.escapedRocketNemmy == 0)
		{
			vars.escapedRocketNemmy = 1;
			return LogAndSplit("escapedRocketNemmy");
		}
		
		if (current.map == 36 && vars.carlosRPDStart == 0)
		{
			vars.carlosRPDStart = 1;
			return LogAndSplit("carlosRPDStart");
		}
		
		if (current.map == 71 && vars.stars == 0)
		{
			vars.stars = 1;
			return LogAndSplit("stars");
		}
		
		if (current.map == 240 && vars.subwayTunnelExit == 0)
		{
			vars.subwayTunnelExit = 1;
			return LogAndSplit("subwayTunnelExit");
		}
		
		if (current.map == 294 && vars.hospitalLift == 0)
		{
			vars.hospitalLift = 1;
			return LogAndSplit("hospitalLift");
		}
		
		if (current.map == 324 && vars.nest2 == 0)
		{
			vars.nest2 = 1;
			return LogAndSplit("nest2");
		}
	}
}

gameTime
{
	return TimeSpan.FromSeconds((current.active - current.cutscene - current.paused) / 1000000.0);
}

isLoading
{
	return true;
}
