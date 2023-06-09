﻿using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PCInfo.WebAPI.Data
{
    public class MyPCInfo
    {
        public int Id { get; set; }

        public string? PCIPV4 { get; set; }
        public string? PCName { get; set; }
        public string? OSVersion { get; set; }
        public string? SystemBitRate { get; set; }
        public string? SystemCatalogPath { get; set; }
        public string? CPUModel { get; set; }
        public string? CPUName { get; set; }
        public string? CPUManufacturerer { get; set; }
        public string? CPUNMaxClockSpeed { get; set; }
        public int CPUKernelCount { get; set; }
        public int DriveCount { get; set; } = 0;
        public int RAMCount { get; set; }
        public string? TotalRAM { get; set; }
        public int ScreenCount { get; set; }
        public string? HResol { get; set; }
        public string? WResol { get; set; }
        public string? VideoCardName { get; set; } = null;
        public string? VideoCardMemoryAmount { get; set; } = null;
        public string? HDDName { get; set; }
        public string? HDDSize { get; set; }

        #region navigation Properties

        public List<MyDriveInfo>? MyDriveInfos { get; set; } = new List<MyDriveInfo>();

        #endregion
    }
}
