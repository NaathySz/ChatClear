## ChatCleaner
A plugin to clear chat. Includes two commands: 
- Clear your own chat
- Clear chat for all players

Both configurable in the config file.

## Config
The config will be auto generated like this:
```json
{
  "admins_only": false,
  "chat_clear_permission": "@css/generic",
  "chat_clear_commands": [
    "clearchat"
  ],
  "chat_clear_for_all_commands": [
    "clearchatforall",
    "clearallchat"
  ],
  "chat_clear_for_all_permission": "@css/slay",
  "ConfigVersion": 1
}
```
