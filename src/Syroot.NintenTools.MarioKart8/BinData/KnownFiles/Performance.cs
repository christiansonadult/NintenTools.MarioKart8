namespace Syroot.NintenTools.MarioKart8.BinData.Performance
{
    public enum Section
    {
        WeightStats, AccelerationStats, OnroadStats, OffroadStats, TurboStats,
        SpeedGroundStats, SpeedWaterStats, SpeedAntigravityStats, SpeedAirStats,
        HandlingGroundStats, HandlingWaterStats, HandlingAntigravityStats, HandlingAirStats,
        KartPoints, DriverPoints, TirePoints, GliderPoints
    }

    public enum WeightStat
    {
        Bumped, Bumping, Unknown
    }

    public enum OffroadStat
    {
        BrakeDirtLight, BrakeDirtMedium, BrakeDirtHeavy,
        BrakeSandLight, BrakeSandMedium, BrakeSandHeavy,
        BrakeIceLight, BrakeIceMedium, BrakeIceHeavy,
        SlipDirtLight, SlipDirtMedium, SlipDirtHeavy,
        SlipSandLight, SlipSandMedium, SlipSandHeavy,
        SlipIceLight, SlipIceMedium, SlipIceHeavy
    }

    public enum TurboStat
    {
        MiniTurboFrames, SuperTurboFrames
    }

    public enum SpeedStat
    {
        Speed, MaxSpeed
    }

    public enum SpeedAirStat
    {
        Speed
    }

    public enum HandlingStat
    {
        Normal, Drift, AutoDrift
    }

    public enum HandlingAirStat
    {
        Roll, Move
    }

    public enum PointSetAttrib
    {
        Weight, Acceleration, OnroadTraction, OffroadTraction, MiniTurbo,
        SpeedGround, SpeedWater, SpeedAntigravity, SpeedAir,
        HandlingGround, HandlingWater, HandlingAntigravity, HandlingAir
    }

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
