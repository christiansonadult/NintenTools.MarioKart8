#pragma warning disable 1591 // Single elements do not require to be documented.

namespace Syroot.NintenTools.MarioKart8.BinData.Item
{
    /// <summary>
    /// Represents the available sections in an Item.bin file.
    /// </summary>
    public enum Section
    {
        RaceTitleSets, RaceSets,
        Distances,
        BattleTitleSets, BattleSets,
        BalloonCounts
    }

    /// <summary>
    /// Represents the item sets available for versus races.
    /// </summary>
    public enum RaceItemSet
    {
        GrandPrix, GrandPrixAI,
        All, AllAI,
        Mushrooms, MushroomsAI,
        Shells, ShellsAI,
        Bananas, BananasAI,
        Bobombs, BobombsAI,
        UnusedMushrooms1, UnusedMushrooms1AI,
        UnusedShells,
        UnusedBananas,
        UnusedBobombs,
        UnusedMushrooms2,
        Frantic, FranticAI
    }

    /// <summary>
    /// Represents the distances used according to AI players participating in the race.
    /// </summary>
    public enum DistanceType
    {
        NoAI,
        WithAI
        // 18 groups are unused
    }

    /// <summary>
    /// Represents the item sets available for battle mode.
    /// </summary>
    public enum BattleItemSet
    {
        All, AllAI,
        Mushrooms, MushroomsAI,
        Shells, ShellsAI,
        Bananas, BananasAI,
        Bobombs, BobombsAI,
        UnusedShellsBananas1,
        UnusedMushrooms,
        UnusedShells,
        UnusedBananas,
        UnusedBobombs,
        UnusedShellsBananas2,
        Frantic, FranticAI
    }
}

#pragma warning restore 1591
