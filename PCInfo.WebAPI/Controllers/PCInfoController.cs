using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCInfo.WebAPI.Data;
using System.Globalization;

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
        public async Task<List<pcInfoGetResponse>> Get()
        {
            var response = _dbContext.pcInfos
                .Select(x => new pcInfoGetResponse()
                {
                    CPUKernelCount = x.CPUKernelCount,
                    CPUManufacturerer = x.CPUManufacturerer,
                    CPUModel = x.CPUModel,
                    CPUName = x.CPUName,
                    CPUNMaxClockSpeed = x.CPUNMaxClockSpeed,
                    DriveCount = x.DriveCount,
                    HDDName = x.HDDName,
                    HDDSize = x.HDDSize,
                    HResol = x.HResol,
                    OSVersion = x.OSVersion,
                    PCIPV4 = x.PCIPV4,
                    PCName = x.PCName,
                    RAMCount = x.RAMCount,
                    ScreenCount = x.ScreenCount,
                    SystemBitRate = x.SystemBitRate,
                    SystemCatalogPath = x.SystemCatalogPath,
                    TotalRAM = x.TotalRAM,
                    VideoCardMemoryAmount = x.VideoCardMemoryAmount,
                    VideoCardName = x.VideoCardName,
                    WResol = x.WResol,
                    DriveInfos = x.MyDriveInfos!.Select(d => new MyDriveInfoDTO
                    {
                        AvailableFreeSpace = d.AvailableFreeSpace,
                        DriveFormat = d.DriveFormat,
                        Name = d.Name,
                        TotalSize = d.TotalSize,
                    }).ToList(),

                }).ToList();


            return response;
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
            my_pcInf.CPUNMaxClockSpeed = _pcInf.CPUNMaxClockSpeed;
            my_pcInf.CPUKernelCount = _pcInf.CPUKernelCount;
            my_pcInf.DriveCount = _pcInf.DriveCount;
            my_pcInf.DriveCount = _pcInf.DriveCount;
            my_pcInf.RAMCount = _pcInf.RAMCount;
            my_pcInf.TotalRAM = _pcInf.TotalRAM;
            my_pcInf.ScreenCount = _pcInf.ScreenCount;
            my_pcInf.HResol = _pcInf.HResol;
            my_pcInf.WResol = _pcInf.WResol;
            my_pcInf.VideoCardName = _pcInf.VideoCardName;
            my_pcInf.VideoCardMemoryAmount = _pcInf.VideoCardMemoryAmount;
            my_pcInf.HDDName = _pcInf.HDDName;
            my_pcInf.HDDSize = _pcInf.HDDSize;


            await _dbContext.AddAsync(my_pcInf);

            foreach (var item in _pcInf.DriveInfos)
            {
                await _dbContext.AddAsync(new MyDriveInfo
                {
                    AvailableFreeSpace = item.AvailableFreeSpace,
                    Name = item.Name,
                    TotalSize = item.TotalSize,
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
