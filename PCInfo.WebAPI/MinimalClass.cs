using System;
using System.Collections.Generic;
using System.Linq;
using PCInfo.WebAPI.Data;

public class FuzzyLogicSearch
{
    private List<MyPCInfo> data;

    public FuzzyLogicSearch(List<MyPCInfo> data)
    {
        this.data = data;
    }

    public void ClusterByFeatures()
    {
        // Кластеризация по признакам
        var clusters = data.GroupBy(x => new {
            x.OSVersion,
            x.SystemBitRate,
            x.CPUManufacturerer,
            x.CPUModel,
            x.CPUName,
            x.CPUNMinGhz,
            x.CPUNMaxGhz,
            x.CPUNBenchmark,
            x.CPUKernelCount,
            x.DriveCount,
            x.HDDSize,
            x.RAMCount,
            x.TotalRAM,
            x.VideoCardName,
            x.VideoCardMemoryAmount,
            x.VideoGhz,
            x.VideoBenchmark
        });

        foreach (var cluster in clusters)
        {
            // Анализ каждой группы
            var group = cluster.ToList();
            if (group.Count > 0)
            {
                // Пример анализа признака "Объем жесткого диска"
                var undefinedHDDSizes = group.Where(x => !x.HDDSize.HasValue).ToList();
                if (undefinedHDDSizes.Count > 0)
                {
                    Console.WriteLine($"Для группы объектов с признаками {string.Join(", ", cluster.Key)} не определен признак HDDSize");
                }

                // Пример принятия решения на основе нечетких данных
                var goodCPUs = group.Where(x => Convert.ToInt32(x.CPUNBenchmark) > 1000).ToList();
                if (goodCPUs.Count > 0)
                {
                    Console.WriteLine($"Для группы объектов с признаками {string.Join(", ", cluster.Key)} процессоры являются хорошими");
                }
            }
        }
    }
}