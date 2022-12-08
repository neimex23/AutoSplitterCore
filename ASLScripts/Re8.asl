/*
--------------------------------------
	Resident Evil Village
	Load Remover & Auto Splitter
	
	Official timing method for RE8 speedruns for PC.
	https://www.speedrun.com/re8
	
	Scipt & offsets pre WW_1.5 by CursedToast 05.26.2021
	SoR script & offsets post WW_1.5 by TheDementedSalad 01/12/2022
	Maintained by TheDementedSalad
--------------------------------------
*/

state("re8", "WW_1.7")
{
	byte LoadState		:	0xC9AF9B8, 0xE1;									//All +9000 base address from 1.6
	byte PauseState		:	0xC9FF658, 0x48;								
	byte TimeBleed		:	0xC9AF9B0, 0x84;								
	uint CutsceneState	:	0xC9AF1E8, 0x10;								
	uint NewestItemHash	:	0xC9AF820, 0x60, 0x18, 0x10, 0x20, 0x58, 0x3C;
	string128 Chapter	:	0xC9AF9B8, 0x60, 0x14;							
	string128 View		:	0xC9AF9B8, 0x58, 0x14;							
	string128 Map		:	0xC9AF718, 0x90, 0x14;							
	string128 Event		:	0xC9AF738, 0x58, 0x68, 0x40, 0x30, 0x14;		
}

state("re8", "WW_1.6")
{
	byte LoadState		:	0xC9A69B8, 0xE1;								
	byte PauseState		:	0xC9F6658, 0x48;								
	byte TimeBleed		:	0xC9A69B0, 0x84;								
	uint CutsceneState	:	0xC9A61E8, 0x10;								
	uint NewestItemHash	:	0xC9A6820, 0x60, 0x18, 0x10, 0x20, 0x58, 0x3C;
	string128 Chapter	:	0xC9A69B8, 0x60, 0x14;							
	string128 View		:	0xC9A69B8, 0x58, 0x14;							
	string128 Map		:	0xC9A6718, 0x90, 0x14;							
	string128 Event		:	0xC9A6738, 0x58, 0x68, 0x40, 0x30, 0x14;		
}

state("re8", "WW_1.5")
{
	byte LoadState		:	0xC9B79A8, 0xE1;								//Same pointer as View & same offsets as old updates
	byte PauseState		:	0xCA07648, 0x48;								//Does not change until you gain control of Ethan again
	byte TimeBleed		:	0xC9B79A0, 0x84;								//Fixes Some Timebleed in Shadows of Rose. 2 in game 3 during cutscene after Eveline in Ethans House
	uint CutsceneState	:	0xC9B71D8, 0x10;								//15 in cutscene, 2 not in cutscene
	uint NewestItemHash	:	0xC9B7810, 0x60, 0x18, 0x10, 0x20, 0x58, 0x3C;	//Find ammo count and pointerscan with 4C as final offset. Find closest pointers and then change them to the ones to the left of here from old updates
	string128 Chapter	:	0xC9B79A8, 0x60, 0x14;							//Same pointer as View & same offsets as old updates
	string128 View		:	0xC9B79A8, 0x58, 0x14;							//AOB Scan 4D 00 61 00 78 00 48 00 44 00 52 00 42 00 72 00 69 00 67 00 68 00 74 00 6E 00 65 00 73 00 73 when on the main menu, then move address forward until it says MainMenu
	string128 Map		:	0xC9B7708, 0x90, 0x14;							//First map is st01_000_Home1FGarden_00 (UTF-16 String), same offsets as old updates
	string128 Event		:	0xC9B7728, 0x58, 0x68, 0x40, 0x30, 0x14;		//AOB Scan 63 00 31 00 30 00 65 00 30 00 30 00 31 00 5F 00 30 00 30 00 00 00 00 00 00 00 00 00 one of these is the correct one
}

