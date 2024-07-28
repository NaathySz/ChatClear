using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Utils;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChatClear;

public class ChatClearConfig : BasePluginConfig
{
    [JsonPropertyName("admins_only")]
    public bool AdminsOnly { get; set; } = false;

    [JsonPropertyName("chat_clear_permission")]
    public string ChatClearPermission { get; set; } = "@css/generic";

    [JsonPropertyName("chat_clear_commands")]
    public List<string> ChatClearCommands { get; set; } = ["clearchat"];

    [JsonPropertyName("chat_clear_for_all_commands")]
    public List<string> ChatClearForAllCommands { get; set; } = ["clearchatforall", "clearallchat"];

    [JsonPropertyName("chat_clear_for_all_permission")]
    public string ChatClearForAllPermission { get; set; } = "@css/slay";
}

public class ChatClear : BasePlugin, IPluginConfig<ChatClearConfig>
{
    public override string ModuleName => "Chat Cleaner";
    public override string ModuleVersion => "1.0.1";
    public override string ModuleAuthor => "Nathy";
    public override string ModuleDescription => "Clear the chat for a player or for all players.";

    public ChatClearConfig Config { get; set; } = new();

    public void OnConfigParsed(ChatClearConfig config)
    {
        Config = config;
    }

    public override void Load(bool hotReload)
    {
        CreateCommands();
    }

    private void CreateCommands()
    {
        foreach (var cmd in Config.ChatClearCommands)
        {
            AddCommand($"css_{cmd}", "Clear chat for the player", ClearChatForPlayer);
        }

        foreach (var cmd in Config.ChatClearForAllCommands)
        {
            AddCommand($"css_{cmd}", "Clear chat for all players", ClearChatForAll);
        }
    }

    public void ClearChatForPlayer(CCSPlayerController? caller, CommandInfo command)
    {
        if (caller != null)
        {
            if (Config.AdminsOnly && !AdminManager.PlayerHasPermissions(caller, Config.ChatClearPermission))
            {
                caller.PrintToChat(Localizer["No permission"]);
                return;
            }

            string invisibleChar = "ㅤ";

            for (int i = 0; i < 140; i++)
            {
                caller.PrintToChat(invisibleChar);
            }

            // repeating cuz the spam not work at once
            for (int i = 0; i < 140; i++)
            {
                caller.PrintToChat(invisibleChar);
            }
        }
        else
        {
            command.ReplyToCommand(Localizer["Can only be executed by player"]);
        }
    }

    public void ClearChatForAll(CCSPlayerController? caller, CommandInfo command)
    {
        if (caller != null && !AdminManager.PlayerHasPermissions(caller, Config.ChatClearForAllPermission))
        {
            caller.PrintToChat(Localizer["No permission"]);
            return;
        }

        string invisibleChar = "ㅤ";

        for (int i = 0; i < 140; i++)
        {
            Server.PrintToChatAll(invisibleChar);
        }

        // repeating cuz the spam not work at once
        for (int i = 0; i < 140; i++)
        {
            Server.PrintToChatAll(invisibleChar);
        }

        Server.PrintToChatAll(Localizer["Chat cleared", caller.PlayerName]);
    }
}
