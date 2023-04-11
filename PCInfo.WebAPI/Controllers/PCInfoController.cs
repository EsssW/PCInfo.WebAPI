using Accord.MachineLearning.Rules;
using Bogus.DataSets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCInfo.WebAPI.Data;

namespace _pcInf.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _pcInfController : ControllerBase
    {
        private DataContext _dbContext { get; set; }

        public _pcInfController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("AssociationRules")]
        public List<AprioiriResultResponse> Test()
        {
            var transactions = _dbContext.pcInfos.Select(x => new List<string>
            {
                x.OSVersion ?? "",
                "SystemBitRate " + x.SystemBitRate ?? "",
                "CPUModel " + x.CPUModel ?? "",
                //x.CPUNMinGhz ?? "",
                //x.CPUNMaxGhz ?? "",
                //x.CPUNBenchmark ?? "",
                "CPUKernelCount " + x.CPUKernelCount.ToString(),
                //x.DriveCount.ToString(),
                "HDDSize " + x.HDDSize.ToString() ?? "",
                //x.RAMCount.ToString(),
                "TotalRAM " + x.TotalRAM.ToString() ?? "",
                "VideoCardName " + x.VideoCardName ?? "",
                "VideoCardMemoryAmount " + x.VideoCardMemoryAmount ?? "",
                "VideoGhz " + x.VideoGhz ?? "",
                "VideoBenchmark " + x.VideoBenchmark ?? "",
            }).ToList();
            string[][] array = transactions.Select(Enumerable.ToArray).ToArray();

            // Create a new A-priori learning algorithm with the requirements
            var apriori = new Apriori<string>(threshold: 6, confidence: 0.7);

            // Use apriori to generate a n-itemset generation frequent pattern
            AssociationRuleMatcher<string> classifier = apriori.Learn(array);

            // Generate association rules from the itemsets:
            AssociationRule<string>[] rules = classifier.Rules;

            var result = new List<AprioiriResultResponse>();

            foreach (AssociationRule<string> rule in rules)
            {
                result.Add(new AprioiriResultResponse
                {
                    Antecedent = string.Join(" ", rule.X),
                    Consequent = string.Join(" ", rule.Y),
                    Support = rule.Support,
                    Confidence = rule.Confidence
                });
            }

            return result;
        }

        [HttpPost("Post")]
        public async Task<PutResponse> Post(MyPCInfoDTO _pcInf)
        {
            //var isPCUnique = _dbContext.pcInfos
            //    .All(x => x.PCIPV4 == _pcInf.PCIPV4 && _pcInf.PCName == x.PCName);
            //if (!isPCUnique)
            //    throw new Exception("PC with such PCIPV4 and PCName already existis");

            var my_pcInf = new MyPCInfo();

            my_pcInf.PCIPV4 = _pcInf.PCIPV4;
            my_pcInf.PCName = _pcInf.PCName;
            my_pcInf.OSVersion = _pcInf.OSVersion;
            my_pcInf.SystemBitRate = _pcInf.SystemBitRate;
            my_pcInf.SystemCatalogPath = _pcInf.SystemCatalogPath;
            my_pcInf.CPUModel = _pcInf.CPUModel;
            my_pcInf.CPUName = _pcInf.CPUName;
            my_pcInf.CPUManufacturerer = _pcInf.CPUManufacturerer;
            my_pcInf.CPUKernelCount = _pcInf.CPUKernelCount;
            my_pcInf.DriveCount = _pcInf.DriveCount;
            my_pcInf.DriveCount = _pcInf.DriveCount;
            my_pcInf.RAMCount = _pcInf.RAMCount;
            my_pcInf.TotalRAM = Int32.Parse(_pcInf.TotalRAM);
            my_pcInf.ScreenCount = _pcInf.ScreenCount;
            my_pcInf.HResol = _pcInf.HResol;
            my_pcInf.WResol = _pcInf.WResol;
            my_pcInf.VideoCardName = _pcInf.VideoCardName;
            my_pcInf.VideoCardMemoryAmount = _pcInf.VideoCardMemoryAmount;
            my_pcInf.HDDSize = Int32.Parse(_pcInf.HDDSize);


            await _dbContext.AddAsync(my_pcInf);

            foreach (var item in _pcInf.DriveInfos)
            {
                await _dbContext.AddAsync(new MyDriveInfo
                {
                    AvailableFreeSpace = Int32.Parse(item.AvailableFreeSpace),
                    Name = item.Name,
                    TotalSize = Int32.Parse(item.TotalSize),
                    DriveFormat = item.DriveFormat,
                    MyPCInfo = my_pcInf
                });
            }

            await _dbContext.SaveChangesAsync();

            return new PutResponse()
            {
                Message = my_pcInf.PCIPV4 + " Complite"
            };
        }
    }
}