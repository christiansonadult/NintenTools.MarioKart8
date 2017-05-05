using System.IO;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData.Performance
{
    /// <summary>
    /// Represents the contents of the &quot;Performance.bin&quot; file as it appears in 4.1 versions of the game.
    /// </summary>
    public class PerformanceData
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceData"/> class from the file with the given name.
        /// </summary>
        /// <param name="fileName">The name of the file from which the instance will be loaded.</param>
        /// <param name="byteOrder">The byte order in which data will be read.</param>
        public PerformanceData(string fileName, ByteOrder byteOrder)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Load(stream, byteOrder);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceData"/> class from the given stream.
        /// </summary>
        /// <param name="stream">The stream from which the instance will be loaded.</param>
        /// <param name="byteOrder">The byte order in which data will be read.</param>
        public PerformanceData(Stream stream, ByteOrder byteOrder)
        {
            Load(stream, byteOrder);
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the 21 <see cref="WeightStat"/> instances.
        /// </summary>
        public WeightStat[] WeightStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="AccelerationStat"/> instances.
        /// </summary>
        public AccelerationStat[] AccelerationStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="OuterslipStat"/> instances.
        /// </summary>
        public OuterslipStat[] OuterslipStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="OffroadStat"/> instances.
        /// </summary>
        public OffroadStat[] OffroadStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="TurboStat"/> instances.
        /// </summary>
        public TurboStat[] TurboStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="SpeedStat"/> instances for speed while on normal ground.
        /// </summary>
        public SpeedStat[] SpeedGroundStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="SpeedStat"/> instances for speed when underwater.
        /// </summary>
        public SpeedStat[] SpeedWaterStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="SpeedStat"/> instances for speed in antigravity sections.
        /// </summary>
        public SpeedStat[] SpeedAntigravityStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="SpeedAirStat"/> instances for speed while gliding.
        /// </summary>
        public SpeedAirStat[] SpeedAirStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="HandlingStat"/> instances for turn rates when on normal ground.
        /// </summary>
        public HandlingStat[] HandlingGroundStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="HandlingStat"/> instances for turn rates while underwater.
        /// </summary>
        public HandlingStat[] HandlingWaterStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="HandlingStat"/> instances for turn rates in antigravity sections.
        /// </summary>
        public HandlingStat[] HandlingAntigravityStats { get; set; }

        /// <summary>
        /// Gets or sets the 21 <see cref="HandlingAirStat"/> instances for turn rates while gliding.
        /// </summary>
        public HandlingAirStat[] HandlingAirStats { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="PointSet"/> array for karts.
        /// </summary>
        public PointSet[] KartPoints { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="PointSet"/> array for drivers.
        /// </summary>
        public PointSet[] DriverPoints { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="PointSet"/> array for tires.
        /// </summary>
        public PointSet[] TirePoints { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="PointSet"/> array for gliders.
        /// </summary>
        public PointSet[] GliderPoints { get; set; }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Saves the data into the file with the given name.
        /// </summary>
        /// <param name="fileName">The name of the file in which the data will be stored.</param>
        /// <param name="byteOrder">The byte order in which data will be read.</param>
        public void Save(string fileName, ByteOrder byteOrder)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Save(stream, byteOrder);
            }
        }

        /// <summary>
        /// Saves the data into the the given stream.
        /// </summary>
        /// <param name="stream">The stream in which the data will be stored.</param>
        /// <param name="byteOrder">The byte order in which data will be read.</param>
        public void Save(Stream stream, ByteOrder byteOrder)
        {
            BinFile binFile = new BinFile("PERF", 84, 1000);
            binFile.Sections.Add(CreateSection("PRWG", WeightStats));
            binFile.Sections.Add(CreateSection("PRAC", AccelerationStats));
            binFile.Sections.Add(CreateSection("PRON", OuterslipStats));
            binFile.Sections.Add(CreateSection("PROF", OffroadStats));
            binFile.Sections.Add(CreateSection("PRMT", TurboStats));
            binFile.Sections.Add(CreateSection("PRSL", SpeedGroundStats));
            binFile.Sections.Add(CreateSection("PRSW", SpeedWaterStats));
            binFile.Sections.Add(CreateSection("PRSA", SpeedAntigravityStats));
            binFile.Sections.Add(CreateSection("PRSG", SpeedAirStats));
            binFile.Sections.Add(CreateSection("PRTL", HandlingGroundStats));
            binFile.Sections.Add(CreateSection("PRTW", HandlingWaterStats));
            binFile.Sections.Add(CreateSection("PRTA", HandlingAntigravityStats));
            binFile.Sections.Add(CreateSection("PRTG", HandlingAirStats));
            binFile.Sections.Add(CreateSection("PTBD", KartPoints));
            binFile.Sections.Add(CreateSection("PTDV", DriverPoints));
            binFile.Sections.Add(CreateSection("PTTR", TirePoints));
            binFile.Sections.Add(CreateSection("PTWG", GliderPoints));
            binFile.Save(stream, byteOrder);
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private void Load(Stream stream, ByteOrder byteOrder)
        {
            BinFile binFile = new BinFile(stream, byteOrder);
            WeightStats = ((ByteArraysGroup)binFile.Sections["PRWG"].Groups[0]).ToStructArray<WeightStat>();
            AccelerationStats = ((ByteArraysGroup)binFile.Sections["PRAC"].Groups[0]).ToStructArray<AccelerationStat>();
            OuterslipStats = ((ByteArraysGroup)binFile.Sections["PRON"].Groups[0]).ToStructArray<OuterslipStat>();
            OffroadStats = ((ByteArraysGroup)binFile.Sections["PROF"].Groups[0]).ToStructArray<OffroadStat>();
            TurboStats = ((ByteArraysGroup)binFile.Sections["PRMT"].Groups[0]).ToStructArray<TurboStat>();
            SpeedGroundStats = ((ByteArraysGroup)binFile.Sections["PRSL"].Groups[0]).ToStructArray<SpeedStat>();
            SpeedWaterStats = ((ByteArraysGroup)binFile.Sections["PRSW"].Groups[0]).ToStructArray<SpeedStat>();
            SpeedAntigravityStats = ((ByteArraysGroup)binFile.Sections["PRSA"].Groups[0]).ToStructArray<SpeedStat>();
            SpeedAirStats = ((ByteArraysGroup)binFile.Sections["PRSG"].Groups[0]).ToStructArray<SpeedAirStat>();
            HandlingGroundStats = ((ByteArraysGroup)binFile.Sections["PRTL"].Groups[0]).ToStructArray<HandlingStat>();
            HandlingWaterStats = ((ByteArraysGroup)binFile.Sections["PRTW"].Groups[0]).ToStructArray<HandlingStat>();
            HandlingAntigravityStats = ((ByteArraysGroup)binFile.Sections["PRTA"].Groups[0]).ToStructArray<HandlingStat>();
            HandlingAirStats = ((ByteArraysGroup)binFile.Sections["PRTG"].Groups[0]).ToStructArray<HandlingAirStat>();
            KartPoints = ((ByteArraysGroup)binFile.Sections["PTBD"].Groups[0]).ToStructArray<PointSet>();
            DriverPoints = ((ByteArraysGroup)binFile.Sections["PTDV"].Groups[0]).ToStructArray<PointSet>();
            TirePoints = ((ByteArraysGroup)binFile.Sections["PTTR"].Groups[0]).ToStructArray<PointSet>();
            GliderPoints = ((ByteArraysGroup)binFile.Sections["PTWG"].Groups[0]).ToStructArray<PointSet>();
        }

        private Section CreateSection<T>(string name, T[] instances)
        {
            Section section = new Section(name, SectionType.ByteArrays);
            ByteArraysGroup group = new ByteArraysGroup();
            group.FromStructArray(instances);
            section.Groups.Add(group);
            return section;
        }
    }
}
