using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace GiveWeapon
{
    public class GiveWeaponConfig : BasePluginConfig
    {
        [JsonPropertyName("Command")]
        public string Command { get; set; } = "css_give";

        [JsonPropertyName("AccessFlag")]
        public string AccessFlag { get; set; } = "@css/root, @css/chat";
    };
}