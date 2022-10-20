//Resident Evil 7 Autosplitter
//By CursedToast 1/28/2017
//Last updated 23/06/2022

//Special thanks to:
// Souzooka - helping me re-code this to reduce lag and improving my coding in ASL. Couldn't have done this without him :)
//CarcinogenSDA (you know why)
//Dchaps - programming support
//shiftweave - programming support
//mgr.inz.Player - inventory memory value support
//Theumer115 - inventory memory value support
//DarkByte - inventory memory value support
//Nexusphobiker - helping me find the most updated pointers in the 2/6/2017 RE7 update, and for teaching me how to find them again in the future.
//TheDementedSalad - found pointers for Steam DX12 update & CeroD Splashscreen update + Made Jack's 55th  timer.
//Ero - ASL helper and code for mm:ss:fff timer for Jack's 55th


state("re7", "1.4")
{
	int gamePauseState: 0x82186C0, 0x28, 0x428, 0x40, 0x28, 0x104;
	string128 map : 	0x81DE6A8, 0x700, 0x0;
	int isdying : 		0x81E4148, 0x60;								//Checks for if you are dying
}

state("re7", "cerod")
{
	int gamePauseState: 0x93698F0, 0x28, 0x428, 0x40, 0x28, 0x104;
	string128 map : 	0x932F7E8, 0x700, 0x0;
	int isdying : 		0x93352C0, 0x60;
}

state("re7", "cerod_nvidia")
{
	int gamePauseState: 0x9358310, 0x28, 0x428, 0x40, 0x28, 0x104;
	string128 map : 0x931D8E8, 0x700, 0x0;
	int isdying : 0x9322E10, 0x60;
}

state("re7", "Buy RE8!! Version")
{
	int gamePauseState: 0x81FB9F8, 0x108;
	string128 map : 0x81EACE0, 0x700, 0x0;
	int isdying : 0x81F24E8, 0x60;
}

state("re7", "12/17 Update")
{
	int gamePauseState: 0x81FA818, 0x108;
	string128 map : 0x81E9B00, 0x700, 0x0;
	int isdying : 0x81F1308, 0x60;
	int Jack55Bonus : 0x8227C20, 0x70, 0x58;
	int Jack55IGT : 0x8227C20, 0x70, 0x98;
	int Jack55Extra : 0x8227C20, 0x70, 0xC0;
	byte Jack55Start: 0x8227C20, 0x70, 0xC8;
	byte Jack55Level : 0x8224BB0, 0x50, 0x268, 0x30, 0x30, 0x100, 0x40, 0x1C0;
	byte Jack55End : 0x8227C20, 0xB0;
}

state("re7", "STEAM-NextGen")
{
	int gamePauseState: 0x8FC42F8, 0x104;
	string128 map : 0x8F7DE00, 0x960, 0x0;
	int isdying : 0x8FB9B48, 0x60;
	int Jack55Bonus : 0x8F80FF8, 0x70, 0x58;
	int Jack55IGT : 0x8F80FF8, 0x70, 0x98;
	int Jack55Extra : 0x8F80FF8, 0x70, 0xC0;
	int Jack55Start : 0x8F80FF8, 0x70, 0xC8;
	byte Jack55Level : 0x8224BB0, 0x6C8, 0x270, 0xB8, 0x3D0, 0x80, 0x1C4;
	byte Jack55End : 0x8F80FF8, 0xB0;
}

state("re7", "STEAM-6/10/22")
{
	int gamePauseState: 0x8FC4478, 0x104;
	string128 map : 0x8F7DF80, 0x960, 0x0;
	int isdying : 0x8FB9CC8, 0x60;
	int Jack55Bonus : 0x8F81178, 0x70, 0x58;
	int Jack55IGT : 0x8F81178, 0x70, 0x98;
	int Jack55Extra : 0x8F81178, 0x70, 0xC0;
	int Jack55Start : 0x8F81178, 0x70, 0xC8;
	byte Jack55Level : 0x8F81178, 0x6C8, 0x270, 0xB8, 0x3D0, 0x80, 0x1C4;
	byte Jack55End : 0x8F81178, 0xB0;
}