state("re8", "WW_1.4")
{
	byte LoadState		:	0xA06CA58, 0xE1;
	byte PauseState		:	0xA0583D8, 0x48;
	byte gameState		:	0xA067CB8, 0x8;
	uint CutsceneState	:	0xA0306D8, 0x10;
	uint NewestItemHash	:	0xA06C8E0, 0x60, 0x18, 0x10, 0x20, 0x58, 0x3C;
	string128 Chapter	:	0xA06CA58, 0x60, 0x14;
	string128 View		:	0xA06CA58, 0x58, 0x14;
	string128 Map		:	0xA06C7E0, 0x90, 0x14;
	string128 Event		:	0xA03D388, 0x58, 0x68, 0x40, 0x30, 0x14;
}

state("re8", "WW_1.3")
{
	byte LoadState		:	0xA06B968, 0xE1;
	byte PauseState		:	0xA0572E8, 0x48;
	uint CutsceneState	:	0xA02F5E8, 0x10;
	uint NewestItemHash	:	0xA06B7F0, 0x60, 0x18, 0x10, 0x20, 0x58, 0x3C;
	string128 Chapter	:	0xA06B968, 0x60, 0x14;
	string128 View		:	0xA06B968, 0x58, 0x14;
	string128 Map		:	0xA06B6F0, 0x90, 0x14;
	string128 Event		:	0xA03C298, 0x58, 0x68, 0x40, 0x30, 0x14;
}

state("re8", "WW_1.2")
{
	byte LoadState		:	0xA06B968, 0xE1;
	byte PauseState		:	0xA0572E8, 0x48;
	uint CutsceneState	:	0xA02F5E8, 0x10;
	uint NewestItemHash	:	0xA06B7F0, 0x60, 0x18, 0x10, 0x20, 0x58, 0x3C;
	string128 Chapter	:	0xA06B968, 0x60, 0x14;
	string128 View		:	0xA06B968, 0x58, 0x14;
	string128 Map		:	0xA06B6F0, 0x90, 0x14;
	string128 Event		:	0xA03C298, 0x58, 0x68, 0x40, 0x30, 0x14;
}

state("re8", "WW_1.1")
{
	byte LoadState		:	0xA069998, 0xE1;
	byte PauseState		:	0xA055318, 0x48;
	uint CutsceneState	:	0xA02D618, 0x10;
	uint NewestItemHash	:	0xA06A5B8, 0x60, 0x18, 0x10, 0x20, 0x58, 0x3C;
	string128 Chapter	:	0xA069998, 0x60, 0x14;
	string128 View		:	0xA069998, 0x58, 0x14;
	string128 Map		:	0xA069720, 0x90, 0x14;
	string128 Event		:	0xA03A2B8, 0x58, 0x68, 0x40, 0x30, 0x14;
}

state("re8", "WW_1.0")
{
	byte LoadState		:	0xA1B1DE8, 0xE1;
	byte PauseState		:	0xA19E058, 0x48;
	uint CutsceneState	:	0x0A17FAC8, 0x10;
	uint NewestItemHash	:	0xA1B29F0, 0x60, 0x18, 0x10, 0x20, 0x58, 0x3C;
	string128 Chapter	:	0xA1B8FB0, 0x60, 0x14;
	string128 View		:	0xA1B8FB0, 0x58, 0x14;
	string128 Map		:	0xA1B4960, 0x90, 0x14;
	string128 Event		:	0xA182B38, 0x58, 0x68, 0x40, 0x30, 0x14;
}

state("re8", "CeroD_1.0")
{
	byte LoadState		:	0xA1B3DE8, 0xE1;
	byte PauseState 	:	0xA1A0058, 0x48;
	uint CutsceneState	:	0xA181AC8, 0x10;
	uint NewestItemHash :	0xA1B49F0, 0x60, 0x18, 0x10, 0x20, 0x58, 0x3C;
	string128 Chapter	:	0xA1BAFB0, 0x60, 0x14;
	string128 View		:	0xA1BAFB0, 0x58, 0x14;
	string128 Map		:	0xA1B6960, 0x90, 0x14;
	string128 Event		:	0xA184B38, 0x58, 0x68, 0x40, 0x30, 0x14;
}

