using Microsoft.AspNetCore.Mvc;
using PCInfo.WebAPI.Data;
using PCInfo.WebAPI.Requests;
using PCInfo.WebAPI.ViewModels;

namespace _pcInf.WebAPI.Controllers;

[ApiController]
public class StatisticController : Controller
{
    private readonly DataContext _db;
    private const int CountItems = 10;

    public StatisticController(DataContext db)
    {
        _db = db;
    }

    [HttpGet]
    [Route("seed")]
    public void Seed()
    {
        new Seeder(_db).Run();
    }
    
    [HttpGet]
    [Route("Paginate")]
    public PaginateViewModel<MyPCInfo> Paginate([FromQuery] int page)
    {
        if (page < 1)
        {
            page = 1;
        }

        var query = _db.pcInfos.AsQueryable();

        var total = query.Count();

        var items = query
            .Skip((page - 1) * CountItems)
            .Take(CountItems)
            .ToList();

        return new PaginateViewModel<MyPCInfo>
        {
            Page = page,
            Limit = CountItems,
            Total = total,
            Items = items
        };
    }
    
    [HttpGet]
    [Route("RAM")]
    public List<RamViewModel> Ram()
    {
        return _db.pcInfos
            .GroupBy(item => item.TotalRAM)
            .Select(item => new
            {
                Name = item.Key.ToString(),
                Value = item.Count()
            })
            .ToList()
            .Select(item => new RamViewModel
            {
                Name = item.Name,
                Value = item.Value
            })
            .ToList();
    }

    [HttpGet]
    [Route("VideoCarts")]
    public List<VideoCartViewModel> VideoCarts()
    {
        return _db.pcInfos
            .GroupBy(item => item.VideoCardName.Contains("Nvidia") ? "Nvidia" : "Amd")
            .Select(item => new
            {
                Name = item.Key,
                Value = item.Count()
            })
            .ToList()
            .Select(item => new VideoCartViewModel
            {
                Name = item.Name,
                Value = item.Value
            })
            .ToList();
    }

    [HttpGet]
    [Route("HDD")]
    public List<HddViewModel> HDD()
    {
        return _db.pcInfos
            .GroupBy(item => item.HDDSize)
            .Select(item => new
            {
                Name = item.Key.ToString(),
                Value = item.Count()
            })
            .ToList()
            .Select(item => new HddViewModel
            {
                Name = item.Name,
                Value = item.Value
            })
            .ToList();
    }
}