state("re7", "CeroD 20.4.0.2")
{
	int gamePauseState: 0x9384AB8, 0x104;
	string128 map : 0x934A600, 0x700, 0x0;
	int isdying : 0x9355468, 0x60;
}

startup
{
	Assembly.Load(File.ReadAllBytes("Components/asl-help")).CreateInstance("Basic");

	vars.TotalTimeInSeconds = 0f;
	
	settings.Add("maingame", false, "Main Campaign");
	settings.CurrentDefaultParent = "maingame";
	settings.Add("meme", false, "Enable No Guest House%");
	settings.Add("splits", false, "Main Game Splits");
	settings.CurrentDefaultParent = "splits";
	settings.Add("c01_Corridor01", false, "Reached the Guest House");
	settings.Add("ChainCutter", false, "Bolt Cutters");
	settings.Add("HandAxe", false, "Axe (first time)");
	settings.Add("Fuse", false, "Fuse (Guest House)");
	settings.Add("welcome2family", false, "Welcome to the family (splits during drag scene)");
	settings.Add("100family", false, "100% run (splits during Zoe stapling hand)", "welcome2family");
	settings.Add("FloorDoorKey", false, "Hatch Key");
	settings.Add("Knife", false, "Knife");
	settings.Add("Handgun_M19", false, "M19 (Guest House gun)");
	settings.Add("Handgun_G17", false, "G17 (Garage gun)");
	settings.Add("EthanCarKey", false, "Car Key");
	settings.Add("EntranceHallKey", false, "Ox Statuette");
	settings.Add("PendulumClock", false, "Clock Pendulum");
	settings.Add("3CrestKeyA", false, "Blue Dog Head");
	settings.Add("SilhouettePazzlePiece", false, "Wooden Statuette");
	settings.Add("3CrestKeyB", false, "White Dog Head");
	settings.Add("WorkroomKey", false, "Dissection Room Key");
	settings.Add("3CrestKeyC", false, "Red Dog Head");
	settings.Add("ChainSaw", false, "Chainsaw");
	settings.Add("chainsawduel", false, "Finished chainsaw fight");
	settings.Add("MorgueKey", false, "Scorpion Key");
	settings.Add("c03_TrailerHouse", false, "Reached the trailer (first time)");
	settings.Add("c03_OldHouse1FEntrance01", false, "Reached the Old House");
	settings.Add("BurnerPartsA", false, "Burner Grip");
	settings.Add("BurnerPartsB", false, "Burner Nozzle");
	settings.Add("SilhouettePazzlePieceOldHouse", false, "Stone Statue");
	settings.Add("Crank", false, "Crank");
	settings.Add("TalismanKey", false, "Crow Key");
	settings.Add("c03_OldHouse2FHallway02", false, "Entered Crow Door (after falling down hole)");
	settings.Add("Lantern", false, "Lantern");
	settings.Add("usedlantern", false, "Placed Lantern (to unlock door)");
	settings.Add("SerumMaterialA", false, "D-Series Arm");
	settings.Add("Magnum", false, "Magnum");
	settings.Add("MasterKey", false, "Snake Key");
	settings.Add("GrenadeLauncher", false, "Grenade Launcher");
	settings.Add("LucasCardKey2", false, "Red Key Card");
	settings.Add("LucasCardKey", false, "Blue Keycard");
	settings.Add("c03_LeftArea2FTvRoom", false, "Reached Lucas TV room");
	settings.Add("Battery", false, "Battery");
	settings.Add("Candle", false, "Candle");
	settings.Add("Valve", false, "Valve Handle");
	settings.Add("Timebomb", false, "Timebomb");
	settings.Add("SerumMaterialB", false, "D-Series Head");
	settings.Add("SerumComplete", false, "Completed Serum");
	settings.Add("hittheroadjack", false, "Injected Jack");
	settings.Add("EvelynRadar1", false, "Mia Start");
	settings.Add("fuse2", false, "Fuse 2 (ship)");
	settings.Add("FoundFootage050", false, "Mia Videotape (picked up)");
	settings.Add("videotapeend", false, "Mia Tape End");
	settings.Add("EvOpener", false, "Lug Wrench");
	settings.Add("EvCable", false, "Power Cable");
	settings.Add("fuse3", false, "Retrieved Fuse (from door on ship)");
	settings.Add("EvelynRadar4", false, "Mia Complete");
	settings.Add("SerumTypeE", false, "Necrotoxin");
	settings.Add("injectedbitch", false, "Injected Evie");
	settings.Add("Handgun_Albert", false, "Albert Gun (Playtime's over)");
	settings.Add("end", false, "End");
	settings.CurrentDefaultParent = null;

	
	settings.Add("nah", false, "Not a Hero");
	settings.CurrentDefaultParent = "nah";
	settings.Add("c08_SaltMineCorridor01", false, "Reached the new section of the mine");
	settings.Add("KeyItem05Ch8", false, "Gear");
	settings.Add("KeyItem03Ch8", false, "Crank");
	settings.Add("Ch8CageKey", false, "Cage Key");
	settings.Add("c08_MiningRoom01", false, "Entered soldier 2's cell");
	settings.Add("Handgun_Albert_C", false, "Handgun (Professional mode only!)");
	settings.Add("AlbertHandgunBulletL", false, "Ramrods (first time, near night vision)");
	settings.Add("MineMasterKey", false, "Clown Key");
	settings.Add("Shotgun_Albert", false, "Shotgun (Professional mode only!)");
	settings.Add("c08_TrainPassage05", false, "Saved soldier 3 (triggers in room with ladder)");
	settings.Add("c08_MiningTunnel01", false, "Reached fat molded boss room");
	settings.Add("nah_bombgone", false, "Beat fat molded/Removed bomb (triggers after leaving area after removing bomb)");
	settings.Add("c08_ShieldMachine_Entra", false, "Entered the Blast Door");
	settings.Add("c08_MineTerminal01", false, "Escaped the explosion (post-horde)");
	settings.Add("lucsbossRoom", false, "Reached the Lucas boss room");
	settings.Add("nah_end", false, "End");
	settings.CurrentDefaultParent = null;
	
	settings.Add("eoz", false, "End of Zoe");
	settings.CurrentDefaultParent = "eoz";
	settings.Add("NumaItem030", false, "Cure for Type-E Infection");
	settings.Add("eoz_usedcure", false, "Used Cure");
	settings.Add("c09_CampSafeRoomInside", false, "Placed Zoe in Safe Room (triggers upon entering)");
	settings.Add("c09_SteamBoat3F", false, "Reached the Boat");
	settings.Add("NumaItem031", false, "Second Cure for Type-E Infection (on boat)");
	settings.Add("c09_SteamBoatB1FCorridor01", false, "Defeated Jack (on boat)");
	settings.Add("c09_MoldSwampGround05", false, "Made it through the Alligator Swamp");
	settings.Add("c09_Cemetery", false, "Reached the Cemetery");
	settings.Add("c09_Church", false, "Reached the Church");
	settings.Add("CH9_WP001", false, "AMG-78");
	settings.Add("c03_MainHouseHall", false, "Reached the Baker's Main Hall");
	settings.Add("eoz_end", false, "End");
	settings.CurrentDefaultParent = null;
	
	settings.Add("55th", false, "Jack's 55th Birthday");
	
	vars.hashToVersion = new Dictionary<string, string> {
		// Steam
		{ "9EDD76273F6653F2B39DE5B9CBB6EFA4", "NextGen" },
		{ "401BA759C4F1FE95ED06B33084FAA187", "6/10/22"}
	};
}