state("re8", "Promo_1.0")
{
	byte LoadState		:	0xA1B2E18, 0xE1;
	byte PauseState		:	0xA19F088, 0x48;
	uint CutsceneState	:	0xA180AF8, 0x10;
	uint NewestItemHash	:	0xA1B3A20, 0x60, 0x18, 0x10, 0x20, 0x58, 0x3C;
	string128 Chapter	:	0xA1B9FE0, 0x60, 0x14;
	string128 View		:	0xA1B9FE0, 0x58, 0x14;
	string128 Map		:	0xA1B5990, 0x90, 0x14;
	string128 Event		:	0xA183B68, 0x58, 0x68, 0x40, 0x30, 0x14;
}

startup
{
	settings.Add("Main", false, "Main Game");
	settings.CurrentDefaultParent = "Main";
	settings.Add("village", false, "Village 1");
	settings.CurrentDefaultParent = "village";
	settings.Add("c10e050_00", false, "Put Rose to Bed");
	settings.Add("c21e000_00", false, "Car Crash Site");
	settings.Add("1664342338", false, "Bolt Cutters");
	settings.Add("arrowKnee", false, "Arrow to the Knee");
	settings.Add("3619364444", false, "Maiden Crest");
	settings.Add("3219462766", false, "Screwdriver");
	settings.Add("1132688171", false, "Demon Crest");
	settings.Add("c21e510_00", false, "Meet Heisenberg Cutscene");
	
	settings.CurrentDefaultParent = "Main";
	settings.Add("castle", false, "Castle");
	settings.CurrentDefaultParent = "castle";
	settings.Add("st11_010_EntranceHallB_In2F", false, "Reached Castle");
	settings.Add("2183898626", false, "Maroon Eye Ring");
	settings.Add("1787578325", false, "Maroon Eye");
	settings.Add("st11_066_PrisonB_In1B", false, "Reached Prison");
	settings.Add("2685023068", false, "Sanguis Virginis (Wine Bottle)");
	settings.Add("1354786552", false, "Courtyard Key");
	settings.Add("st11_092_Courtyard_Out", false, "Reached Courtyard");
	settings.Add("st11_074_BasementA_In1B", false, "Reached Distillery");
	settings.Add("1738775830", false, "Dimitrescu's' Key");
	settings.Add("418018598", false, "Iron Insignia Key");
	settings.Add("3513834249", false, "Mask of Sorrow");
	settings.Add("c22e550_00", false, "Ethan Loses Hand Cutscene");
	settings.Add("2365661828", false, "Mask of Joy");
	settings.Add("st11_094_Roof_Out", false, "Reached Roof");
	settings.Add("314002296", false, "Mask of Rage");
	settings.Add("1344029762", false, "Mask of Pleasure");
	settings.Add("3784287352", false, "Mounted Animal Skull");
	settings.Add("3237773355", false, "Animal Skull");
	settings.Add("c22e720_00", false, "Lady D. Boss Start");
	settings.Add("c22e800_00", false, "Lady D. Defeated");
	settings.Add("2309731541", false, "Dirty Flask (Head)");
	
	settings.CurrentDefaultParent = "Main";
	settings.Add("villageTwo", false, "Village 2");
	settings.CurrentDefaultParent = "villageTwo";
	settings.Add("808039580", false, "Winged Key");
	settings.Add("c26e200_00", false, "Meet Duke Cutscene");
	settings.Add("1093531362", false, "Jack Handle");
	settings.Add("185799830", false, "Four-Winged Key");
	settings.Add("c26e250_00", false, "Meet Duke Again Cutscene (after key)");
	
	settings.CurrentDefaultParent = "Main";
	settings.Add("dollhouse", false, "Beneviento");
	settings.CurrentDefaultParent = "dollhouse";
	settings.Add("c23e050_00", false, "Mia Graveyard Cutscene");
	settings.Add("st14_001_Hall_In1F", false, "Reached Beneviento House");
	settings.Add("2896087978", false, "Blood Covered Ring");
	settings.Add("2573570207", false, "Silver Key");
	settings.Add("3856576850", false, "Winding Key");
	settings.Add("895810843", false, "Wedding Ring");
	settings.Add("2925641544", false, "Tweezers");
	settings.Add("3542466670", false, "Film");
	settings.Add("2554874516", false, "Scissors");
	settings.Add("2564145014", false, "Brass Medallion");
	settings.Add("1042056128", false, "Breaker Box Key");
	settings.Add("988104371", false, "Relief of a Child");
	settings.Add("709500517", false, "Fuse");
	settings.Add("c23e660_00", false, "Escaped Baby");
	settings.Add("360286557", false, "Four-Winged Unborn Key");
	settings.Add("2563213816", false, "Leg Flask");
	
	settings.CurrentDefaultParent = "Main";
	settings.Add("fish", false, "Moreau");
	settings.CurrentDefaultParent = "fish";
	settings.Add("c26e500_00", false, "Werewolf Cutscene");
	settings.Add("3720810444", false, "Arm Flask");
	settings.Add("2844148845", false, "Boat Key");
	settings.Add("c24e200_02", false, "Meet Chris Cutscene");
	settings.Add("1142718375", false, "Crank");
	settings.Add("c24e810_00", false, "Moreau (the best) Defeated");
	settings.Add("847933194", false, "Six-Winged Unborn Key");
	
	settings.CurrentDefaultParent = "Main";
	settings.Add("walterwhite", false, "Heisenberg");
	settings.CurrentDefaultParent = "walterwhite";
	settings.Add("719654765", false, "Torso Flask");
	settings.Add("158765264", false, "Giant's Chalice");
	settings.Add("c25e210_00", false, "Meet Heisenberg Cutscene");
	settings.Add("propellerOne", false, "Escaped Propeller Man (First Time)");
	settings.Add("st15_060_CastingMachineRoom_In3B", false, "Reached Casting Machine Room (First Time)");
	settings.Add("1911895058", false, "Relief Mold");
	settings.Add("1152567186", false, "Relief of a Horse");
	settings.Add("propellerTwo", false, "Escaped Propeller Man (Second Time)");
	settings.Add("1629499072", false, "Cog Mold");
	settings.Add("1858058345", false, "Large Cog");
	settings.Add("st15_030_BigFanArea_In2B", false, "Reached Big Fan (that will kill you)");
	settings.Add("1876186812", false, "Key Mold");
	settings.Add("4041096499", false, "Heisenberg's Key");
	settings.Add("controlRoom", false, "Reached Control Room (after killing Propeller Man)");
	settings.Add("c25e710_00", false, "Got on Tank");
	
	settings.CurrentDefaultParent = "Main";
	settings.Add("chris", false, "Chris Section");
	settings.CurrentDefaultParent = "chris";
	settings.Add("chrisStart", false, "Chris Start");
	settings.Add("c31e300_00", false, "'Reached Target Location'");
	settings.Add("c31e500_01", false, "Urias Start");
	settings.Add("c31e600_00", false, "Reached Megamycete");
	settings.Add("c31e800_00", false, "Chris End");
	
	settings.CurrentDefaultParent = "Main";
	settings.Add("finale", false, "Finale");
	settings.CurrentDefaultParent = "finale";
	settings.Add("c32e150_00", false, "Miranda Start");
	settings.Add("c32e400_00", false, "Miranda End");
	settings.CurrentDefaultParent = null;
	
	settings.Add("Rose", false, "Shadows of Rose");
	settings.CurrentDefaultParent = "Rose";
	settings.Add("Dungeon", false, "Dungeon");
	settings.CurrentDefaultParent = "Dungeon";
	settings.Add("2508552683", false, "Cell Key");
	settings.Add("3110932807", false, "Switch Handle");
	settings.Add("c101e060_00", false, "Escape Dungeon");
	
	settings.CurrentDefaultParent = "Rose";
	settings.Add("DukeCas", false, "Dukes Castle");
	settings.CurrentDefaultParent = "DukeCas";
	settings.Add("1318941960", false, "Bolt Cutters");
	settings.Add("3926554463", false, "RW-Variant Flask 1");
	settings.Add("4184743477", false, "Bronze Mask");
	settings.Add("115291557", false, "RW-Variant Flask 2");
	settings.Add("1467618523", false, "Monocular Key");
	settings.Add("298520404", false, "Silver Mask");
	settings.Add("2307002485", false, "Snake Painting");
	settings.Add("3483404595", false, "Triocular Key");
	settings.Add("2809339309", false, "Gold Mask");
	settings.Add("st21_022_StorageRoom_In2F", false, "Escape Jail Trap");
	settings.Add("c101e200_01", false, "Urias Wannabe Start");
	settings.Add("99244898", false, "RW-Variant Flask 3");
	settings.Add("c101e250_00", false, "Urias Wannabe End");
	
	settings.CurrentDefaultParent = "Rose";
	settings.Add("Doll", false, "Evelines Dollhouse");
	settings.CurrentDefaultParent = "Doll";
	settings.Add("st24_011_ElevatorHall_In1B", false, "Enter Evelines Dollhouse");
	settings.Add("3628065826", false, "Scissors");
	settings.Add("417473928", false, "Relief of a Child");
	settings.Add("1290304315", false, "Catherine Doll");
	settings.Add("3115585574", false, "Jimmy's Room Key");
	settings.Add("2962654202", false, "Jimmy Doll");
	settings.Add("385992936", false, "Black Haired Doll");
	settings.Add("3745882423", false, "Breaker Box Key");
	settings.Add("3959031061", false, "Fuse Map");
	settings.Add("2553722918", false, "Fuse");
	settings.Add("c102e050_00", false, "Down The Elevator");
	settings.Add("c102e070_00", false, "Reach Large Bedroom");
	settings.Add("c102e100_00", false, "Escape Large Mia Doll");
	settings.Add("954750291", false, "Drawer Key");
	settings.Add("4283333959", false, "RW-Variant Flask 4");
	settings.Add("c102e250_00", false, "Beat Eveline");
	
	settings.CurrentDefaultParent = "Rose";
	settings.Add("Vill", false, "Mirandas Village");
	settings.CurrentDefaultParent = "Vill";
	settings.Add("c103e070_01", false, "Reach Cave");
	settings.Add("c103e300_00", false, "Reach Crystal");
	settings.Add("c103e310_00", false, "Miranda Start");
	settings.Add("c103e320_00", false, "Miranda Finish");
	
	
	
}

