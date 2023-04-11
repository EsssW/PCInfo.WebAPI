using Bogus;

namespace PCInfo.WebAPI.Data;

public class Seeder
{
    private readonly DataContext _db;

    public Seeder(DataContext db)
    {
        _db = db;
    }

    public void Run()
    {
        var deviceFaker = new Faker<MyDriveInfo>()
            .RuleFor(item => item.Name, (faker, item) => faker.Random.ArrayElement(new [] { "Toshiba", "A-Data", "Samsung", "Patriot" }))
            .RuleFor(item => item.DriveFormat, (faker, item) => faker.Random.ArrayElement(new [] { "NFTC", "FAT32" }))
            .RuleFor(item => item.TotalSize, (faker, item) => faker.Random.ArrayElement(new [] { 512, 1024, 1536, 2048 }))
            .RuleFor(item => item.AvailableFreeSpace, (faker, item) => faker.Random.Int(100, item.TotalSize ?? 100));
        var pcInfoFaker = new Faker<MyPCInfo>()
            .RuleFor(item => item.PCIPV4, (faker, item) => faker.Internet.Ip())
            .RuleFor(item => item.PCName, (faker, item) => "pc_" + faker.Name.FirstName())
            .RuleFor(item => item.OSVersion, (faker, item) => faker.Random.ArrayElement(new [] { "Windows 10", "Windows 8.1", "Windows 8", "Windows 7", "Windows 11" }))
            .RuleFor(item => item.SystemBitRate, (faker, item) => faker.Random.ArrayElement(new [] { "32", "64" }))
            .RuleFor(item => item.SystemCatalogPath, (faker, item) => faker.Random.ArrayElement(new [] { @"C:\WINDOWS\system\" + (item.SystemBitRate ?? "32") }))
            .RuleFor(item => item.CPUModel, (faker, item) => faker.Random.ArrayElement(GetProcessors().Select(item => item.CPU).ToArray())) //
            .RuleFor(item => item.CPUName, (faker, item) => item.CPUModel) //
            .RuleFor(item => item.CPUManufacturerer, (faker, item) => item.CPUModel) //
            .RuleFor(item => item.CPUNMinGhz, (faker, item) => GetProcessors().First(processor => processor.CPU == item.CPUModel).MinGhz.ToString())
            .RuleFor(item => item.CPUNMaxGhz, (faker, item) => GetProcessors().First(processor => processor.CPU == item.CPUModel).MaxGhz.ToString())
            .RuleFor(item => item.CPUNBenchmark, (faker, item) => GetProcessors().First(processor => processor.CPU == item.CPUModel).Benchmark.ToString())
            .RuleFor(item => item.CPUKernelCount, (faker, item) => faker.Random.Int(1, 8))
            .RuleFor(item => item.CPUThreads, (faker, item) => faker.Random.Int(1, 8))
            .RuleFor(item => item.DriveCount, (faker, item) => faker.Random.ArrayElement(new [] { 1, 2, 3, 4 }))
            .RuleFor(item => item.HDDSize, (faker, item) => faker.Random.ArrayElement(new [] { 128, 256, 512, 1024 }))
            .RuleFor(item => item.RAMCount, (faker, item) => faker.Random.ArrayElement(new [] { 1, 2, 3, 4 }))
            .RuleFor(item => item.TotalRAM, (faker, item) => faker.Random.ArrayElement(new [] { 2, 3, 4, 6, 8, 12, 16, 32, 64, 128 }))
            .RuleFor(item => item.ScreenCount, (faker, item) => faker.Random.Int())
            .RuleFor(item => item.HResol, (faker, item) => faker.Random.Int(360, 1980).ToString())
            .RuleFor(item => item.WResol, (faker, item) => faker.Random.Int(360, 1980).ToString())
            .RuleFor(item => item.VideoCardName, (faker, item) => faker.Random.ArrayElement(GetVideoCards().Select(item => item.Name).ToArray()))
            .RuleFor(item => item.VideoCardMemoryAmount, (faker, item) => GetVideoCards().First(videoCard => videoCard.Name == item.VideoCardName).RAM.ToString())
            .RuleFor(item => item.VideoGhz, (faker, item) => GetVideoCards().First(videoCard => videoCard.Name == item.VideoCardName).Ghz.ToString())
            .RuleFor(item => item.VideoBenchmark, (faker, item) => GetVideoCards().First(videoCard => videoCard.Name == item.VideoCardName).Benchmark.ToString())
            .RuleFor(item => item.MyDriveInfos, (faker, item) => deviceFaker.Generate(faker.Random.Int(1, 3)).ToList());


        _db.pcInfos.AddRange(pcInfoFaker.Generate(100).ToList());
        _db.SaveChanges();
    }

    private List<Processor> GetProcessors()
    {
        return new List<Processor>
        {
            new Processor {CPU = "Intel core i5-13600k", MinGhz = 2600, MaxGhz = 5100, Benchmark = 23883},
            new Processor {CPU = "Intel core i5-13600kf", MinGhz = 2600, MaxGhz = 5100, Benchmark = 23810},
            new Processor {CPU = "Intel core i5-12600k", MinGhz = 2800, MaxGhz = 4900, Benchmark = 17269},
            new Processor {CPU = "Intel core i5-12600kf", MinGhz = 2700, MaxGhz = 5000, Benchmark = 17029},
            new Processor {CPU = "Intel core i5-11600", MinGhz = 2800, MaxGhz = 4800, Benchmark = 11300},
            new Processor {CPU = "Intel core i5-11500B", MinGhz = 3300, MaxGhz = 4600, Benchmark = 11057},
            new Processor {CPU = "Intel core i5-10400", MinGhz = 2900, MaxGhz = 4300, Benchmark = 7475},
            new Processor {CPU = "Intel core i5-9500", MinGhz = 3000, MaxGhz = 4400, Benchmark = 5945},
            new Processor {CPU = "Intel core i5-8500", MinGhz = 3000, MaxGhz = 4100, Benchmark = 5740},
            new Processor {CPU = "Intel core i5-8500B", MinGhz = 3000, MaxGhz = 4100, Benchmark = 5635},
            new Processor {CPU = "Intel core i5-6600", MinGhz = 3300, MaxGhz = 3900, Benchmark = 3713},
            new Processor {CPU = "Intel xeon platinum 8347C", MinGhz = 2100, MaxGhz = 3500, Benchmark = 30790},
            new Processor {CPU = "Intel xeon gold 6342", MinGhz = 2800, MaxGhz = 3500, Benchmark = 29396},
            new Processor {CPU = "Intel i7-12700K", MinGhz = 2700, MaxGhz = 5000, Benchmark = 21591},
            new Processor {CPU = "Intel xeon gold 6238", MinGhz = 2100, MaxGhz = 3700, Benchmark = 17158},
            new Processor {CPU = "Intel core i9-7960X", MinGhz = 2800, MaxGhz = 4400, Benchmark = 16560},
            new Processor {CPU = "Intel core i7-11700k", MinGhz = 3600, MaxGhz = 5000, Benchmark = 14918},
            new Processor {CPU = "Intel core i3-4350T", MinGhz = 3600, MaxGhz = 3600, Benchmark = 1749},
            new Processor {CPU = "Intel core i3-8100", MinGhz = 3600, MaxGhz = 3600, Benchmark = 3755},
            new Processor {CPU = "Intel core i5-6600", MinGhz = 3300, MaxGhz = 3900, Benchmark = 3712},
            new Processor {CPU = "Intel xeon W3670", MinGhz = 3200, MaxGhz = 3450, Benchmark = 3914},
            new Processor {CPU = "Intel core i7-4770R", MinGhz = 3200, MaxGhz = 3900, Benchmark = 3997},
            new Processor {CPU = "AMD ryzen 5 5600G", MinGhz = 3900, MaxGhz = 4400, Benchmark = 11961},
            new Processor {CPU = "AMD ryzen 7 PRO 3700", MinGhz = 3600, MaxGhz = 4400, Benchmark = 12045},
            new Processor {CPU = "AMD ryzen 5 PRO 5650G", MinGhz = 3900, MaxGhz = 4400, Benchmark = 12013},
            new Processor {CPU = "AMD ryzen 9 4900HS", MinGhz = 3000, MaxGhz = 4300, Benchmark = 11075},
            new Processor {CPU = "AMD ryzen 7 4800H", MinGhz = 2900, MaxGhz = 4200, Benchmark = 11019},
            new Processor {CPU = "AMD ryzen 5 3600X", MinGhz = 3800, MaxGhz = 4400, Benchmark = 11550},
            new Processor {CPU = "AMD ryzen 5 PRO 4650G", MinGhz = 3700, MaxGhz = 4200, Benchmark = 9830},
            new Processor {CPU = "AMD ryzen 5 2600X", MinGhz = 3600, MaxGhz = 4200, Benchmark = 7892},
            new Processor {CPU = "AMD ryzen 3 PRO 1200", MinGhz = 3100, MaxGhz = 3400, Benchmark = 3798},
            new Processor {CPU = "AMD fx-9370", MinGhz = 4400, MaxGhz = 4700, Benchmark = 3647},
            new Processor {CPU = "AMD fx-8310", MinGhz = 3400, MaxGhz = 4300, Benchmark = 3284},
            new Processor {CPU = "AMD fx-4150", MinGhz = 3900, MaxGhz = 4100, Benchmark = 2004},
            new Processor {CPU = "AMD A8-9600", MinGhz = 3100, MaxGhz = 3400, Benchmark = 1942},
            new Processor {CPU = "AMD A10-7700K", MinGhz = 3400, MaxGhz = 3800, Benchmark = 1937},
        };
    }

    private List<VideoCard> GetVideoCards()
    {
        return new List<VideoCard>
        {
            new VideoCard { Name = "Nvinia titan Xp",	RAM = 12280,	Ghz = 1582,	Benchmark = 49809 },
            new VideoCard { Name = "Nvidia GeForce GTX 1080 TI",	RAM = 11264,	Ghz = 1582,	Benchmark = 47817 },
            new VideoCard { Name = "AMD radeon RX 6650 XT",	RAM = 8192,	Ghz = 2635,	Benchmark = 47576 },
            new VideoCard { Name = "Nvidia GeForce RTX 2070 Super",	RAM = 8192,	Ghz = 1770,	Benchmark = 47065 },
            new VideoCard { Name = "AMD Radeon RX 5700 XT",	RAM = 8192,	Ghz = 1755,	Benchmark = 45077 },
            new VideoCard { Name = "Nvidia quadro K5000M",	RAM = 4096,	Ghz = 712,	Benchmark = 6314 },
            new VideoCard { Name = "Nvidia GeForce GTX 465",	RAM = 1024,	Ghz = 607,	Benchmark = 6210 },
        };
    }
}

class Processor
{
    public string CPU { get; set; }
    public int MinGhz { get; set; }
    public int MaxGhz { get; set; }
    public int Benchmark { get; set; }
}

class VideoCard
{
    public string Name { get; set; }
    public int RAM { get; set; }
    public int Ghz { get; set; }
    public int Benchmark { get; set; }
}