init
{
	vars.splits = new HashSet<string>();
	vars.inventoryPtr = IntPtr.Zero;
	vars.fuse3PickedUp = 0;
	vars.fuse2PickedUp = 0;

	vars.Jack55Full = 0;
	vars.Jack55Timer = 0;
	vars.Jack55Finish = 0;
	

	switch (modules.First().ModuleMemorySize)
	{
		case (241680384):
			version = "1.2";
			vars.inventoryPtr = 0x7091CA0;
			break;
		case (162590720):
			version = "cerod_nvidia";
			vars.inventoryPtr = 0x9322E10;
			break;
		case (248446976):
			version = "1.3";
			vars.inventoryPtr = 0x70DADB0;
			break;
		case (272252928):
		case (142016512):
			version = "1.4";
			vars.inventoryPtr = 0x81E4148;
			break;
		case (162668544):
		case (342978560):
			version = "cerod";
			vars.inventoryPtr = 0x93352C0;
			break;
		case (142069760):
			version = "Buy RE8!! Version";
			vars.inventoryPtr = 0x081F24E8;
			break;
		case (142065664):
			version = "12/17 Update";
			vars.inventoryPtr = 0x81F1308;
			break;
		case (162783232):
			version = "CeroD 20.4.0.2";
			vars.inventoryPtr = 0x9355468;
			break;
		default:
			version = "1.1";
			vars.inventoryPtr = 0x707FCD0;
			break;
	}
	
	if(version == "STEAM-NextGen"){
		vars.inventoryPtr = 0x8FB9B48;
	}
	
	if(version == "STEAM-6/10/22"){
		vars.inventoryPtr = 0x8FB9CC8;
	}

	// Track inventory IDs
	if (version == "STEAM-NextGen" || version == "STEAM-6/10/22"){
		current.inventory = new string[20].Select((_, i) => {
		StringBuilder sb = new StringBuilder(300);
		IntPtr ptr;
		new DeepPointer(vars.inventoryPtr, 0x60, 0x10, 0x20 + (i * 8), 0x18, 0x80, 0x14).DerefOffsets(memory, out ptr);
		memory.ReadString(ptr, sb);
		return sb.ToString();
		}).ToArray();
	}
	
	else{
		current.inventory = new string[20].Select((_, i) => {
		StringBuilder sb = new StringBuilder(300);
		IntPtr ptr;
		new DeepPointer(vars.inventoryPtr, 0x60, 0x20, 0x30 + (i * 8), 0x28, 0x80, 0x24).DerefOffsets(memory, out ptr);
		memory.ReadString(ptr, sb);
		return sb.ToString();
		}).ToArray();
	}
	
	if(version != "CeroD 20.4.0.2"){
		vars.isLoading = false;
		vars.gameModule = modules.First();
		// Default Value is something like: `K:\RemnantFromTheAshes\Remnant\Binaries\Win64\Remnant-Win64-Shipping.exe`
		// Technically you can (easily) have an Epic install without the .egstore due to the way Epic launches their games but y'know *meh*
		vars.gameStorefront = Directory.Exists(vars.gameModule.FileName + "/../../../../.egstore") ? "EGS" : "STEAM";
		
		// Creating a hash of the file seems to be a relatively *ok* way of detecting the version.
		// For some reason getting the product version from the exe itself, doesn't work, and it just returns an empty string
		// You could fix this by creating a DLL Component instead of an ASL, which is alot of effort and I don't feel like doing that.
		using (var stream = new FileStream(vars.gameModule.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 16 * 1024 * 1024))
		{
			byte[] checksum = System.Security.Cryptography.MD5.Create().ComputeHash(stream);
			vars.gameHash = BitConverter.ToString(checksum).Replace("-", String.Empty);
		}

		if(!vars.hashToVersion.ContainsKey(vars.gameHash)) {
			print("[Remnant ASL]: Unknown/Unsupported Game Hash: " + vars.gameHash.ToString());
			MessageBox.Show("Unknown Game Hash: \"" + vars.gameHash.ToString() + "\" \n Contact the developers for help!\nHash Copied to clipboard...", "Remnant ASL", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Clipboard.SetText(vars.gameHash.ToString());
			return;
		}

		version = (vars.gameStorefront + "-" + vars.hashToVersion[vars.gameHash]);
		print("[Remnant ASL]: Game Storefront: " + vars.gameStorefront.ToString());
		print("[Remnant ASL]: Game Hash: " + vars.gameHash.ToString());
		print("[Remnant ASL]: ASL Version: " + version.ToString());
	}
}

onStart
{
	vars.TotalTimeInSeconds = 0f;
}

start
{	
	if (settings["nah"]){
		return (current.map == "c04_CavePassage01" && current.inventory[0] == "CKnife" || current.map == "c04_Ship3FInfirmaryPast");
	}
	if (settings["eoz"]){
		return (current.map == "sm0878_Carpet08A" || current.map == "c04_Ship3FInfirmaryPast" || current.map == "c09_JoeHouseInside" && current.inventory[0] == "NumaItem071");
	}
	if (settings["meme"]){
		return ((current.inventory[0] == "Knife" || current.inventory[1] == "Knife") && current.map == "c03_MainHouse1FWash");
	}
	if (settings["55th"]){
		return (current.Jack55Start == 1 && old.Jack55Start == 0);
	}

	return current.map == "c04_Ship3FInfirmaryPast";
}

update
{
	//print(modules.First().ModuleMemorySize.ToString());
	
	// Track inventory IDs
	if (version == "Next Gen" || version == "6/10/22"){
		current.inventory = new string[20].Select((_, i) => {
		StringBuilder sb = new StringBuilder(300);
		IntPtr ptr;
		new DeepPointer(vars.inventoryPtr, 0x60, 0x10, 0x20 + (i * 8), 0x18, 0x80, 0x14).DerefOffsets(memory, out ptr);
		memory.ReadString(ptr, sb);
		return sb.ToString();
		}).ToArray();
	}
	
	else{
		current.inventory = new string[20].Select((_, i) => {
		StringBuilder sb = new StringBuilder(300);
		IntPtr ptr;
		new DeepPointer(vars.inventoryPtr, 0x60, 0x20, 0x30 + (i * 8), 0x28, 0x80, 0x24).DerefOffsets(memory, out ptr);
		memory.ReadString(ptr, sb);
		return sb.ToString();
		}).ToArray();
	}
		
	vars.isdead = (current.isdying == 0 ? 1 : 0);

	if (timer.CurrentPhase == TimerPhase.NotRunning) { vars.splits.Clear(); vars.fuse2PickedUp = 0; vars.fuse3PickedUp = 0; vars.Jack55Full = 0; vars.Jack55Finish = 0; vars.Jack55Timer = 0; vars.Helper.Texts["Total Time"].Right = "00:00.000";}
	
	
	if(settings["55th"]){	
	
		vars.Helper.Texts["Total Time"].Left = "Time:";
		vars.Helper.Texts["Total Time"].Right = "00:00.000";
		
		if(current.Jack55Level == 0 || current.Jack55Level == 2 || current.Jack55Level == 5){
			vars.Jack55Timer = 900000;
		}
		if(current.Jack55Level == 1){
			vars.Jack55Timer = 600000;
		}
		if(current.Jack55Level == 3){
			vars.Jack55Timer = 300000;
		}
		if(current.Jack55Level == 4){
			vars.Jack55Timer = 480000;
		}
		
		if(current.Jack55End == 1 && old.Jack55End == 0){
			vars.Jack55Full = current.Jack55IGT + current.Jack55Extra - current.Jack55Bonus;
			vars.Jack55Finish = vars.Jack55Timer - vars.Jack55Full;
			vars.TotalTimeInSeconds += vars.Jack55Finish;
			
			vars.Helper.Texts["Total Time"].Right = TimeSpan.FromMilliseconds(vars.TotalTimeInSeconds).ToString(@"mm\:ss\.fff");
		}
	}
	
	if (!settings["55th"]){
		vars.Helper.Texts.RemoveAll();
		}
}

split
{
	// Item splits
	string[] currentInventory = (current.inventory as string[]);
	string[] oldInventory = (old.inventory as string[]); // throws error first update, will be fine afterwards.
	if (!currentInventory.SequenceEqual(oldInventory)){
		string[] delta = (currentInventory as string[]).Where((v, i) => v != oldInventory[i]).ToArray();

		foreach (string item in delta){
			if (item == "FuseCh4"){
				if (vars.fuse2PickedUp == 0 && current.map != "c04_Ship1FCorridor"){
					vars.fuse2PickedUp = 1;
					return settings["fuse2"];
				}
				else if (vars.fuse3PickedUp == 0 && current.map == "c04_Ship1FCorridor"){
					if (settings["fuse2"]){
						if (vars.fuse2PickedUp == 1){
							vars.fuse3PickedUp = 1;
							return settings["fuse3"];
						}
					}
					else{
						vars.fuse3PickedUp = 1;
						return settings["fuse3"];
					}
				}
			}
			else if (!vars.splits.Contains(item)){
				vars.splits.Add(item);
				return settings[item];
			}
		}
	}


	//Removed item splits

	var removedItems = oldInventory.Except(currentInventory);

	if (removedItems.Contains("ChainSaw") && vars.isdead == 0){
		if (!vars.splits.Contains("removedSaw")){
			vars.splits.Add("removedSaw");
			return settings["chainsawduel"];
		}
	}

	if (currentInventory.Count(v => v == "SerumComplete") == 1){
		if (!vars.splits.Contains("injectedJack")){
			vars.splits.Add("injectedJack");
			return settings["hittheroadjack"];
		}
	}

	if (removedItems.Contains("Lantern") && vars.isdead == 0){
		if (!vars.splits.Contains("usedTheLantern")){
			vars.splits.Add("usedTheLantern");
			return settings["usedlantern"];
		}
	}

	if (removedItems.Contains("SerumTypeE") && vars.isdead == 0 && current.map == "c04_c013F"){
		if (!vars.splits.Contains("injectedEvieWithSerum")){
			vars.splits.Add("injectedEvieWithSerum");
			return settings["injectedbitch"];
		}
	}

	if (removedItems.Contains("Handgun_Albert")){
		if (!vars.splits.Contains("endMainCampaign")){
			vars.splits.Add("endMainCampaign");
			return settings["end"];
		}
	}

	if (removedItems.Contains("MachineGun")){
		if (!vars.splits.Contains("videotapeend")){
			vars.splits.Add("videotapeend");
			return settings["videotapeend"];
		}
	}

	if (settings["eoz"])
	{
		if (!vars.splits.Contains(""))
		{
			vars.splits.Add("");
		}
	}
	
	if (removedItems.Contains("NumaItem030")){
		if (!vars.splits.Contains("zoecuresplit")){
			vars.splits.Add("zoecuresplit");
			return settings["eoz_usedcure"];
		}
	}

	// Map splits
	if (current.map != old.map){
		if (current.map == "c08_MiningPassage02_static" && old.map == "c08_MiningTunnel01" && !vars.splits.Contains(current.map) || current.map == "c08_MiningPassage02" && old.map == "c08_MiningTunnel01" && !vars.splits.Contains(current.map)){
			vars.splits.Add(current.map);
			return settings["nah_bombgone"];
		}
		else if (!vars.splits.Contains(current.map)){
			vars.splits.Add(current.map);
			return settings[current.map];
		}
	}

	if (current.map == "c01_Outside01" && current.inventory[0] != "MailMia" && settings["100family"] == false && !vars.splits.Contains("welcome2family"))
	{
		vars.splits.Add("welcome2family");
		return settings["welcome2family"];
	}
	
	if (current.map == "c03_MainHouse1FLDK" && !vars.splits.Contains("100family"))
	{
		vars.splits.Add("100family");
		return settings["100family"];
	}
	
	if (current.map == "c08_BossRoom01" && !vars.splits.Contains("lucsbossRoom"))
	{
		vars.splits.Add("lucsbossRoom");
		return settings["lucsbossRoom"];
	}

    if (old.map == "c08_BossRoom01" && current.inventory[0] != old.inventory[0] && !vars.splits.Contains("nah_end"))
	{
		vars.splits.Add("nah_end");
		return settings["nah_end"];
	}

    if (current.map == "c03_MainHouseHall" && current.inventory[0] != "NumaItem031" && !vars.splits.Contains("eoz_end"))
	{
		vars.splits.Add("eoz_end");
		return settings["eoz_end"];
	}
	
	if (settings["55th"]){
		if(current.Jack55End == 1 && old.Jack55End == 0){
			return true;
		}
	}
}

isLoading
{	
	if(settings["55th"]){
		return current.gamePauseState != 0 && current.gamePauseState != 262400 && current.gamePauseState != 8 && current.gamePauseState != 262144 || current.Jack55End == 1;
	}
	
	else return current.gamePauseState != 0 && current.gamePauseState != 262400 && current.gamePauseState != 8 && current.gamePauseState != 262144;
}

reset
{
	if (settings["55th"]){
		return current.gamePauseState == 0 && old.gamePauseState == 256;
	}
}

shutdown
{
}
