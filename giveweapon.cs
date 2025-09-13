using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Logging;

namespace GiveWeapon;

[MinimumApiVersion(284)]

public class GiveWeapon : BasePlugin
{
    public override string ModuleName => "GiveWeapon";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "kanoah";
    public override string ModuleDescription => "Adds a command to give weapons";
    public string AccessFlag { get; set; } = "@css/root, @css/chat";
    public string Command { get; set; } = "css_give";

    public override void Load(bool hotReload)
    {
        Logger.LogInformation($"Loaded version {ModuleVersion}!");
        AddCommand($"{Command}", "Gives you a weapon!", WeaponCommand);
    }
    
    public void WeaponCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (!AdminManager.PlayerHasPermissions(player, AccessFlag))
        {
            command.ReplyToCommand("You do not have permission to use this command.");
            return;
        }
        

        if (player == null)
        {
            return;
        }

        string? inputWeaponName = command.ArgByIndex(1)?.ToLower();

        if (inputWeaponName == null)
        {
            return;
        }
        
        string? matchedWeapon = WeaponList
            .Where(weapon => weapon.Key.Contains(inputWeaponName, StringComparison.CurrentCultureIgnoreCase))
            .Select(weapon => weapon.Value)
            .FirstOrDefault();

        if (matchedWeapon == null)
        {
            command.ReplyToCommand("Invalid weapon.");
            return;
        }

        if (!player.PawnIsAlive)
        {
            command.ReplyToCommand("You can't use this command while you're dead!");
            return;
        }

        player.GiveNamedItem(matchedWeapon);
        command.ReplyToCommand("Weapon given.");
    }

    private static readonly Dictionary<string, string> WeaponList = new Dictionary<string, string>
    {
        { "usp", "weapon_usp_silencer" },
        { "glock", "weapon_glock" },
        { "m4a1s", "weapon_m4a1_silencer" },
        { "m4a4", "weapon_m4a4" },
        { "deagle", "weapon_deagle" },
        { "cz75", "weapon_cz75a" },
        { "p250", "weapon_p250" },
        { "tec9", "weapon_tec9" },
        { "57", "weapon_fiveseven" },
        { "r8", "weapon_revolver" },
        { "awp", "weapon_awp" },
        { "scout", "weapon_ssg08" },
        { "galil", "weapon_galil" },
        { "ak47", "weapon_ak47" },
        { "elite", "weapon_elite" },
        { "aug", "weapon_aug" },
        { "famas", "weapon_famas" },
        { "sg556", "weapon_sg556" },
        { "scar", "weapon_scar20" },
        { "p2000", "weapon_p2000" },
        { "g3sg1", "weapon_g3sg1" },
        { "mac10", "weapon_mac10" },
        { "mp5sd", "weapon_mp5sd" },
        { "mp7", "weapon_mp7" },
        { "mp9", "weapon_mp9" },
        { "bizon", "weapon_bizon" },
        { "p90", "weapon_p90" },
        { "ump45", "weapon_ump45" },
        { "mag7", "weapon_mag7" },
        { "nova", "weapon_nova" },
        { "sawed", "weapon_sawedoff" },
        { "xm", "weapon_xm1014" },
        { "m249", "weapon_m249" },
        { "negev", "weapon_negev" },
        { "zeus", "weapon_taser" },
        { "taser", "weapon_taser" },
        { "he", "weapon_hegrenade" },
        { "flash", "weapon_flashbang" },
        { "incendiary", "weapon_incgrenade" },
        { "molotov", "weapon_molotov" },
        { "decoy", "weapon_decoy" },
        { "smoke", "weapon_smokegrenade" },
        { "c4", "weapon_c4" }
    };
}