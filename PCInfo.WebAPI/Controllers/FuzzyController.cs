using Accord.MachineLearning.Rules;
using Microsoft.AspNetCore.Mvc;
using PCInfo.WebAPI.Data;

namespace _pcInf.WebAPI.Controllers;

[ApiController]
public class FuzzyController
{
    private DataContext _dbContext { get; set; }

    public FuzzyController(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public class AprioiriResultResponse
    {
        public string Antecedent { get; set; }
        public string Consequent { get; set; }
        public double Support { get; set; }
        public double Confidence { get; set; }
    }

    public class DTO
    {
        public string Name { get; set; }
        public string FuzzyLogicInfo { get; set; }
    }

    [HttpGet]
    [Route("fuzzy-logic")]
    public List<DTO> FuzzyLogic()
    {
        List<string> list = new List<string>();
        var response = _dbContext.pcInfos
            .Select(x => new DTO()
            {
                Name = x.PCName,
                FuzzyLogicInfo = new MyFuzzy(x).GetFuzzyRes()

            }).ToList();
        return response;
    }

    public class MyFuzzy
    {
        private MyPCInfo pCInfo { get; set; }

        public MyFuzzy(MyPCInfo _pcInfo)
        {
            pCInfo = _pcInfo;
        }

        public string GetFuzzyRes()
        {
            var sumSet = GetOS_VersionSet()
                         + GetSystemBitRateSet()
                         + GetCPUBenchmarkSet()
                         + GetVideoCardBenchmarkSet()
                         + GetVideoCardRAMSet()
                         + GetRAMSet()
                         + GetHDDsizeSet();

            double res = sumSet / 7;
            if (res >= 0.86) return "Очень Мощный";
            if (res >= 0.72) return "Мощный";
            if (res >= 0.62) return "Средний";
            return "Слабый";
        }

        private double GetOS_VersionSet()
        {
            if (pCInfo.OSVersion.Contains("Windows 11")) return 1.0;
            if (pCInfo.OSVersion.Contains("Windows 10")) return 0.9;
            if (pCInfo.OSVersion.Contains("Windows 8.1")) return 0.8;
            if (pCInfo.OSVersion.Contains("Windows 8")) return 0.79;
            if (pCInfo.OSVersion.Contains("Windows 7")) return 0.71;
            return 0.5; // other
        }

        private double GetSystemBitRateSet()
        {
            if (pCInfo.SystemBitRate == "64") return 1.0;
            if (pCInfo.SystemBitRate == "32") return 0.9;
            return 0.5; // other
        }

        private double GetCPUBenchmarkSet()
        {
            int CPU_Benchmark = int.Parse(pCInfo.CPUNBenchmark);
            if (CPU_Benchmark > 20000) return 1;
            else if (CPU_Benchmark > 15000) return 0.8;
            else if (CPU_Benchmark > 10000) return 0.7;
            else if (CPU_Benchmark > 5000) return 0.5;
            else return 0.3;
        }

        private double GetVideoCardBenchmarkSet()
        {
            int Video_Benchmark = int.Parse(pCInfo.VideoBenchmark);
            if (Video_Benchmark > 49000) return 1;
            else if (Video_Benchmark > 45500) return 0.8;
            else if (Video_Benchmark > 40000) return 0.7;
            else if (Video_Benchmark > 30000) return 0.5;
            else return 0.3;
        }

        private double GetVideoCardRAMSet()
        {
            int VideoCardMemoryAmount = int.Parse(pCInfo.VideoCardMemoryAmount);
            if (VideoCardMemoryAmount > 10000) return 1;
            else if (VideoCardMemoryAmount > 8000) return 0.8;
            else if (VideoCardMemoryAmount > 6000) return 0.7;
            else if (VideoCardMemoryAmount > 4000) return 0.5;
            else return 0.3;
        }

        private double GetRAMSet()
        {
            int TotalRAM = pCInfo.TotalRAM ?? 0;
            if (TotalRAM == 128) return 1;
            else if (TotalRAM == 64) return 0.98;
            else if (TotalRAM == 32) return 0.95;
            else if (TotalRAM == 16) return 0.90;
            else if (TotalRAM == 12) return 0.8;
            else if (TotalRAM == 8) return 0.75;
            else if (TotalRAM == 6) return 0.3;
            else if (TotalRAM == 4) return 0.2;
            else return 0.1;
        }

        private double GetHDDsizeSet()
        {
            int HDDSize = pCInfo.HDDSize ?? 0;
            if (HDDSize >= 1024) return 1;
            else if (HDDSize >= 512) return 0.8;
            else if (HDDSize >= 256) return 0.5;
            else if (HDDSize >= 128) return 0.3;
            else return 0.2;
        }
    }
}