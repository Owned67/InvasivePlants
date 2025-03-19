# InvasivePlants
Seed and clone planting controller. Requires players to plant seeds and clones in planter boxes.

Modern solution for unmaintained plugin Agriblock by Death.

# Permissions
*  `invasiveplants.bypass` Bypass planter check for players with this permission allowing them to plant seeds and clones anywhere.

# Configuration
**Example**
```json
{
  "EnableChatMessage": true,
  "ReturnItem": true,
  "ItemBlacklistShortnames": [],
  "CallOnDenyPlantHook": false
}
```

**Properties**
* `EnableChatMessage` Enable or disable sending message to user when attempting to plant outside of a planter.
* `ReturnItem` Enable or disable returning seeds & clones to players when attempting to plant outside of a planter.
* `ItemIgnoreListShortnames (list strings)` List of item shortnames to ignore planter check for.
* `CallOnDenyPlantHook` For developers - If you don't need this or don't know what it's for you can leave it as `false`. If `true` this hook will be called when a player is denied planting outside of a planter.

**Item ignore list example**
```json
"ItemIgnoreListShortnames": [
  "clone.hemp",
  "seed.hemp"
]
```
Full list of item shortnames on Rust: https://www.corrosionhour.com/rust-item-list/
