using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PCInfo.WebAPI.Data
{
    public class MyPCInfo
    {
        public int Id { get; set; }
        // SYSTEM INFO
        public string? PCIPV4 { get; set; } // ip 192.168.1.1 - 192.168.1.201
        public string? PCName { get; set; } // pc_{name} or pc_server_{ds,fl,tel,vpn,dhcp}
        public string? OSVersion { get; set; } // [Windows 10, Windows 8.1, Windows 8, Windows 7, Windows 11,  Windows XP, Windows Vista]
        public string? SystemBitRate { get; set; } // [32, 64]
        public string? SystemCatalogPath { get; set; } // [Fat32, Ext3, Ext4, NDFS]
        // CPU INFO
        public string? CPUModel { get; set; } // intel-i9/ryzen-5
        public string? CPUName { get; set; } // 7700k/5600h....
        public string? CPUManufacturerer { get; set; } // intel/amd
        public string? CPUNMinGhz{ get; set; } // 2600
        public string? CPUNMaxGhz{ get; set; } // 5200
        public string? CPUNBenchmark { get; set; } // количество очков в бенчмарк (берется из инета)
        public int CPUKernelCount { get; set; } // 1 ядро/2 ядра ....
        public int CPUThreads { get; set; } // 1 пото/ 2потока......
        // PC DRIVE INFO
        public int DriveCount { get; set; } = 0; // количестов дисков
        public int? HDDSize { get; set; } // [128, 256, 512, 1024]
        // RAM INFO
        public int RAMCount { get; set; } // [1, 2, 3, 4]
        public int? TotalRAM { get; set; } // [2, 3, 4, 6, 8, 12, 16, 32, 64, 128]
        // PC Screen Resolution
        public int ScreenCount { get; set; } // количество экранов
        public string? HResol { get; set; } // height  экрана 
        public string? WResol { get; set; } // width экрана
        // PC VIDEO CARD
        public string? VideoCardName { get; set; } = null; //  Nvidia GeForce GTX 1080 TI
        public string? VideoCardMemoryAmount { get; set; } = null; // 11264MB
        public string? VideoGhz { get; set; } = null; // Частота 1582, 1770........
        public string? VideoBenchmark { get; set; } = null; // количество очков в бенчмарк (берется из инета) 47817

        #region navigation Properties

        public List<MyDriveInfo>? MyDriveInfos { get; set; }

        #endregion
    }
}