init
{
	vars.firstCutsceneFinished = false;
	vars.startControlFlag = false;
	vars.completedSplits = new List<string>();
	vars.arrowKnee = false;
	vars.chrisStart = false;
	
	switch (modules.First().ModuleMemorySize)
	{
		case (628158464):
			version = "CeroD_1.0";
			break;
		case (620384256):
			version = "Promo_1.0";
			break;
		case (620728320):
			version = "WW_1.1";
			break;
		case (629379072):
			version = "WW_1.2";
			break;
		case (624750592):
			version = "WW_1.2";
			break;
		case (639950848):
			version = "WW_1.3";
			break;
		case (646139904):
			version = "WW_1.4";
			break;
		case (628748288):
			version = "WW_1.5";
			break;
		case (617246720):
			version = "WW_1.6";
			break;
		case (631308288):
			version = "WW_1.7";
			break;
		case (640962560):
		default:
			version = "WW_1.0";
			break;
	}
}

start
{
	// Start the timer
	if (vars.firstCutsceneFinished && current.PauseState == 0)
	{
		// Reset values here so the timer actually starts
		vars.firstCutsceneFinished = false;
		vars.startControlFlag = true;
		
		// Start the timer
		return true;
	}
}

update
{
	// Reset variables when the timer is reset.
	if (timer.CurrentPhase == TimerPhase.NotRunning)
	{
		vars.startControlFlag = false;
		vars.completedSplits.Clear();
		vars.arrowKnee = false;
		vars.chrisStart = false;
	}

	// Prevent initial time bleed by starting the timer only after the cutscene with Mia on the sofa (player gains control of Ethan)
	if (current.Chapter == "Chapter1" || current.Chapter == "Chapter10_1")
	{
		if (current.CutsceneState == 2 && old.CutsceneState == 15 && !vars.startControlFlag)
		{
			vars.firstCutsceneFinished = true;
		}
	}
	
	// Uncomment debug information in the event of an update.
	//print(modules.First().ModuleMemorySize.ToString());
}

