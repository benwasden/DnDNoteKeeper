namespace DnDNoteKeeper.Data;

public static class GameConstants
{
    public static readonly string[] StatNames = 
        ["Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma"];

    public static readonly string[] Races =
        ["Human", "Elf", "Dwarf", "Halfling", "Gnome", "Half-Elf", "Half-Orc", "Tiefling", "Dragonborn"];

    public static readonly string[] Classes =
        ["Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard"];

    public static readonly string[] Backgrounds =
        ["Acolyte", "Charlatan", "Criminal", "Entertainer", "Folk Hero", "Guild Artisan", "Hermit", "Noble", "Outlander", "Sage", "Sailor", "Soldier", "Urchin"];

    public static readonly string[] Alignments =
        ["Lawful Good", "Neutral Good", "Chaotic Good", "Lawful Neutral", "True Neutral", "Chaotic Neutral", "Lawful Evil", "Neutral Evil", "Chaotic Evil"];

    public static readonly Dictionary<string, int> ClassHitDice = new()
    {
        ["Barbarian"] = 12, ["Fighter"] = 10, ["Paladin"] = 10, ["Ranger"] = 10,
        ["Bard"] = 8, ["Cleric"] = 8, ["Druid"] = 8, ["Monk"] = 8,
        ["Rogue"] = 8, ["Warlock"] = 8, ["Sorcerer"] = 6, ["Wizard"] = 6
    };
}