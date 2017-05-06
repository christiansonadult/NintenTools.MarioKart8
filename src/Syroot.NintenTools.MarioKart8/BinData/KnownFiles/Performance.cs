#pragma warning disable 1591 // Single elements do not require to be documented.

namespace Syroot.NintenTools.MarioKart8.BinData.Performance
{
    /// <summary>
    /// Represents the available sections in a Performance.bin file.
    /// </summary>
    public enum Section
    {
        WeightStats, AccelerationStats, OnroadStats, OffroadStats, TurboStats,
        SpeedGroundStats, SpeedWaterStats, SpeedAntigravityStats, SpeedAirStats,
        HandlingGroundStats, HandlingWaterStats, HandlingAntigravityStats, HandlingAirStats,
        KartPoints, DriverPoints, TirePoints, GliderPoints
    }

    /// <summary>
    /// Represents the weight stats stored at the resulting group element indices.
    /// </summary>
    public enum WeightStat
    {
        Bumped, Bumping, Unknown
    }

    /// <summary>
    /// Represents the offroad stats stored at the resulting group element indices.
    /// </summary>
    public enum OffroadStat
    {
        BrakeDirtLight, BrakeDirtMedium, BrakeDirtHeavy,
        BrakeSandLight, BrakeSandMedium, BrakeSandHeavy,
        BrakeIceLight, BrakeIceMedium, BrakeIceHeavy,
        SlipDirtLight, SlipDirtMedium, SlipDirtHeavy,
        SlipSandLight, SlipSandMedium, SlipSandHeavy,
        SlipIceLight, SlipIceMedium, SlipIceHeavy
    }
    
    /// <summary>
    /// Represents the turbo stats stored at the resulting group element indices.
    /// </summary>
    public enum TurboStat
    {
        MiniTurboFrames, SuperTurboFrames
    }

    /// <summary>
    /// Represents the speed stats stored at the resulting group element indices.
    /// </summary>
    public enum SpeedStat
    {
        Speed, MaxSpeed
    }
    
    /// <summary>
    /// Represents the speed air stats stored at the resulting group element indices.
    /// </summary>
    public enum SpeedAirStat
    {
        Speed
    }

    /// <summary>
    /// Represents the handling stats stored at the resulting group element indices.
    /// </summary>
    public enum HandlingStat
    {
        Normal, Drift, AutoDrift
    }

    /// <summary>
    /// Represents the handling air stats stored at the resulting group element indices.
    /// </summary>
    public enum HandlingAirStat
    {
        Roll, Move
    }

    /// <summary>
    /// Represents the point set attributes at the resulting group element indices.
    /// </summary>
    public enum PointSetAttrib
    {
        Weight, Acceleration, OnroadTraction, OffroadTraction, MiniTurbo,
        SpeedGround, SpeedWater, SpeedAntigravity, SpeedAir,
        HandlingGround, HandlingWater, HandlingAntigravity, HandlingAir
    }

    /// <summary>
    /// Represents the kart point sets stored at the resulting group indices of Mario Kart 8.
    /// </summary>
    public enum KartPointSetMK8
    {
        StandardKart,
        PipeFrame,
        Mach8,
        SteelDriver,
        CatCruiser,
        CircuitSpecial,
        TriSpeeder,
        Badwagon,
        Prancer,
        Biddybuggy,
        Landship,
        Sneeker,
        SportsCoupe,
        GoldStandard,
        StandardBike,
        Comet,
        SportBike,
        TheDuke,
        FlameRider,
        Varmint,
        MrScooty,
        JetBike,
        YoshiBike,
        StandardAtv,
        WildWiggler,
        TeddyBuggy,
        Gla,
        SilverArrow,
        SlRoadster,
        BlueFalcon,
        TanookiKart,
        BDasher,
        MasterCycle,
        Unused1,
        Unused2,
        Streetle,
        PWing,
        CityTripper,
        BoneRattler
    }

