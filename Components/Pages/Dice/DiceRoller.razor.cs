namespace DnDNoteKeeper.Components.Pages.Dice;

public partial class DiceRoller
{
    private static readonly int[] QuickDice = [4, 6, 8, 10, 12, 20];

    private int customCount = 1;
    private int customSides = 20;
    private int customModifier = 0;

    private RollResult? currentResult;
    private readonly List<RollResult> history = [];
    private readonly Random _rng = new();

    private void QuickRoll(int sides)
    {
        var result = Roll(1, sides, 0);
        PushResult(result);
    }

    private void RollCustom()
    {
        var count = Math.Clamp(customCount, 1, 100);
        var result = Roll(count, customSides, customModifier);
        PushResult(result);
    }

    private RollResult Roll(int count, int sides, int modifier)
    {
        var rolls = Enumerable.Range(0, count).Select(_ => _rng.Next(1, sides + 1)).ToList();
        var total = rolls.Sum() + modifier;
        var label = count == 1
            ? $"d{sides}{(modifier != 0 ? (modifier >= 0 ? $"+{modifier}" : $"{modifier}") : "")}"
            : $"{count}d{sides}{(modifier != 0 ? (modifier >= 0 ? $"+{modifier}" : $"{modifier}") : "")}";

        return new RollResult(label, sides, rolls, modifier, total);
    }

    private void PushResult(RollResult result)
    {
        currentResult = result;
        history.Insert(0, result);
        if (history.Count > 20)
            history.RemoveAt(history.Count - 1);
    }

    private void ClearHistory()
    {
        history.Clear();
        currentResult = null;
    }

    private record RollResult(string Label, int Sides, List<int> Rolls, int Modifier, int Total);
}