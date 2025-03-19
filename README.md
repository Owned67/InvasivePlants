# InvasivePlants
Seed and clone planting controller. Requires players to plant seeds and clones in planter boxes.

Modern solution for unmaintained plugin Agriblock by Death.

# Configuration
```json
{
  "ReturnItem": true,
  "ItemBlacklistShortnames": []
}
```
**Properties**
* **ReturnItem (true/false):** Enable/disable returning seeds & clones to players when attempting to plant outside of a planter
* **ItemIgnoreListShortnames (list strings):** List of item shortnames to ignore planter check for

**Item ignore list example**
```json
"ItemIgnoreListShortnames": [
  "clone.hemp",
  "seed.hemp"
]
```

Full list of item shortnames on Rust: https://www.corrosionhour.com/rust-item-list/