    /// <summary>
    /// Represents the kart point sets stored at the resulting group indices of Mario Kart 8 Deluxe.
    /// </summary>
    public enum KartPointSetMK8D
    {
        StandardKart,
        PipeFrame,
        Mach8,
        SteelDriver,
        CatCruiser,
        CircuitSpecial,
        TriSpeeder,
        Badwagon,
        Prancer,
        Biddybuggy,
        Landship,
        Sneeker,
        SportsCoupe,
        GoldStandard,
        StandardBike,
        Comet,
        SportBike,
        TheDuke,
        FlameRider,
        Varmint,
        MrScooty,
        JetBike,
        YoshiBike,
        StandardAtv,
        WildWiggler,
        TeddyBuggy,
        Gla,
        SilverArrow,
        SlRoadster,
        BlueFalcon,
        TanookiKart,
        BDasher,
        MasterCycle,
        SplatBuggy,
        Inkstriker,
        Streetle,
        PWing,
        KoopaClown,
        CityTripper,
        BoneRattler
    }

    /// <summary>
    /// Represents the driver point sets stored at the resulting group indices of Mario Kart 8.
    /// </summary>
    public enum DriverPointSetMK8
    {
        Mario,
        Luigi,
        Peach,
        Daisy,
        Yoshi,
        Toad,
        Toadette,
        KoopaTroopa,
        Bowser,
        DonkeyKong,
        Wario,
        Waluigi,
        Rosalina,
        MetalMario,
        PinkGoldPeach,
        Lakitu,
        ShyGuy,
        BabyMario,
        BabyLuigi,
        BabyPeach,
        BabyDaisy,
        BabyRosalina,
        Larry,
        Lemmy,
        Wendy,
        Ludwig,
        Iggy,
        Roy,
        Morton,
        Mii,
        TanookiMario,
        Link,
        VillagerMale,
        Isabelle,
        CatPeach,
        DryBowser,
        VillagerFemale
    }

    /// <summary>
    /// Represents the driver point point sets stored at the resulting group indices of Mario Kart 8 Deluxe.
    /// </summary>
    public enum DriverPointSetMK8D
    {
        Mario,
        Luigi,
        Peach,
        Daisy,
        Yoshi,
        Toad,
        Toadette,
        KoopaTroopa,
        Bowser,
        DonkeyKong,
        Wario,
        Waluigi,
        Rosalina,
        MetalMario,
        PinkGoldPeach,
        Lakitu,
        ShyGuy,
        BabyMario,
        BabyLuigi,
        BabyPeach,
        BabyDaisy,
        BabyRosalina,
        Larry,
        Lemmy,
        Wendy,
        Ludwig,
        Iggy,
        Roy,
        Morton,
        Mii,
        TanookiMario,
        Link,
        VillagerMale,
        Isabelle,
        CatPeach,
        DryBowser,
        VillagerFemale,
        KingBoo,
        DryBones,
        BowserJr,
        InklingFemale,
        InklingMale
    }

    /// <summary>
    /// Represents the tire point sets stored at the resulting group indices.
    /// </summary>
    public enum TirePointSet
    {
        Standard,
        Monster,
        Roller,
        Slim,
        Slick,
        Metal,
        Button,
        OffRoad,
        Sponge,
        Wood,
        Cushion,
        BlueStandard,
        HotMonster,
        AzureRoller,
        CrimsonSlim,
        CyberSlick,
        RetroOffRoad,
        GoldTires,
        GlaTires,
        TriforceTires,
        LeafTires
    }

    /// <summary>
    /// Represents the glider point sets stored at the resulting group indices.
    /// </summary>
    public enum GliderPointSet
    {
        SuperGlider,
        CloudGlider,
        WarioWing,
        WaddleWing,
        PeachParasol,
        Parachute,
        Parafoil,
        FlowerGlider,
        BowserKite,
        PlaneGlider,
        MktvParafoil,
        GoldGlider,
        HylianKite,
        PaperGlider
    }
}

#pragma warning restore 1591