split
{
	// Item splits
	if (current.NewestItemHash != old.NewestItemHash)
	{
		vars.hashString = current.NewestItemHash.ToString();
		if (settings[vars.hashString] && !vars.completedSplits.Contains(vars.hashString))
		{
			vars.completedSplits.Add(vars.hashString);
			return true;
		}
	}
	
	// Event splits
	if (current.Event != old.Event)
	{
		// Lycan Defense
		if (settings["arrowKnee"] && !vars.arrowKnee)
		{
			if (current.Event == "c21e160_02" || current.Event == "c21e160_00" || current.Event == "c21e160_01" || current.Event == "c21e160_03")
			{
				vars.arrowKnee = true;
				return true;
			}
		}
			
		// Chris Start
		if (settings["chrisStart"] && !vars.chrisStart)
		{
			if (current.Event == "c31e000_00" || current.Event == "c31e100_04")
			{
				vars.chrisStart = true;
				return true;
			}
		}

		// All other events without variant IDs
		if (settings[current.Event] && !vars.completedSplits.Contains(current.Event))
		{
			vars.completedSplits.Add(current.Event);
			return true;
		}
	}
	
	// Map splits
	if (current.Map != old.Map)
	{
		if (settings[current.Map] && !vars.completedSplits.Contains(current.Map))
		{
			vars.completedSplits.Add(current.Map);
			return true;
		}
		
		// Propellerman Splits
		else if (current.Chapter == "Chapter2_5")
		{
			if (old.Map == "st15_010_Passage_In1B" && current.Map == "st15_074_ScrapArea_In5B" && settings["propellerOne"])
			{
				if (!vars.completedSplits.Contains("propellerOne"))
				{
					vars.completedSplits.Add("propellerOne");
					return true;
				}
			}
				
			if (old.Map == "st15_042_Passage7_In3B" && current.Map == "st15_072_HallArea_In5B" && settings["propellerTwo"])
			{
				if (!vars.completedSplits.Contains("propellerTwo"))
				{
					vars.completedSplits.Add("propellerTwo");
					return true;
				}
			}
				
			if (old.Map == "st15_022_PropellerManFinal_In1B" && current.Map == "st15_018_ControlRoom_In1B" && settings["controlRoom"])
			{
				if (!vars.completedSplits.Contains("controlRoom"))
				{
					vars.completedSplits.Add("controlRoom");
					return true;
				}
			}
		}
	}
}

isLoading
{
	if(settings["Rose"]){
		return current.CutsceneState == 15 || current.PauseState == 1 || current.LoadState != 0 || current.TimeBleed == 3;
	}
	
	else return current.CutsceneState == 15 || current.PauseState == 1 || current.LoadState != 0;
}

reset
{
	return current.View == "MainMenu" || current.View == "DLCRoot_GE/DLC05/DLC05_2/Chapter10MainMenu";
}
